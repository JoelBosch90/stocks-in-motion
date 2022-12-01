const dateToYyyyMmDd = (date: Date) => `${date.getUTCFullYear()}-${date.getUTCMonth() + 1}-${date.getUTCDate()}`

export default dateToYyyyMmDd