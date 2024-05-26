import requestExecuter from "@functions/RequestExecuter";
import AccountDetails from "@models/AccountDetails";
import { useEffect, useState } from "react";
import { Col, Container, Form, InputGroup, Row, Stack } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function AccountInfo() {
    const navigation = useNavigate();
    const [isEditing, setIsEditing] = useState(false);
    const [accountDetails, setAccountDetails] = useState<AccountDetails>({
        uid: '',
        userName: '',
        name: '',
        surname: '',
        lastName: '',
        email: '',
        urlImage: '',
        role: ''
    });

    async function updateAccount(): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8070/v1/Account/UpdateUserData}`;
        return await fetch(url, {
            method: 'Patch', headers: headers, body: JSON.stringify({
                "uid": accountDetails?.uid,
                "name": accountDetails?.name,
                "surname": accountDetails?.surname,
                "lastName": accountDetails?.lastName,
                "userName": accountDetails?.userName,
                "urlImage": ""
            })
        });
    }

    async function getAccount(): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8070/v1/Account/GetAccountDetailsById?id=${localStorage.getItem("id")}`;
        try {
            return fetch(url, { method: 'Get', headers: headers });
        }
        catch (error) { return null; }
    }

    const handleEditToggle = () => {
        setIsEditing(!isEditing);
    };

    const handleSave = () => {
        setIsEditing(false);
    };

    useEffect(() => {
        async function fetchAccount() {
            try {
                const data = await requestExecuter<AccountDetails>(getAccount);
                if (data === null) {
                    console.log("Данные не получены")
                }
                else setAccountDetails(data);
                console.log(data);
            }
            catch (error) {
                console.log(error);

            }
        }

        if (localStorage.getItem("id") === null) {
            navigation("/signIn");
        }
        fetchAccount();


    }, [])
    return (
        <>
            <Container fluid="md">
                <div style={{ alignItems: 'center', display: 'flex', justifyContent: 'center', minHeight: '500px' }}>
                    <div style={{ border: "1px solid black", borderRadius: "5px", padding: "20px" }}>
                        <h1>Личный кабинет</h1>
                        <Row>
                            <Col>
                                <img style={imageStyle}
                                    src={accountDetails?.urlImage === "" ? "https://cdn-icons-png.flaticon.com/512/149/149452.png" : accountDetails?.urlImage}
                                    alt={accountDetails?.userName}
                                />
                            </Col>
                            <Col>
                                <Form>
                                    <Stack direction="horizontal" gap={3}>
                                        <div className="w-100">
                                            <Form.Label>Имя пользователя</Form.Label>
                                            <InputGroup className="mb-3">
                                                <InputGroup.Text>@</InputGroup.Text>
                                                <Form.Control
                                                    disabled={!isEditing}
                                                    placeholder="Username"
                                                    value={accountDetails?.userName}
                                                />
                                            </InputGroup>
                                        </div>

                                        <div className="w-100">
                                            <Form.Label>Почта</Form.Label>
                                            <InputGroup className="mb-3">
                                                <Form.Control
                                                    disabled={!isEditing}
                                                    placeholder="Email"
                                                    value={accountDetails?.email.toLowerCase()}
                                                />
                                            </InputGroup>
                                        </div>
                                    </Stack>

                                    <Stack direction="horizontal" gap={3}>
                                        <div className="w-100">
                                            <Form.Label>Имя</Form.Label>
                                            <InputGroup className="mb-3">
                                                <Form.Control
                                                    disabled={!isEditing}
                                                    placeholder="Фамилия"
                                                    value={accountDetails?.name}
                                                />
                                            </InputGroup>
                                        </div>
                                        <div className="w-100">
                                            <Form.Label>Фамилия</Form.Label>
                                            <InputGroup className="mb-3">
                                                <Form.Control
                                                    disabled={!isEditing}
                                                    placeholder="Фамилия"
                                                    value={accountDetails?.lastName}
                                                />
                                            </InputGroup>
                                        </div>
                                    </Stack>

                                    <Stack direction="horizontal" gap={3}>
                                        <div className="w-100">
                                            <Form.Label>Должность</Form.Label>
                                            <InputGroup className="mb-3">
                                                <Form.Control
                                                    disabled
                                                    value={accountDetails?.role}
                                                />
                                            </InputGroup>
                                        </div>
                                    </Stack>

                                    <div style={{ paddingTop: "10px" }}>
                                        {isEditing ? (
                                            <button className={"btn btn-primary"} onClick={handleSave}>
                                                Сохранить
                                            </button>
                                        ) : (
                                            <button className={"btn btn-primary"} onClick={handleEditToggle}>
                                                Изменить данные
                                            </button>
                                        )}
                                    </div>

                                </Form>
                            </Col>
                        </Row>

                    </div>
                </div>
            </Container >
        </>
    )
}

const imageStyle: React.CSSProperties = {
    width: '250px',
    height: '250px',
    borderRadius: '50%',
    marginRight: '15px',
};
