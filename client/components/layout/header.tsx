/**
 *  Component that creates a header for the common website layout.
 */

import Link from 'next/link'
import styles from './header.module.scss'

interface HeaderProps {}

const Header = ({}: HeaderProps) => {

  const pages = {
    "/": "Home",
    "/about": "About",
  }

  const links = Object.entries(pages).map(([url, name]) => {
    return (
      <Link href={ url }>
        { name }
      </Link>
    )
  })

  return (
    <header className={styles.header}>
      <nav>
        { links }
      </nav>
      <hr/>
    </header>
  )
}

export default Header