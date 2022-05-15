/**
 *  This is a simple page that briefly explains the reasons behind building this
 *  website.
 */

import type { NextPage } from 'next'
import { useRouter } from 'next/router';
import Head from 'next/head'
import Layout from '../components/layout'
import Slider from '../components/slider'
import Card from '../components/card'

const Stocks : NextPage = () => {

  const router = useRouter()

  // Create some temporary example elements.
  const cards = ['AAPL', 'MSFT', 'AMZN', 'GOOGL', 'GOOG', 'TSLA', 'BRK.B', 'JNJ', 'UNH', 'FD', 'NVDA', 'XOM', 'PG', 'JPM', 'V', 'CVX', 'HD', 'PFE', 'MA', 'ABBV']
    .map((name:string) : JSX.Element => <Card name={name} key={name} onClick={() => router.push(`/stocks/${name}`)} />)

  return (
    <>
      <Head>
        <title>Stocks</title>
      </Head>
      <Layout>
        <h1>Stocks</h1>
        <p>Some introduction text ...</p>
        
        <section>
          <h3>Favorites</h3>
          <Slider>{ cards }</Slider>
        </section>        

        <section>
          <h3>S&P 500</h3>
          <Slider>{ cards }</Slider>
        </section>
      </Layout>
    </>
  )
}

export default Stocks