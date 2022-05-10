/**
 *  This adds a global component to the application that can be used to declare
 *  properties that will be shared by all pages in the application. For example,
 *  it makes sure that all pages have access to global styling.
 */

import type { AppProps } from 'next/app'
import Head from 'next/head'
import '../styles/globals.scss'

function App({ Component, pageProps }: AppProps) {
  return (
    <>
      <Head>
        <title>Stocks in Motion</title>
        <meta name="description" content="Yet another website that lets you analyze stocks" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Component {...pageProps} />
    </>
  )
}

export default App
