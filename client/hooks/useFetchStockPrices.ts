import { useEffect } from 'react'
import { StockPrice } from "../types/StockPrice"
import dateToYyyyMmDd from "../helpers/dateToYyyyMmDd"
import formatStockPrices from "../helpers/formatStockPrices"

type StockPriceSetter = (value: StockPrice[]) => void

const useFetchStockPrices = (ticker: string, setter: StockPriceSetter, first: Date, last: Date) => {
  useEffect(() => {  
    fetch(`/api/stocks/${ticker}?first=${dateToYyyyMmDd(first)}&last=${dateToYyyyMmDd(last)}`)
      .then((response: Response) => response.json())
      .then(formatStockPrices)
      .then(setter)
  }, [ticker, setter])
}

export default useFetchStockPrices