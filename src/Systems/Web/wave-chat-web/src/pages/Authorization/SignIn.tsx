import { Button, Container,Form, InputGroup} from "react-bootstrap"
import { Eye,EyeSlashFill } from "react-bootstrap-icons"
import {useNavigate } from 'react-router-dom'
import React, { useState } from "react"
import { useDispatch } from "react-redux";
import { login } from "./../../stores/accountSlice";

export type AuthAccount = {
    email: string,
    password: string
};

export type data = {
    id: string | null,
    accessToken: string | null,
    refreshToken: string | null,
    name: string | null
}

export type AuthResponse = {
    data: data
    errorMessage: string
};

export default function SignInPage(){
    const [userName,setUserName] = useState("");
    const dispatch = useDispatch();

    const navigate = useNavigate();

    const [showPassword, setShowPassword] = React.useState(false);
    const [errorEmail, setErrorEmail] = React.useState("");
    const [errorPassword, setErrorPassword] = React.useState("");
    const [email,setEmail] = React.useState("");
    const [password,setPassword] = React.useState("");

    const handleSubmit = async (event:  React.SyntheticEvent<HTMLFormElement>) => {
        event.preventDefault();
        event.stopPropagation();
        
        const account: AuthAccount =  {email: email,password: password};
        const data = await signIn(account);

        if(data.errorMessage != ""){
            console.log(data.errorMessage);
            if(data.errorMessage.match("password")){
                setErrorPassword(data.errorMessage);
            }
            else setErrorEmail(data.errorMessage);
        }else{

            localStorage.setItem("id",data.data.id!.toString());
            localStorage.setItem("accessToken",data.data.accessToken!);
            localStorage.setItem("refreshToken",data.data.refreshToken!);
            localStorage.setItem("name",data.data.name!);
            localStorage.removeItem("idChat");
            setUserName(data.data.name!);
            dispatch(login(userName))
            navigate('/');
            //window.location.reload()
        }
    }

    async function signIn(account: AuthAccount): Promise<AuthResponse> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        const url = `http://localhost:8010/v1/Authorization/SignIn?email=${account.email}&password=${account.password}`
        const response = await fetch(url,{method: 'GET',headers: headers});
        return await response.json();
    }

    if(localStorage.getItem('id') != null) navigate('/');

    return <>
    <div style={{flex: 1}}>
        <Container fluid="md">
            <div style={{alignItems: 'center',display: 'flex',justifyContent: 'center',minHeight: '500px'}}>
                <div style = {{border: "1px solid black", borderRadius: "5px",padding: "20px"}}>
                    <h3>Авторизация</h3>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group className="mb-3" controlId="email">
                            <Form.Label>Почта</Form.Label>
                            <InputGroup hasValidation>
                                <Form.Control isInvalid={errorEmail != ""} type="email" name="email" value={email} onChange={(e => setEmail(e.target.value))} placeholder="name@example.com" required />
                                <Form.Control.Feedback type="invalid">
                                    {errorEmail}
                                </Form.Control.Feedback>
                            </InputGroup>
                        </Form.Group>
                        <Form.Group className="mb-3" id="userPassword">
                            <Form.Label>Пароль</Form.Label>
                            <InputGroup hasValidation>
                                <Form.Control isInvalid = {errorPassword != ""} type={showPassword ? "text" : "password"} value={password} onChange={(e => setPassword(e.target.value))} name="password" id="passwordVisible" placeholder="Password" />
                                <Button style={{backgroundColor: "#242424"}} variant="contained" onClick={() => setShowPassword(!showPassword)}>
                                    {showPassword ? <EyeSlashFill color="white" size={20}/> : <Eye color="white" size={20}></Eye>}
                                </Button>
                                <Form.Control.Feedback type="invalid">
                                    {errorPassword}
                                </Form.Control.Feedback>
                            </InputGroup>
                            <Form.Label>
                                <a className="justify-content-end" href="/ForgotPassword">Забыли пароль?</a>
                            </Form.Label> 
                        </Form.Group>
                        <Button className="btn btn-primary" variant="contained" type="submit" >Войти</Button>
                    </Form>
                </div>
            </div>
        </Container>    
    </div>
    </>
}