import { StockPrice, RawStockPrice } from "../types/StockPrice"

const formatStockPrices = (stockPrices: RawStockPrice[]): StockPrice[] => {
  return Array.isArray(stockPrices) ? stockPrices.map((stockPrice: RawStockPrice) => ({
    ...stockPrice,
    added: new Date(stockPrice.added),
    moment: new Date(stockPrice.moment),

    // Convert cents to decimal currencies.
    open: stockPrice.open / 100,
    close: stockPrice.open / 100,
    high: stockPrice.open / 100,
    low: stockPrice.open / 100,
  })) : []
}

export default formatStockPrices
