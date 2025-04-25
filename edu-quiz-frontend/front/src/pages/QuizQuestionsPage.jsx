import CustomQuestion from "../components/CustomQuestion";
import CustomAnswer from "../components/CustomAnswer";
import CustomButton from "../components/CustomButton";

function QuizQuestionsPage() {
    return (
        <div className="container mt-5">
            <CustomQuestion
                title="Агентски базирани системи и нивна примена"
                question="Која е главната предност на агентски базираните системи во сложени средини?"
            />
            <div className="row mt-5 g-3">
                <div className="col-md-6">
                    <CustomAnswer
                        letter="A"
                        text="Подобрена флексибилност и адаптација на агенти во динамични околини."
                    />
                </div>
                <div className="col-md-6">
                    <CustomAnswer
                        letter="B"
                        text="Намалена потреба од паралелна обработка на податоци."
                    />
                </div>
                <div className="col-md-6">
                    <CustomAnswer
                        letter="C"
                        text="Ограничена комуникација меѓу агенти за подобра контрола."
                    />
                </div>
                <div className="col-md-6">
                    <CustomAnswer
                        letter="D"
                        text="Централизирано управување со сите агенти во системот."
                    />
                </div>
            </div>
            <div className=" mt-4 d-flex justify-content-center">
                <CustomButton btnText={"СЛЕДНИ ПРАШАЊЕ"}/>
            </div>
        </div>
    )
}

export default QuizQuestionsPage