/**
 *  Component that creates a single stock chart.
 */

import React, { FunctionComponent, useState } from 'react'
import { StockPrice } from "../../types/StockPrice"
import currencyToSymbol from "../../helpers/currencyToSymbol"
import useFetchStockPrices from '../../hooks/useFetchStockPrices'
import TimePriceChart, { InputPoint, InputData } from "../TimePriceChart/TimePriceChart"

export interface StockChartProps {
  ticker: string
}

const StockChart : FunctionComponent<StockChartProps> = ({ ticker }) => {
  const [stockPrices, setStockPrices] = useState<StockPrice[]>([])
  const currency = currencyToSymbol(stockPrices.length > 1 ? stockPrices[0].currency : "USD")
  
  const last = new Date()
  const first = new Date(last)
  first.setDate(1)
  first.setMonth(last.getMonth() - 6)
  useFetchStockPrices(ticker, setStockPrices, first, last)

  const data: InputData = stockPrices.map((stockPrice: StockPrice) : InputPoint => ({
    date: stockPrice.moment,
    price: stockPrice.close,
  }))

  return <TimePriceChart data={data} currency={currency} />
}

export default StockChart