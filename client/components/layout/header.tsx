/**
 *  Component that creates a header for the common website layout.
 */

import React, { FunctionComponent } from 'react';
import Link from 'next/link'
import styles from './header.module.scss'

interface HeaderProps {
  className: string,
}

const Header : FunctionComponent<HeaderProps> = ({ className }) => {

  const pages = {
    "/": "Home",
    "/stocks": "Stocks",
    "/features": "Features",
    "/about": "About",
  }

  const links = Object.entries(pages).map(([url, name]) => {
    return (
      <Link
        key={ url }
        href={ url }
      >
        { name }
      </Link>
    )
  })

  return (
    <header className={`${className} ${styles.header}`}>
      <nav>
        { links }
      </nav>
      <hr/>
    </header>
  )
}

export default Header