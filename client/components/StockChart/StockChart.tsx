/**
 *  Component that creates a single stock chart.
 */

import React, { FunctionComponent } from 'react'
import { StockPrice } from '../../types/StockPrice'
import currencyToSymbol from '../../helpers/currencyToSymbol'
import TimePriceChart, { InputPoint, InputData } from '../TimePriceChart/TimePriceChart'
import { useGetStockPricesByTickerQuery } from '../../store/services/stocks'
import formatStockPrices from '../../helpers/formatStockPrices'
import dateToYyyyMmDd from '../../helpers/dateToYyyyMmDd'

export interface StockChartProps {
  ticker: string
}

const StockChart : FunctionComponent<StockChartProps> = ({ ticker }) => {
  const last = new Date()
  const first = new Date(last)
  first.setDate(1)
  first.setMonth(last.getMonth() - 6)
  const { data } = useGetStockPricesByTickerQuery({
    ticker,
    first: dateToYyyyMmDd(first),
    last: dateToYyyyMmDd(last)
  })

  const stockPrices = formatStockPrices(data ?? [])
  const currency = currencyToSymbol(stockPrices.length > 1 ? stockPrices[0].currency : 'USD')
  const refinedData: InputData = stockPrices.map((stockPrice: StockPrice) : InputPoint => ({
    date: stockPrice.moment,
    price: stockPrice.close,
  }))

  return <TimePriceChart data={refinedData} currency={currency} />
}

export default StockChart