/**
 *  Component that creates a footer for the common website layout.
 */

import React, { FunctionComponent } from 'react';
import styles from './footer.module.scss'

interface FooterProps {
  className: string,
}

const Footer : FunctionComponent<FooterProps> = ({ className }) => {
  return (
    <footer className={`${className} ${styles.footer}`}>
      <small>
        Copyright © 2022 Bosch & Bosch. All rights reserved.
      </small>
    </footer>
  )
}

export default Footer