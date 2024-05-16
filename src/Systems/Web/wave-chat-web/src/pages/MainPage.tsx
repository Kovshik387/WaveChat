import React from "react";

export default function MainPage(){
    const [image,setImage] = React.useState("");
    if (localStorage.getItem("id") !== null){
        
    }
    
    return <>
    <div style={{flex: 1}}>
        {
            localStorage.getItem("id") !== null ?
            <h1 style={CenterText}>WaveChat</h1>

            :
            
            <h2>Добро пожаловать</h2>
        }

    </div>
    </>
}

const CenterText : React.CSSProperties = {
    textAlign: 'center'
}