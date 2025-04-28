import React from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Layout from './Layout'
import QuizesPage from './pages/quiz/QuizesPage'
import AboutUsPage from './pages/common/AboutUsPage'
import QuizQuestionsPage from './pages/quiz/QuizQuestionsPage'
import ResultPage from './pages/quiz/ResultPage'
import RegisterPage from './pages/authentication/RegisterPage'
import LoginPage from './pages/authentication/LoginPage'
import HomePage from './pages/common/HomePage'

function App() {
  return (
    <>
      <Router>  
      <Routes>
        <Route element={<Layout/>}>
          <Route path="/home" element={<HomePage/>} />
          <Route path="/" element={<HomePage/>} />
          <Route path="/result" element={<ResultPage/>} />
          <Route path="/login" element={<LoginPage/>} />
          <Route path="/register" element={<RegisterPage/>} />
          <Route path="/quizes" element={<QuizesPage/>} />
          <Route path="/about" element={<AboutUsPage/>} />
          <Route path="/profile" element={<AboutUsPage/>} />
          <Route path="/quiz/:id" element={<QuizQuestionsPage/>} />
        </Route>
      </Routes>
    </Router>
    </>
  )
}

export default App
