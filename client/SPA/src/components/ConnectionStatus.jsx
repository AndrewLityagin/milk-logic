// components/Status.jsx

import "./ConnectionStatus.css";

function ConnectionStatus({status}) {
  if (!status) return null;

  const statusMap = {
    ONLINE: {
      text: "ONLINE",
      className: "status-online"
    },
    RECONNECTING: {
      text: "RECONNECTING",
      className: "status-reconnecting"
    },
    OFFLINE: {
      text: "OFFLINE",
      className: "status-offline"
    }
  };
 
 const current = statusMap[status];

  if (!current) return null;

  return (
    <div className={`connection-status`}>
        <div className={`connection-indicator ${current.className}`}></div>
        <span>{current.text}</span>
    </div>
  );
}

export default ConnectionStatus;