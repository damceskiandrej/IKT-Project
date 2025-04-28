import CustomButton from "../../components/CustomButton";

function RegisterPage() {
    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <form style={{ width: '100%', maxWidth: '400px' }}>
                <div className="form-group mb-3">
                    <label htmlFor="name">Name</label>
                    <input type="text" className="form-control" id="name" placeholder="Enter your name" />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="surname">Surname</label>
                    <input type="text" className="form-control" id="surname" placeholder="Enter your surname" />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="email">Email address</label>
                    <input type="email" className="form-control" id="email" placeholder="Enter email" />
                </div>
                <div className="form-group mb-4">
                    <label htmlFor="password">Password</label>
                    <input type="password" className="form-control" id="password" placeholder="Password" />
                </div>
                <CustomButton btnText={"Регистрирај се"} />
            </form>
        </div>
    );
}

export default RegisterPage;
