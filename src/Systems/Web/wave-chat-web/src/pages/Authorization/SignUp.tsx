import {useNavigate } from 'react-router-dom'
import { Col, Container,Form,InputGroup, Row} from "react-bootstrap"
import { Eye,EyeSlashFill } from "react-bootstrap-icons"
import React from 'react';
export type AuthRequest = {
    name: String,
    lastName: String,
    surnName: String | null,
    
    email: String,
    password: String,
    username: String
};

export type data = {
    id: string | null,
    accessToken: string | null,
    refreshToken: string | null
}

export type AuthResponse = {
    data: data
    errorMessage: string
};

export default function SignInPage(){

    const navigate = useNavigate();
    if(localStorage.getItem('id') != null) navigate('/');
    
    const [showPassword, setShowPassword] = React.useState(false);
    const [errorValue, setError] = React.useState("");
    const [password,setPassword] = React.useState("");

    const signUpData = async(userData: AuthRequest): Promise<AuthResponse> => {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type','application/json');

        const url = `http://localhost:8010/v1/Authorization/SignUp`
        const response = await fetch(url,{method: 'Post',headers: headers,body: JSON.stringify({
            "name": userData.name,
            "surname": userData.surnName,
            "lastname": userData.lastName,
            "email": userData.email,
            "password": userData.password,
            "username": userData.username 
        })});
        return await response.json();
    }

    const handleSubmit = async (event:  React.SyntheticEvent<HTMLFormElement>) => {
        event.preventDefault();
        event.stopPropagation();
        
        const form = event.currentTarget;
        const formElements = form.elements as typeof form.elements & {
            email: {value: String},
            password: {value: String},
            username: {value: String},
            name: {value: String},
            lastname: {value: String},
            surname: {value: String | null}
        }

        const account: AuthRequest =  {
            email: formElements.email.value,
            password: formElements.password.value,
            username: formElements.username.value,
            name: formElements.name.value,
            lastName: formElements.lastname.value,
            surnName: formElements.surname.value
        };

        var response = await signUpData(account); console.log(response);
        if (response.errorMessage != ""){
            setError(response.errorMessage![0].toString());
            window.alert(response.errorMessage![0].toString());
        }
        else{
            localStorage.setItem("id",response.data.id!.toString());
            localStorage.setItem("accessToken",response.data.accessToken!);
            localStorage.setItem("refreshToken",response.data.refreshToken!);
            navigate('/');
        }
    }

    return <>
 <div style={{flex: 1}}>
        <Container fluid="md">
            <div style={{alignItems: 'center',display: 'flex',justifyContent: 'center',minHeight: '500px'}}>
                <div style = {{border: "1px solid black", borderRadius: "5px",padding: "20px"}}>
                    <h3>Регистрация</h3>
                    <Form onSubmit={handleSubmit} >
                        <Row>
                            <Col>
                                <Form.Group className="mb-3" >
                                    <Form.Label>Почта</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control type="email" name="email" id="email" placeholder="name@example.com" required />
                                    </InputGroup>

                                    <Form.Label>Пароль</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control  type={showPassword ? "text" : "password"} value={password} onChange={(e => setPassword(e.target.value))} name="password" id="passwordVisible" placeholder="Password" />
                                        <button className="btn btn-primary" style={{backgroundColor: "#242424"}} onClick={() => setShowPassword(!showPassword)}>
                                            {showPassword ? <EyeSlashFill color="white" size={20}/> : <Eye color="white" size={20}></Eye>}
                                        </button>

                                    </InputGroup>

                                    <Form.Label>Имя пользователя</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control type="text" name="username" id="username" required />
                                    </InputGroup>

                                </Form.Group>
                            </Col>
                            
                            <Col>
                                <Form.Group className="mb-3" >
                                    <Form.Label>Фамилия</Form.Label>
                                    <InputGroup hasValidation  >
                                        <Form.Control id="lastname" name="lastname"/>
                                    </InputGroup>

                                    <Form.Label>Имя</Form.Label>
                                    <InputGroup hasValidation>
                                        <Form.Control id="name" name="name" />
                                    </InputGroup>

                                    <Form.Label>Отчество</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control id="surname" name="surname"/>
                                    </InputGroup>
                                </Form.Group>

                            </Col>
                            <button className='btn btn-primary' type="submit" id="submBtn" >Зарегестрироваться</button>
                            
                        </Row>
                    </Form>
                </div>
            </div>
        </Container>    
    </div>
    </>
}