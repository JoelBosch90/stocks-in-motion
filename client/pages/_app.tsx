/**
 *  This adds a global component to the application that can be used to declare
 *  properties that will be shared by all pages in the application. For example,
 *  it makes sure that all pages have access to global styling.
 */

import '../styles/globals.scss'
import type { AppProps } from 'next/app'

function App({ Component, pageProps }: AppProps) {
  return <Component {...pageProps} />
}

export default App
