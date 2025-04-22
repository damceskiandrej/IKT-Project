import CustomCardsList from '../components/CustomCardsList'

function MyQuizesPage() {
    return (
        <div className='container my-4'>
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-center">
                    <h3 style={{color: "rgba(60, 141, 123, 1)"}}>ТВОИ КВИЗОВИ</h3>
                </div>
            </div>
            <CustomCardsList className='row g-4'/>
        </div>
    )
}

export default MyQuizesPage