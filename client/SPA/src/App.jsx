import { useEffect, useState } from "react";
import './App.css';
import Status from "./components/Status.jsx";
import SparkLine from "./components/SparkLine.jsx";
import Regime from "./components/Regime.jsx";
import AnimatedCell from "./components/AnimatedCell.jsx";
import DateRangePicker from "./components/DateRangePicker.jsx";
import ConnectionStatus from "./components/ConnectionStatus.jsx";


function App() 
{
  const [sensorsData, setSensorsData] = useState([])
  const [regime, setRegime] = useState("live");
  const [startPeriod, setStartDate] = useState(new Date( new Date() - 60000 * 60 * 24));
  const [endPeriod, setEndDate] = useState(new Date());
  const [connectionState, setConnectionState] = useState("OFFLINE");
  const [uploadError, setUploadError] = useState("");
  const [uploadSuccess, setUploadSuccess] = useState("");
  const handleXmlUpload = async (event) => {
    const file = event.target.files[0];
    if (!file) return;

    setUploadError("");
    setUploadSuccess("");

    try {
      const text = await file.text();

      const response = await fetch("/api/data/xml", {
        method: "POST",
        headers: {
          "Content-Type": "application/xml",
          "Accept": "application/json"
        },
        body: text
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText);
      }

      setUploadSuccess("Файл успешно отправлен");
    } catch (err) {
      setUploadError("Ошибка сервера: " + err.message);
    }
  };
  const load = async () =>{
        try
        {
          let end = new Date();
          let start = new Date(end - 5 * 1000);
          if(regime !="live"){
            start = startPeriod;
            end = endPeriod;
          }
          const results  = await Promise.all([
            FetchSensorsData(1, start, end),
            FetchSensorsData(2, start, end),
            FetchSensorsData(3, start, end)
          ]);
          setSensorsData(results);
          setConnectionState("ONLINE");
        }
        catch (err) {
          console.error("Connection error:" +err);
          setConnectionState(prev => prev === "ONLINE" ? "RECONNECTING" : "OFFLINE");
        }
    };

  useEffect(() => { 
    if (regime !== "live") return;
    load();
    const interval = regime === "live"
    ? setInterval(load, 5000)
    : null;

    return () => {
      clearInterval(interval);
    }; 
  }, [regime, startPeriod, endPeriod]);

  return (
    <div className="container">
      <div className="header__buttons">
        <div className="regime-filter">
            <Regime value={regime} onChange={setRegime}/> 
              <DateRangePicker regime={regime} startPeriod={startPeriod} endPeriod={endPeriod} setStartDate={setStartDate} setEndDate={setEndDate} load={load}/>
        </div>
        <ConnectionStatus status = {connectionState}/>
      </div>
      <table className="sensor-table">
        <thead>
          <tr>
            <th>Sensor</th>
            <th>Последнее<br/>значение</th>
            <th>Тренд<br/>(последние 60 секунд)</th>
            <th>Min</th>
            <th>Max</th>
            <th>Avg</th>
            <th>Статус</th>
            <th>Обновленно</th>
          </tr>
        </thead>
        <tbody>
          {sensorsData.map(sensor => (
            <tr key={sensor.sensorId}>
              <AnimatedCell value = {sensor.sensorId} innerComponent={null}/>
              <AnimatedCell value = {sensor.lastValue} innerComponent={null}/>
              <td><SparkLine values = {sensor.sensorTrend}/></td>
              <AnimatedCell value ={sensor.min} innerComponent={null} />
              <AnimatedCell value = {sensor.max} innerComponent={null}/>
              <AnimatedCell value = {sensor.avg} innerComponent={null}/>
              <AnimatedCell value = {GetValueStatus(sensor.lastValue)}>
                <Status status = {GetValueStatus(sensor.lastValue)}/>
              </AnimatedCell>
              <AnimatedCell value = {new Date(sensor.timestamp).toLocaleTimeString("ru-RU")}/>
            </tr>
          ))}
        </tbody>
      </table>
      <div className="upload-wrapper">
        <label className="upload-button">
          Выбрать XML
          <input
            type="file"
            accept=".xml"
            onChange={handleXmlUpload}
            hidden
          />
        </label>

        {uploadSuccess && (
          <span className="upload-success">{uploadSuccess}</span>
        )}

        {uploadError && (
          <span className="upload-error">{uploadError}</span>
        )}
      </div>
    </div>
  );
}

export function GetValueStatus(value){
  if(value == null)
    return "UNDEFINED";
  if(value > 0 && value < 70)
    return "OK";
  if(value >= 70 && value < 90)
    return "WARNING"
  return "ERROR";
}

export async function FetchSensorsData(sensorId, periodStart, periodEnd) 
  {
    const params5sec = new URLSearchParams({
        sensorId,
        start:  periodStart.toISOString(),
        end: periodEnd.toISOString()
      });
    
    const params60sec = new URLSearchParams({
        sensorId,
        start:  new Date(periodEnd.getTime() - 60 * 1000).toISOString(),
        end: periodEnd.toISOString()
      });

    const paramsSummury = new URLSearchParams({
        sensorId,
        start: periodStart.toISOString(),
        end: periodEnd.toISOString()
      });

    const [sensorData, sensorTrend, sensorSummury] = await Promise.all([
      fetch(`/api/data?${params5sec}`),
      fetch(`/api/data?${params60sec}`),
      fetch(`/api/sensors/summury?${paramsSummury}`)
    ]);

  /* if (!sensorData.ok || !sensorSummury.ok || !sensorTrend.ok) {
      throw new Error("API error");
    }*/

    const sensors = await sensorData.json();
    const summury = await sensorSummury.json();
    const trend = await sensorTrend.json();
    
    const lastValue = sensors?.length
      ? sensors[sensors.length - 1].value
      : null;

    if (lastValue === null) {
        return {
        sensorId,
        lastValue: null,
        sensorTrend: [],
        min: null,
        max: null,
        avg: null,
        timestamp: new Date()
      };
    }

    return {
      sensorId,
      lastValue: lastValue,
      sensorTrend: trend.map(s => s.value),
      min: summury.min,
      max: summury.max,
      avg: summury.avg,
      timestamp: new Date()
    };
  };

export default App
