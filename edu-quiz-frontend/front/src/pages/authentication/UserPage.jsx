import CustomButton from "../../components/CustomButton";
import UploadDocument from "@/components/UploadDocument.jsx";
import useUser from "@/hooks/useUser.js";
import {Role} from "@/enum/role.js";

function UserPage() {

    const user = useUser();

    debugger;
    const isProfessor = user && user.role === Role.PROFESSOR;

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            {isProfessor && (
                <>
                    <UploadDocument btnText={"Додади студенти"} apiEndpoint="http://localhost:5190/api/Import/ImportStudents" />
                    <UploadDocument btnText={"Додади квиз"} apiEndpoint="http://localhost:5190/api/Import/ImportQuiz" />
                </>
            )}

            <div className="form-group mb-3">
                <CustomButton btnText={"Твои квизови"} />
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Сите квизови"} />
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Добиени совети"} />
            </div>
        </div>
    );
}

export default UserPage