import requestExecuter from "@functions/RequestExecuter";
import AccountDetails from "@models/AccountDetails";
import { useEffect, useState } from "react";
import { Button, Col, Container, Form, InputGroup, Modal, Row, Stack } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function AccountInfo() {
    const navigation = useNavigate();
    const [isEditing, setIsEditing] = useState(false);
    const [showModal, setShowModal] = useState(false);


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

    const handleShowModal = () => setShowModal(true);
    const handleCloseModal = () => setShowModal(false);

    const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            const formData = new FormData();
            formData.append('file', file);
            formData.append('userId', accountDetails.uid);
            const headers = new Headers();
            headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
            const url = `http://localhost:8090/v1/Storage/PutProfileFile?userId=${accountDetails.uid}`;
            try {
                const response = await fetch(url, {
                    method: 'PUT',
                    headers: headers,
                    body: formData
                });
                if (response.ok) {
                    const data = await response.text();
                    setAccountDetails(prevDetails => ({ ...prevDetails, urlImage: data }));
                    handleCloseModal();
                }
            } catch (error) {
                console.error("Error uploading image:", error);
            }
        }
    };

    async function updateAccount(): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8070/v1/Account/UpdateUserData`;
        console.log("update")
        return fetch(url, {
            method: 'Patch', headers: headers, body: JSON.stringify({
                "uid": accountDetails?.uid,
                "name": accountDetails?.name,
                "surname": accountDetails?.surname,
                "lastName": accountDetails?.lastName,
                "userName": accountDetails?.userName,
            })
        });
    }

    const handleSubmit = async (event: React.SyntheticEvent<HTMLFormElement>) => {
        event.preventDefault();
        event.stopPropagation();
        
        await updateAccount();
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
                    <div style={modernCardStyle}>
                        <h1>Личный кабинет</h1>
                        <Row>
                            <Col>
                                <img style={imageStyle}
                                    src={accountDetails?.urlImage === "" ? "https://cdn-icons-png.flaticon.com/512/149/149452.png" : accountDetails?.urlImage}
                                    alt={accountDetails?.userName}
                                />
                                <div style={{ paddingTop: "20px" }}>
                                    <Button variant="primary" onClick={handleShowModal}>
                                        Загрузить новое фото
                                    </Button>
                                </div>
                            </Col>
                            <Col>
                                <Form onSubmit={handleSubmit}>
                                    <Stack direction="horizontal" gap={3}>
                                        <div className="w-100">
                                            <Form.Label>Имя пользователя</Form.Label>
                                            <InputGroup className="mb-3">
                                                <InputGroup.Text>@</InputGroup.Text>
                                                <Form.Control
                                                    disabled={!isEditing}
                                                    placeholder="Username"
                                                    value={accountDetails?.userName}
                                                    onChange={(e) => setAccountDetails(x => ({ ...x, userName: e.target.value }))}
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
                                                    onChange={(e) => setAccountDetails(x => ({ ...x, email: e.target.value }))}
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
                                                    onChange={(e) => setAccountDetails(x => ({ ...x, name: e.target.value }))}
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
                                                    onChange={(e) => setAccountDetails(x => ({ ...x, lastName: e.target.value }))}
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


                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>Загрузить новое фото</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group>
                                <Form.Label>Выберите файл</Form.Label>
                                <Form.Control type="file" onChange={handleFileChange} />
                            </Form.Group>

                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            Закрыть
                        </Button>
                    </Modal.Footer>
                </Modal>

            </Container >
        </>
    )
}

const modernCardStyle: React.CSSProperties = {
    border: "1px solid rgba(0, 0, 0, 0.125)",
    borderRadius: "10px",
    boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
    padding: "20px",
    background: "white",
    transition: "transform 0.2s",
};

const imageStyle: React.CSSProperties = {
    width: '250px',
    height: '250px',
    borderRadius: '50%',
    marginRight: '15px',
};
