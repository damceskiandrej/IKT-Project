import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom'; 
import CustomCardsList from '../../components/CustomCardsList';
import { getQuizzesByUser, getQuizResultByUserAndQuizId } from '../../api/quizApi'; 
import useUser from '../../hooks/useUser';
import { useLocation } from 'react-router-dom';
import CustomButton from '../../components/CustomButton';
import { div } from 'three/tsl';

function QuizResultPage() {
    const [quizzes, setQuizzes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const user = useUser();
    const navigate = useNavigate();
    const userId = user ? user.userId : "";
    const location = useLocation();
    const {submission} = location.state || {};

    if(!submission) return <div>No Result data provided</div>

    const {quiz, results} = submission
    console.log("submission", submission)
    // console.log("quiz", quiz)
    // console.log("results", results)
    
    return (
        <>

        </>
    )
}


export default QuizResultPage;
