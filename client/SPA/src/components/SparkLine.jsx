// components/Sparkline.jsx

import { BarChart, Bar } from "recharts";
import "./SparkLine.css";

function Sparkline({ values }) {

  const data = values.map((v, i) => ({
    index: i,
    value: v
  }));

  return (
    <div className={`sparkline-container`}>
        <BarChart width={175} height={40} data={data}>
            <Bar
                type="monotone"
                dataKey="value"
                stroke="#FFFFFF"
                strokeWidth={2}
                dot={false}
            />
        </BarChart>
    </div>
  );
}

export default Sparkline;