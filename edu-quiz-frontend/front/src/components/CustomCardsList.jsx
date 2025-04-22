import CustomCard from './CustomCard'

function CustomCardsList() {
    return (
        <div className="container my-4">
            <div className="row g-4">
                <div className="col-md-4">
                    <CustomCard/>
                </div>
                <div className="col-md-4">
                    <CustomCard/>
                </div>
                <div className="col-md-4">
                    <CustomCard/>
                </div>
                <div className="col-md-4">
                    <CustomCard/>
                </div>
                <div className="col-md-4">
                    <CustomCard/>
                </div>
                <div className="col-md-4">
                    <CustomCard/>
                </div>
            </div>
        </div>
    )
}

export default CustomCardsList 