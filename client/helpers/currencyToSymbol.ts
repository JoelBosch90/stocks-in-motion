export const symbols: Record<string, string> = {
  EUR: '€',
  USD: '$',
  AUD: '$',
  CAD: '$',
  HKD: '$',
  JPY: '¥',
  GBP: '£',
  CHF: '₣',
  KRW: '₩',
}
const currencyToSymbol = (currency: string) : string => currency in symbols ? symbols[currency] : currency

export default currencyToSymbol
