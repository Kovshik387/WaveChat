import {useNavigate } from "react-router-dom";

export default async function CheckToken(){
    const headers = new Headers();
    if (localStorage.getItem("refreshToken") === null) return;

    headers.set('Access-Control-Allow-Origin', '*');
    headers.set('Content-Type','application/json');
    headers.set("Authorization","Bearer "+ localStorage.getItem("refreshToken")!);
    console.log("refresh: " + localStorage.getItem("refreshToken"));
    const url = `http://localhost:8010/v1/Authorization/RefreshAccess?refreshToken=${localStorage.getItem("refreshToken")}`;
    const response = await fetch(url,{method: 'Get',headers: headers});
    if (response.status == 200){
        var result: AuthResponse = await response.json()
        console.log(result)
        if(result.errorMessage === ""){
            console.log('update')
            localStorage.setItem("accessToken",result.data.accessToken!);
            localStorage.setItem("refreshToken",result.data.refreshToken!);
        }
        else{
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("id");
            const navigate = useNavigate();
            navigate("/signIn");
        }
    }
    else{
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("id");
        const navigate = useNavigate();
        navigate("/signIn");
    }
}

export type data = {
    id: string | null,
    accessToken: string | null,
    refreshToken: string | null
}

export type AuthResponse = {
    data: data
    errorMessage: string
};