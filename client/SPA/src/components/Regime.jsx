import "./Regime.css";

function Regime({ value, onChange }) {
  return (
    <div className="regime-container">
      <button
        className={`regime-button ${value === "live" ? "active" : ""}`}
        onClick={() => onChange("live")}
      >
        Live
      </button>

      <button
        className={`regime-button ${value === "historical" ? "active" : ""}`}
        onClick={() => onChange("historical")}
      >
        Historical
      </button>
    </div>
  );
}

export default Regime;