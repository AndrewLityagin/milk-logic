// components/Status.jsx

import "./Status.css";

function Status({ status, sensorTrend = [], timestamp }) {

  if (!status) return null;

  const statusMap = {
    OK: {
      text: "OK",
      className: "status-ok",
      description: "Значение в нормальном диапазоне (0–70)."
    },
    WARNING: {
      text: "WARNING",
      className: "status-warning",
      description: "Повышенное значение (70–90)."
    },
    ERROR: {
      text: "ERROR",
      className: "status-error",
      description: "Критическое значение (≥90)."
    },
    UNDEFINED: {
      text: "UNDEFINED",
      className: "status-undefined",
      description: "Нет данных от датчика."
    }
  };

  const current = statusMap[status];
  if (!current) return null;

  const getStatus = (v) => {
    if (v == null) return "UNDEFINED";
    if (v > 0 && v < 70) return "OK";
    if (v >= 70 && v < 90) return "WARNING";
    return "ERROR";
  };

  const lastValues = sensorTrend.slice(-5);

  const historyTooltip = lastValues
    .map((value, i) => {
      const index = sensorTrend.length - lastValues.length + i;

      const time = new Date(timestamp);
      const offset = sensorTrend.length - 1 - index;

      time.setSeconds(time.getSeconds() - offset);

      const s = getStatus(value);

      return `${time.toLocaleTimeString("ru-RU")}  ${s} (${value})`;
    })
    .join("\n");

  const tooltip =
    current.description +
    (historyTooltip ? "\n\nПоследние значения:\n" + historyTooltip : "");

  return (
    <div className="status-badge" title={tooltip}>
      <div className={`indicator ${current.className}`}></div>
      <span>{current.text}</span>
    </div>
  );
}

export default Status;