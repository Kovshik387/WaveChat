import requestExecuter from "@functions/RequestExecuter";
import AccountDetails from "@models/AccountDetails";
import { useEffect, useState } from "react";
import { Form, InputGroup, Button } from "react-bootstrap";

export interface SideBarProprs {
    setNewAccount: (value: AccountDetails[]) => void;
    setSearch: (value: string) => void;
    search: String;
}

export default function SearchSideBar({ setNewAccount, setSearch, search }: SideBarProprs) {
    useEffect(() => {

    }, []);
    const handleSubmit = async (event: React.SyntheticEvent<HTMLFormElement>) => {
        event.preventDefault();
        event.stopPropagation();
        try {
            async function GetNewChats(): Promise<Response | null> {
                const headers = new Headers();
                headers.set('Access-Control-Allow-Origin', '*');
                headers.set('Content-Type', 'application/json');
                headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
                const url = `http://localhost:8020/v1/Chat/GetAccountByUserName?userName=${search}&id=${localStorage.getItem("id")}`
                try {
                    return fetch(url, { method: "Get", headers: headers });
                }
                catch (error) { return null; }
            }
            const data = await requestExecuter<AccountDetails[]>(GetNewChats);
            if (!data) return;
            setNewAccount(data);
        }
        catch (error) {
            console.log(error);
        }
    }


    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
    };

    return (
        <>
            <Form onSubmit={handleSubmit}>
                <InputGroup className="mb-3 " data-bs-theme="dark">
                    <Form.Control
                        placeholder="@Пользователь"
                        value={search.toString()}
                        onChange={onChangeHandler}
                    />
                    <Button type="submit" variant="secondary">
                        Найти
                    </Button>
                </InputGroup>
            </Form>
        </>
    )
}

const sideBarSearch: React.CSSProperties = {
    display: "flex",
    alignItems: "center",
    marginBottom: "20px",
    height: "40px",
    backgroundColor: "#1E1E1E",
    borderRadius: "10px",
    transition: 'background-color 0.5s ease',
    width: "100%",
    color: '#bdc3c7'
}