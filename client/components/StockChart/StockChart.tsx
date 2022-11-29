/**
 *  Component that creates a single stock chart.
 */

import React, { FunctionComponent } from 'react';
import styles from './StockChart.module.scss'
import { useEffect, useState } from 'react';
import { StockPrice, RawStockPrice } from "../../types/StockPrice"

interface StockChartProps {
  ticker: string;
}

const StockChart : FunctionComponent<StockChartProps> = ({ ticker }) => {
  const [stockPrices, setStockPrices] = useState<StockPrice[]>([])

  const formatStockPriceDates = (stockPrices: RawStockPrice[]) => {
    return Array.isArray(stockPrices) ? stockPrices.map((stockPrice: RawStockPrice) => ({
      ...stockPrice,
      added: new Date(stockPrice.added),
      moment: new Date(stockPrice.moment),
    })) : [];
  }

  // Get the tickers from the API.
  useEffect(() => {
    fetch(`/api/stocks/${ticker}`)
      .then((response: Response) => response.json())
      .then(formatStockPriceDates)
      .then(setStockPrices)
  }, [ticker, setStockPrices])

  return (
    <div className={`${styles["stock-chart"]}`}>
      {stockPrices.map(stockPrice => (<p key={stockPrice.id}>${stockPrice?.close}</p>))}
    </div>
  )
}

export default StockChart