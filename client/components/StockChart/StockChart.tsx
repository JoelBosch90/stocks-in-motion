/**
 *  Component that creates a single stock chart.
 */

import React, { FunctionComponent } from 'react';
import styles from './StockChart.module.scss'
import { useEffect, useState } from 'react';
import { StockPrice, RawStockPrice } from "../../types/StockPrice"
import LineChart from "../LineChart/LineChart"

interface StockChartProps {
  ticker: string;
}

const StockChart : FunctionComponent<StockChartProps> = ({ ticker }) => {
  const [stockPrices, setStockPrices] = useState<StockPrice[]>([])

  const formatStockPrices = (stockPrices: RawStockPrice[]) => {
    return Array.isArray(stockPrices) ? stockPrices.map((stockPrice: RawStockPrice) => ({
      ...stockPrice,
      added: new Date(stockPrice.added),
      moment: new Date(stockPrice.moment),

      // Convert cents to dollars.
      open: stockPrice.open / 100,
      close: stockPrice.open / 100,
      high: stockPrice.open / 100,
      low: stockPrice.open / 100,
    })) : [];
  }

  // Get the tickers from the API.
  useEffect(() => {
    fetch(`/api/stocks/${ticker}`)
      .then((response: Response) => response.json())
      .then(formatStockPrices)
      .then(setStockPrices)
  }, [ticker, setStockPrices])

  return (
    <div className={`${styles["stock-chart"]}`}>
      <LineChart data={stockPrices.map((stockPrice: StockPrice) => ({ y: stockPrice.close, x: stockPrice.moment }))} />
    </div>
  )
}

export default StockChart