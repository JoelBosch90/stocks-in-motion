/**
 *  Component that creates a carousel that can house multiple components in a
 *  scrollable row- or column-like fashion inspired by the Netflix grid.
 */

import React, { FunctionComponent, useState, useRef } from 'react';
import styles from './Carousel.module.scss'

interface CarouselProps {
  children: React.ReactNode,
  column?: boolean,
}

const Carousel : FunctionComponent<CarouselProps> = ({ children, column=false }) => {

  const carouselRef = useRef(null);
  const scrollContainerRef = useRef(null);
  const [scrollAmount, setScrollAmount] = useState<number>(0);

  const wrapCard = (content: React.ReactNode) : React.ReactNode => (
    <div className={styles.carousel}>
      { content }
    </div>
  )

  const carouselWidth = () : number => carouselRef?.current ? carouselRef?.current['clientWidth'] : 0
  const scrollContainerWidth = () : number => scrollContainerRef?.current ? scrollContainerRef?.current['clientWidth'] : 0
  const maxScrollAmount = () : number => scrollContainerWidth() - carouselWidth()

  // It is better if users still see a card that was shown before scrolling so
  // that they know that nothing was skipped. Hence we only step by 80% of the
  // full carousel width.
  const scrollStep = () : number => Math.round(0.8 * carouselWidth());

  const scroll = (direction: string) : void => {
    switch(direction) {
      case 'back': setScrollAmount((amount:number) : number => Math.min(amount + scrollStep(), 0)); break
      case 'forward': setScrollAmount((amount:number) : number => Math.max(amount - scrollStep(), -maxScrollAmount())); break
      default: return
    }
  }

  return (
    <div
      ref={carouselRef}
      className={`${styles.carousel} ${column ? styles.column : ''}`}
    >

      <div
        ref={scrollContainerRef}
        className={styles["scroll-container"]}
        style={{ '--scrollAmount': `${scrollAmount}px` } as React.CSSProperties}
      >
        { React.Children.map(children, wrapCard) }
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

export default Carousel