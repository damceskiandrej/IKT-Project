import { useState, useEffect } from 'react';
import CustomSearch from '../components/CustomSearch';
import CustomCardsList from '../components/CustomCardsList';
import { getAllQuizzes } from '../api/quizApi';

function QuizesPage() {
    const [quizzes, setQuizzes] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchQuizzes = async () => {
            try {
                setIsLoading(true);
                const data = await getAllQuizzes();
                setQuizzes(data);
                console.log(data)
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };
        fetchQuizzes();
    }, []);

    if (error) {
        return <div className="text-center my-5">Error: {error}</div>;
    }
    return (
        <div className='container my-4'>
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-end">
                    <div className="search-wrapper">
                        <CustomSearch />
                    </div>
                </div>
            </div>
            <CustomCardsList quizzes={quizzes} className='row g-4'/>
        </div>
    )
}

export default QuizesPage;