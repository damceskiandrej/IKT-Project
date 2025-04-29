import CustomButton from "../../components/CustomButton";

function UserPage() {
    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <div className="form-group mb-3">
                <CustomButton btnText={"Твои квизови"}/>
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Сите квизови"}/>    
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Добиени совети"}/>
            </div>
        </div>
    )
}
export default UserPage