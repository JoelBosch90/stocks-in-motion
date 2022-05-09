/**
 *  Component that creates a header for the common website layout.
 */

import Link from 'next/link'
import styles from './header.module.scss'

interface HeaderProps {}

const Header = ({}: HeaderProps) => {
  return (
    <header className={styles.header}>
      <nav>
        <Link href="/">
          Home
        </Link>
        <Link href="/about">
          About
        </Link>
      </nav>
      <hr/>
    </header>
  )
}

export default Header