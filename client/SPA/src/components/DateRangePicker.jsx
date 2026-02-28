import "./DateRangePicker.css";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function DateRangePicker({ regime, startPeriod, endPeriod, setStartDate, setEndDate, load}) {
  return (
     <div className="date-range-picker">
          <span>От: </span>
          <DatePicker
            showTimeSelect
            selected={startPeriod}
            maxDate={endPeriod}
            onChange={(date) => setStartDate(date)}
            dateFormat="dd.MM.yyyy HH:MM:ss"
            className="date-picker-input"
            disabled={regime=="live"}/>
          <span>До: </span>
          <DatePicker
            showTimeSelect
            minDate={startPeriod}
            selected={endPeriod}
            onChange={(date) => setEndDate(date)}
            dateFormat="dd.MM.yyyy HH:MM:ss"
            className="date-picker-input"
            disabled={regime=="live"}/>
          <button className="show-button" onClick={load}  disabled={regime=="live"}>
            Показать
          </button>
        </div>
            
  );
}
export default DateRangePicker;
