/**
 *  This is a simple page that briefly explains the meaning and use cases of
 *  this application.
 */

import type { NextPage } from 'next'
import Head from 'next/head'
import Layout from '../components/layout'

const About : NextPage = () => {
  return (
    <>
      <Head>
        <title>About</title>
      </Head>
      <Layout>
        <h1>Title</h1>
        <p>Some text ...</p>
        <h2>Subtitle</h2>
        <p>Some more text ...</p>
      </Layout>
    </>
  )
}

export default About