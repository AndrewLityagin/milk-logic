import { useEffect, useState } from "react";
import { api } from "./api/client";
import './App.css'

function App() {
  const [data, setData] = useState(0)
   const now = new Date();
   const fiveMinutesAgo = new Date(now - 5 * 60 * 1000);
   useEffect(() => {
    api.get("/data", {
    params: {
      start: fiveMinutesAgo,
      end: now
    }
  }).then(res => {
         console.log(res.data); 
        setData(res.data);
      })
      .catch(err => {
        console.error("Ошибка:", err);
      });
  }, []);

    return (
    <div>
      <h1>Данные с сервера</h1>
       <table border="1">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Value</th>
          </tr>
        </thead>
         <tbody>
            {data.length === 0 && <p>Загрузка...</p>}
            {Array.isArray(data) &&
              data.map(item => 
                (
                  <tr key={item.id}>
                    <td>{item.id}</td>
                    <td>{item.sensorid}</td>
                    <td>{item.timestamp}</td>
                    <td>{item.value}</td>
                </tr>
              ))}
          </tbody>
      </table>
    </div>
  );
}

export default App
