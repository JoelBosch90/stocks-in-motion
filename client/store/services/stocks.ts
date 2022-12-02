import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { RawStockPrice } from '../../types/StockPrice'

interface StockPricesByTickerParams {
  ticker: string
  first: string
  last: string
}

export const stocksApi = createApi({
  reducerPath: 'stocksApi',
  baseQuery: fetchBaseQuery({ baseUrl: '/api/' }),
  endpoints: (builder) => ({
    getStockPricesByTicker: builder.query<RawStockPrice[], StockPricesByTickerParams>({
      query: ({ ticker, first, last }) => 
        `stocks/${ticker}?first=${first}&last=${last}`,
    }),
    getStocks: builder.query<string[], void>({
      query: () => 'stocks'
    })
  }),
})

export const { useGetStockPricesByTickerQuery, useGetStocksQuery } = stocksApi