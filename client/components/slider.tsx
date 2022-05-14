/**
 *  Component that creates a slider that can house multiple components in a
 *  scrollable row- or column-like fashion inspired by the Netflix grid.
 */

import React, { FunctionComponent, useState, useRef } from 'react';
import styles from './slider.module.scss'

interface SliderProps {
  children: JSX.Element | JSX.Element[],
  column?: boolean,
}

const Slider : FunctionComponent<SliderProps> = ({ children, column=false }) => {

  const sliderRef = useRef(null);
  const scrollContainerRef = useRef(null);
  const [scrollAmount, setScrollAmount] = useState<number>(0);

  const wrapSlide = (content: JSX.Element) : JSX.Element => (
    <div className={styles.slide}>
      { content }
    </div>
  )

  const sliderWidth = () : number => sliderRef?.current ? sliderRef?.current['clientWidth'] : 0
  const scrollContainerWidth = () : number => scrollContainerRef?.current ? scrollContainerRef?.current['clientWidth'] : 0
  const maxScrollAmount = () : number => scrollContainerWidth() - sliderWidth()

  const scroll = (direction: string) : void => {
    switch(direction) {
      case 'back': setScrollAmount((amount:number) : number => Math.min(amount + sliderWidth(), 0)); break
      case 'forward': setScrollAmount((amount:number) : number => Math.max(amount - sliderWidth(), -maxScrollAmount())); break
      default: return
    }
  }

  return (
    <div
      ref={sliderRef}
      className={`${styles.slider} ${column ? styles.column : ''}`}
    >

      <div
        ref={scrollContainerRef}
        className={styles.scrollContainer}
        style={{ '--scrollAmount': `${scrollAmount}px` } as React.CSSProperties}
      >
        { React.Children.map(children, wrapSlide) }
      </div>

      <button
        className={styles.start}
        onClick={() => scroll('back')}
      >
        {`<`}
      </button>

      <button
        className={styles.end}
        onClick={() => scroll('forward')}
      >
        {`>`}
      </button>

    </div>
  )
}

export default Slider