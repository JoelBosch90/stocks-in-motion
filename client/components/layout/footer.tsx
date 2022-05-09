/**
 *  Component that creates a footer for the common website layout.
 */

import styles from './footer.module.scss'

interface FooterProps {
  className: string,
}

const Footer = ({ className }: FooterProps) => {
  return (
    <footer className={`${className} ${styles.footer}`}>
      <small>
        Copyright © 2022 Bosch. All rights reserved.
      </small>
    </footer>
  )
}

export default Footer