export interface RawStockPrice {
  id: number;
  added: string;
  moment: string;
  currency: string;
  open: number;
  high: number;
  close: number;
  low: number;
  volume: number;
  dataRequestId: number;
  stockId: number;
}

export interface StockPrice {
  id: number;
  added: Date;
  moment: Date;
  currency: string;
  open: number;
  high: number;
  close: number;
  low: number;
  volume: number;
  dataRequestId: number;
  stockId: number;
}