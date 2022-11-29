/**
 *  Component that creates a single card representation.
 */

import React, { FunctionComponent } from 'react';
import styles from './StockChart.module.scss'

interface StockChartProps {
  name: string,
  onClick?: (event: React.MouseEvent<HTMLElement>) => void,
}

const StockChart : FunctionComponent<StockChartProps> = ({ name, onClick }) => {
  return (
    <div
      className={`${styles.stockChart} ${onClick ? styles.clickable : ''}`}
      onClick={onClick}
    >
      <h4>{ name }</h4>
    </div>
  )
}

export default StockChart