/**
 *  This is a simple page that briefly explains the reasons behind building this
 *  website.
 */

import type { NextPage } from 'next'
import { useRouter } from 'next/router'
import Head from 'next/head'
import Layout from '../../components/layout'
import Card from '../../components/card'


const Stock : NextPage = () => {
  
  const router = useRouter()
  const ticker = String(router?.query?.ticker)

  return (
    <>
      <Head>
        <title>{ ticker }</title>
      </Head>
      <Layout>
        <h1>{ ticker }</h1>
        <p>Some initial information ...</p>
        <Card name={ticker} />
      </Layout>
    </>
  )
}

export default Stock