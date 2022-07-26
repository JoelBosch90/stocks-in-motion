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
import { useEffect, useState } from 'react';

const Stocks : NextPage = () => {

  const router = useRouter()
  const [tickers, setTickers] = useState([])

  // Get the tickers from the API.
  useEffect(() => {
    fetch('/api/stocks')
      .then(response => response.json())
      .then(setTickers)
  }, [setTickers])

  // Create a card for each ticker.
  const cards = tickers.map((name:string) : JSX.Element =>
    <Card
      name={name}
      key={name}
      onClick={() => router.push(`/stocks/${name}`)}
    />
  )

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