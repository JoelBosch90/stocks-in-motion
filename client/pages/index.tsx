import type { NextPage } from 'next'
import Head from 'next/head'
import Layout from '../components/layout'

const Home : NextPage = () => {
  return (
    <>
      <Head>
        <title>Home</title>
      </Head>
      <Layout>
        <h1>Home</h1>
        <p>Welcome to the website!</p>
      </Layout>
    </>
  )
}

export default Home
