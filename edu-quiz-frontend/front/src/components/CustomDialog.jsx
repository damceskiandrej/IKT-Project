import CustomButton from "./CustomButton"

function CustomDialog() {
    return (
        <div className="card p-4 m-4 shadow-sm" style={{ maxWidth: '500px' }}>
          <div className="card-body">
            <h4 className="card-title text-center mb-4">Дали сакаш да продожиш?</h4>
            <div className="d-flex justify-content-center">
                <div className="me-2">
                    <CustomButton btnText={"ДА"}/>
                </div>
                <CustomButton btnText={"НЕ"}/>
            </div>
          </div>
        </div>
    )
}

export default CustomDialog