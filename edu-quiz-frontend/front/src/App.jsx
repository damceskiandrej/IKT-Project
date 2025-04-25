import './App.css'
import AboutUsPage from './pages/AboutUsPage'
import MyQuizesPage from './pages/MyQuizesPage'
import QuizesPage from './pages/QuizesPage'
import QAPage from './pages/QAPage'
import ResultPage from './pages/ResultPage'
import QuizQuestionsPage from './pages/QuizQuestionsPage'

function App() {
  return (
    <>
      <QuizQuestionsPage/>
      <ResultPage score={69}/>
      <QAPage/>
      <AboutUsPage/>
      <MyQuizesPage/>
      <QuizesPage/>
    </>
  )
}

export default App
