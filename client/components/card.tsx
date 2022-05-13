/**
 *  Component that creates a single card representation.
 */

import React, { FunctionComponent } from 'react';
import styles from './card.module.scss'

interface CardProps {
  name: string,
  onClick?: (event: React.MouseEvent<HTMLElement>) => void,
}

const Card : FunctionComponent<CardProps> = ({ name, onClick }) => {
  return (
    <div
      className={`${styles.card} ${onClick ? styles.clickable : ''}`}
      onClick={onClick}
    >
      <h4>{ name }</h4>
    </div>
  )
}

export default Card