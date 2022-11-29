/**
 *  This is a simple page that briefly explains the reasons behind building this
 *  website.
 */

import type { NextPage } from 'next'
import { useRouter } from 'next/router'
import Head from 'next/head'
import Layout from '../../components/Layout/Layout'
import Card from '../../components/Card/Card'
import StockChart from '../../components/StockChart/StockChart'


const Stock : NextPage = () => {
  
  const router = useRouter()
  const ticker = router?.query?.ticker && String(router?.query?.ticker);

  return (
    <>
      <Head>
        <title>{ ticker }</title>
      </Head>
      <Layout>
        <h1>{ ticker }</h1>
        <p>Some initial information ...</p>
        {ticker && <Card name={ticker} />}
        {ticker && <StockChart ticker={ticker} />}
      </Layout>
    </>
  )
}

export default Stock