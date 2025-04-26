import React from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Layout from './Layout'
import QuizesPage from './pages/QuizesPage'
import AboutUsPage from './pages/AboutUsPage'

function App() {
  return (
    <>
      <Router>  
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<QuizesPage/>} />
          <Route path="/home" element={<QuizesPage/>} />
          <Route path="/quizes" element={<QuizesPage/>} />
          <Route path="/about" element={<AboutUsPage/>} />
          <Route path="/profile" element={<AboutUsPage/>} />
        </Route>
      </Routes>
    </Router>
    </>
  )
}

export default App
