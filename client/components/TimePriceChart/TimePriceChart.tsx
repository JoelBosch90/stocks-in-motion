/**
 *  Component that creates a chart that plots price over time.
 */

import React, { FunctionComponent, useRef, useState, MouseEvent } from 'react'
import useMeasure from 'react-use-measure'
import styles from './TimePriceChart.module.scss'
import * as d3 from "d3"
import debounce from "../../helpers/debounce"

export interface TimePriceChartProps {
  data: InputData
  currency: string
}

export type InputData = InputPoint[]
export interface InputPoint {
  date: Date
  price: number
}

type ChartPoint = [number, number]
type ChartData = ChartPoint[]

interface ToolTipState {
  x: number
  y: number
  date?: Date
  price?: number
  visible: boolean
}

const TimePriceChartLoader : FunctionComponent = () => <div className={`${styles.loader}`} />

const TimePriceChart : FunctionComponent<TimePriceChartProps> = ({ data, currency }) => {
  const defaultTooltip = {
    x: 0,
    y: 0,
    date: undefined,
    price: undefined,
    visible: false,
  }
  const [tooltip, setTooltip] = useState<ToolTipState>(defaultTooltip)
  const tooltipRef = useRef<SVGTextElement>(null)

  const [svgRef, svgBounds] = useMeasure()

  if (data.length < 1) return <TimePriceChartLoader />

  const chartData: ChartData = data
    // @TODO: For now, I'm filtering out double dates, but we really should
    // solve this in the database.
    .filter((item: InputPoint, index: number, array: InputData) : boolean => array.findIndex(point => point.date.getTime() === item.date.getTime()) === index)
    // Convert the input objects to a [number, number] format that easier to
    // work with in d3.
    .map((point: InputPoint): ChartPoint => [point.date.getTime(), point.price])
  chartData.sort((a: ChartPoint, b: ChartPoint) => a[0] - b[0])

  const minX = chartData.reduce((previous: ChartPoint, current: ChartPoint) => previous[0] < current[0] ? previous : current)[0]
  const maxX = chartData.reduce((previous: ChartPoint, current: ChartPoint) => previous[0] > current[0] ? previous : current)[0]
  const minY = chartData.reduce((previous: ChartPoint, current: ChartPoint) => previous[1] < current[1] ? previous : current)[1]
  const maxY = chartData.reduce((previous: ChartPoint, current: ChartPoint) => previous[1] > current[1] ? previous : current)[1]

  // @TODO: I don't like that this is not dynamic. We should come up with these
  // values in a different way.
  const margins = {
    top: 25,
    right: 50,
    bottom: 25,
    left: 50,
    tooltip: 5,
  }

  const xScale = d3
    .scaleTime()
    .domain([minX, maxX])
    .range([margins.left, svgBounds.width - margins.right])
  const yScale = d3
    .scaleLinear()
    .domain([minY, maxY])
    .range([svgBounds.height - (margins.bottom + margins.top), margins.top])

  const line = d3.line()
    .x((d) => xScale(d[0]))
    .y((d) => yScale(d[1]))
  const d = line(chartData) ?? undefined

  return (
    <div className={styles.chart}>
      <svg ref={svgRef} viewBox={`0 0 ${svgBounds.width} ${svgBounds.height}`}>
        {yScale.ticks(5).map((price) => (
          <g
            className={styles.legend}
            key={price}
            transform={`translate(0, ${yScale(price)})`}
          >
            <line
              x1={margins.left}
              x2={svgBounds.width - margins.right}
              stroke="currentColor"
              strokeWidth={1}
              strokeDasharray="2,5"
            />
            <text
              fill="currentColor"
              dominantBaseline="middle"
            >
              {`${currency}${price}`}
            </text>
          </g>
        ))}
        
        {xScale.ticks().map((date) => (
          <g
            className={styles.legend}
            key={date.getTime()}
            transform={`translate(${xScale(date)}, ${svgBounds.height - margins.bottom})`}
          >
            <text
              fill="currentColor"
              dominantBaseline="middle"
              textAnchor="middle"
            >
              {new Date(date).toLocaleString("default", { month: "long" })}
            </text>
          </g>
        ))}

        <path d={d} fill="none" stroke="currentColor" />

        {chartData.map((point) => (
          <circle
            r="2"
            key={point[0]}
            cx={xScale(point[0])}
            cy={yScale(point[1])}
            fill="currentColor"
            onMouseEnter={() => setTooltip({
              x: xScale(point[0]),
              y: yScale(point[1] + margins.tooltip),
              date: new Date(point[0]),
              price: point[1],
              visible: true,
            })}
            onMouseOut={debounce<void>(() => setTooltip(defaultTooltip))}
          />
        ))}

        <text
          ref={tooltipRef}
          className={styles.tooltip}
          style={{
            opacity: tooltip.visible ? 1 : 0,
            visibility: tooltip.visible ? "visible" : "hidden"
          }}
          x={tooltip.x}
          y={tooltip.y}
          fill="currentColor"
        >
          {tooltip.price ?  `${currency}${tooltip.price}` : "Loading..."}
        </text>
      </svg>
    </div>
  )
}

export default TimePriceChart