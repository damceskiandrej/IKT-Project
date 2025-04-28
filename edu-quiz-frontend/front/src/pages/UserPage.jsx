import CustomButton from "../components/CustomButton";

function UserPage() {
    return (
        <div className="container">
            <CustomButton btnText={"Твои квизови"}/>
            <CustomButton btnText={"Сите квизови"}/>
            <CustomButton btnText={"Добиени совети"}/>
        </div>
    )
}
export default UserPage