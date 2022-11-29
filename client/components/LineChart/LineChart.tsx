/**
 *  Component that creates a single card representation.
 */

import React, { FunctionComponent } from 'react';
import * as d3 from "d3";
import styles from './LineChart.module.scss'

interface LineChartPoint {
  x: number | Date;
  y: number;
}

interface LineChartProps {
  data: LineChartPoint[];
}

const LineChart : FunctionComponent<LineChartProps> = ({ data }) => {
  const line = d3.line()
  const dateToNumber = (date: Date | number) => date instanceof Date ? date.getDate() : date;
  const dataTransformed = data.map((point: LineChartPoint) => [dateToNumber(point.x), point.y])
  const d = line(data.map((point: LineChartPoint) => [dateToNumber(point.x), point.y]))
  console.log(data.map((point: LineChartPoint) => [dateToNumber(point.x), point.y]))

  return (
    <svg className={`${styles["line-chart"]}`} viewBox={`0 0 800 400`}>
      <path d={d ?? ""} fill="none" stroke="currentColor" />
    </svg>
  )
}

export default LineChart