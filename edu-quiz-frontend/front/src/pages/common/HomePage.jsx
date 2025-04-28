import CustomButton from "../../components/CustomButton";

function HomePage() {
    return (
        <div className="container d-flex flex-column justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <div className="mb-3">
                <CustomButton btnText={"Login"} />
            </div>
            <div>
                <CustomButton btnText={"Register"} />
            </div>
        </div>
    )
}

export default HomePage;
