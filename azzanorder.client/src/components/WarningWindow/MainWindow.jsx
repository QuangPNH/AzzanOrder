import React from "react";
import "./style.css";

const MainWindow = () => {
    return (
        <div className="box">
            <div id="warning-container" className="login-widget">
                <div className="overlap-group">
                    <div className="text-wrapper">WARNING</div>
                    <h2 className="phone-textbox">Are you sure you want to log out?</h2>
                    <button id="logoutbutton" className="button">
                        <button className="div" >Log out</button>
                    </button>
                    <button className="button-wrapper">
                        <button className="button-2">Cancel</button>
                    </button>
                </div>
            </div>
        </div>
    );
};
export default MainWindow;