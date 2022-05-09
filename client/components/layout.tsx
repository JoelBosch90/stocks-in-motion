/**
 *  Component that creates a common page layout including a navigation header
 *  and a website footer. It can be used encapsulate the page's content like
 *  this, which will automatically add the common page layout:
 *    <Layout>
 *      <h1>Some content example title</h1>
 *      <p>Some content example text</p>
 *    </Layout>
 */

import Header from './layout/header'
import Footer from './layout/footer'

interface LayoutProps {
  children: JSX.Element | JSX.Element[]
  home?: boolean
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <Header />
      { children }
      <Footer />
    </>
  )
}

export default Layout