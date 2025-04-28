import { useState, useEffect } from 'react';
import CustomSearch from '../components/CustomSearch';
import CustomCardsList from '../components/CustomCardsList';
import { getAllQuizzes } from '../api/quizApi';

function QuizesPage() {
    const [quizzes, setQuizzes] = useState([]);
    const [filteredQuizzes, setFilteredQuizzes] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState("");

    useEffect(() => {
        const fetchQuizzes = async () => {
            try {
                setIsLoading(true);
                const data = await getAllQuizzes();
                setQuizzes(data);
                setFilteredQuizzes(data); 
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };
        fetchQuizzes();
    }, []);

    const handleSearchChange = (term) => {
        setSearchTerm(term);

        const filtered = quizzes.filter((quiz) =>
            quiz.category?.toLowerCase().includes(term.toLowerCase())
        );
        setFilteredQuizzes(filtered);
    };

    if (error) {
        return <div className="text-center my-5">Error: {error}</div>;
    }

    return (
        <div className='container my-4'>
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-end">
                    <div className="search-wrapper">
                        <CustomSearch searchTerm={searchTerm} onSearchChange={handleSearchChange} />
                    </div>
                </div>
            </div>
            <CustomCardsList quizzes={filteredQuizzes} className='row g-4' />
        </div>
    )
}

export default QuizesPage;
