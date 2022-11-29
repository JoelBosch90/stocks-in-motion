/**
 *  This is a simple page that briefly explains the reasons behind building this
 *  website.
 */

import type { NextPage } from 'next'
import Head from 'next/head'
import Layout from '../components/Layout/Layout'

const About : NextPage = () => {
  return (
    <>
      <Head>
        <title>About</title>
      </Head>
      <Layout>
        <h1>About</h1>
        <p>Some text ...</p>
        <h2>Technology</h2>
        <p>Some more text ...</p>
        <h2>Content</h2>
        <p>Some more text ...</p>
      </Layout>
    </>
  )
}

export default About