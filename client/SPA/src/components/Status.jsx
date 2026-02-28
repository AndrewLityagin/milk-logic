// components/Status.jsx

import "./Status.css";

function Status({status}) {
    
  if (!status) return null;

  const statusMap = {
    OK: {
      text: "OK",
      className: "status-ok"
    },
    WARNING: {
      text: "WARNING",
      className: "status-warning"
    },
    ERROR: {
      text: "ERROR",
      className: "status-error"
    },
    UNDEFINED: {
      text: "UNDEFINED",
      className: "status-undefined"
    }
  };
 
 const current = statusMap[status];

  if (!current) return null;

  return (
    <div className={`status-badge`}>
        <div className={`indicator ${current.className}`}></div>
        <span>{current.text}</span>
    </div>
  );
}

export default Status;