import CustomSearch from '../components/CustomSearch'
import CustomCardsList from '../components/CustomCardsList'

function QuizesPage() {
    return (
        <div className='container my-4'>
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-end">
                    <div className="search-wrapper">
                        <CustomSearch />
                    </div>
                </div>
            </div>
            <CustomCardsList className='row g-4'/>
        </div>
    )
}

export default QuizesPage