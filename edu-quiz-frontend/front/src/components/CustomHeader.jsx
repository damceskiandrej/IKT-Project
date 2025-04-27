function CustomHeader() {
    return (
        <header className="bg-light py-2">
            <div className="container d-flex justify-content-between align-items-center">
                <a href="/home" className="d-flex align-items-center text-decoration-none">
                    <img src="../../img/logo.png" style={{width: "85px"}}/>
                </a>

                <nav>
                    <ul className="d-flex list-unstyled mb-0">
                        <li className="ms-4"><a href="/home" className="text-dark text-decoration-none">ПОЧЕТНА</a></li>
                        <li className="ms-4"><a href="/about" className="text-dark text-decoration-none">ЗА НАС</a></li>
                        <li className="ms-4"><a href="/quizes" className="text-dark text-decoration-none">КВИЗОВИ</a></li>
                        <li className="ms-4"><a href="/profile" className="text-dark text-decoration-none">ПРОФИЛ</a></li>
                    </ul>
                </nav>
            </div>
        </header>
    )
}

export default CustomHeader