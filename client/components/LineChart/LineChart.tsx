/**
 *  Component that creates a single card representation.
 */

import React, { FunctionComponent } from 'react';
import styles from './LineChart.module.scss'

interface LineChartProps {
  labels: [];
  data: [];
}

const LineChart : FunctionComponent<LineChartProps> = ({ labels, data }) => {
  return (
    <div className={`${styles["line-chart"]}`}>
    </div>
  )
}

export default LineChart