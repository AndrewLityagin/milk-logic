// components/Sparkline.jsx

import { BarChart, Bar, Tooltip } from "recharts";
import "./SparkLine.css";

function Sparkline({ values, timestamp }) {

  const data = values.map((v, i) => ({
    index: i,
    value: v
  }));

  return (
    <div className={`sparkline-container`}>
        <BarChart width={175} height={40} data={data}>
           <Tooltip
              formatter={(value, name, props) => {
                const index = props.payload.index;
                const time = new Date(timestamp);
                const offset = values.length - 1 - index;
                time.setSeconds(time.getSeconds() - offset);
                return [`${value}`, time.toLocaleTimeString("ru-RU")];
              }}
              contentStyle={{
                backgroundColor: "#1e1e1e",
                border: "1px solid #cfd6e4",
                borderRadius: "0px",
                color: "#e6edf3",
                fontSize: "12px",
                padding: "8px 10px",
                lineHeight: "1.4",
                boxShadow: "none",
                fontFamily: "Consolas, monospace"
              }}
              labelStyle={{
                color: "#e6edf3",
                fontWeight: "500",
                marginBottom: "4px"
              }}
              itemStyle={{
                color: "#e6edf3",
                fontFamily: "Consolas, monospace"
              }}
              cursor={{
                fill: "rgba(255,255,255,0.05)"
              }}
            />
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