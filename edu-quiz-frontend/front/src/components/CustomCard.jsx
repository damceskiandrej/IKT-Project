import CustomButton from './CustomButton'

function CustomCard() {
    return (
        <div className="card border-0" style={{width: "25em"}}>
            <div className="card-body p-4">
                <h4 className="card-title">Агентски базирани системи и нивна примена</h4>
                <p className="card-text">Развој и симулација на автономни агенти во различни сложени средини.</p>
                <div className="d-flex justify-content-center">
                    <CustomButton btnText={"ПОЧНИ КВИЗ"}/>
                </div>
            </div>
        </div>
    )
}

export default CustomCard