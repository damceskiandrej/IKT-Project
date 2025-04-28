import CustomButton from "../components/CustomButton";

function RegisterPage() {
    return (
        <form>
            <div className="form-group">
                <label for="exampleInputEmail1">Name</label>
                <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email"/>              </div>
            <div className="form-group">
                <label for="exampleInputPassword1">Surname</label>
                <input type="password" className="form-control" id="exampleInputPassword1" placeholder="Password"/>
            </div>
            <div className="form-group">
                <label for="exampleInputEmail1">Email address</label>
                <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email"/>
            </div>
            <div className="form-group">
                <label for="exampleInputPassword1">Password</label>
                <input type="password" className="form-control" id="exampleInputPassword1" placeholder="Password"/>
            </div>
            <CustomButton btnText={"Логирај се"}/>
        </form>
    )
}

export default RegisterPage