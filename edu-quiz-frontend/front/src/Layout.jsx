import CustomHeader from './components/CustomHeader'
import React from "react";
import {Outlet} from 'react-router-dom'

function Layout() {
    return (
        <>
            <CustomHeader />
            <Outlet />
        </>
    )
}

export default Layout