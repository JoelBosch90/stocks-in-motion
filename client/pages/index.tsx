/**
 *  This is the website's homepage. It should give a quick introduction to the
 *  website and provide ways for the user to interact with the core elements of
 *  the website very quickly.
 */

import type { NextPage } from 'next'
import Head from 'next/head'
import Layout from '../components/Layout/Layout'

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
