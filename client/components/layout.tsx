/**
 *  Component that creates a common page layout including a navigation header
 *  and a website footer. It can be used encapsulate the page's content like
 *  this, which will automatically add the common page layout:
 *    <Layout>
 *      <h1>Some content example title</h1>
 *      <p>Some content example text</p>
 *    </Layout>
 */

import React, { FunctionComponent } from 'react';
import Header from './layout/header'
import Footer from './layout/footer'
import styles from './layout.module.scss'

interface LayoutProps {
  children: JSX.Element | JSX.Element[]
}

const Layout : FunctionComponent<LayoutProps> = ({ children }) => {
  return (
    <div className={styles.layout}>
      <Header className={styles.header} />
      <main>
        { children }
      </main>
      <Footer className={styles.footer} />
    </div>
  )
}

export default Layout