import React from 'react'
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'
import Layout from './Layout'
import QuizesPage from './pages/quiz/QuizesPage'
import AboutUsPage from './pages/common/AboutUsPage'
import QuizQuestionsPage from './pages/quiz/QuizQuestionsPage'
import ResultPage from './pages/quiz/ResultPage'
import RegisterPage from './pages/authentication/RegisterPage'
import LoginPage from './pages/authentication/LoginPage'
import HomePage from './pages/common/HomePage'
import UserPage from './pages/authentication/UserPage'
import ProtectedRoute from './components/ProtectedRoute'
import QuizQuestionReviewPage from './pages/quiz/QuizQuestionReviewPage'

function App() {
  return (
    <Router>
      <Routes>

        {/* Public routes */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* Routes within layout, now unprotected */}
        <Route element={<ProtectedRoute />}>
          <Route element={<Layout />}>
            <Route path="/" element={<HomePage />} />
            <Route path="/home" element={<HomePage />} />
            <Route path="/resultPage" element={<ResultPage />} />
            <Route path="/quizes" element={<QuizesPage />} />
            <Route path="/about" element={<AboutUsPage />} />
            <Route path="/profile" element={<UserPage />} />
            <Route path="/quiz/:id" element={<QuizQuestionsPage />} />
            <Route path="/reviewPage" element={<QuizQuestionReviewPage />} />
          </Route>
        </Route>
        {/* Fallback route */}
        <Route path="*" element={<Navigate to="/home" />} />
      </Routes>
    </Router>
  )
}

export default App
