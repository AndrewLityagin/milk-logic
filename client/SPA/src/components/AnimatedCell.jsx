import { useEffect, useRef, useState } from "react";
import "./AnimatedCell.css";
function AnimatedCell({ value, children }) {
  const prevValue = useRef(value);
  const [flash, setFlash] = useState(false);

  useEffect(() => {
    if (prevValue.current !== value) {
      setFlash(false);
      requestAnimationFrame(() => setFlash(true));
      prevValue.current = value;
    }
  }, [value]);

  return (
    <td className={flash ? "cell-flash" : ""}>
        {children ?? value}
    </td>
  );
}

export default AnimatedCell;