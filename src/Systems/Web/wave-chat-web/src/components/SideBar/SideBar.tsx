import { useEffect, useState } from "react";
import SideBarItem from "./SideBarItem"
import AccountChats from "@models/Chat";
import requestExecuter from "@functions/RequestExecuter";
import SearchSideBar from "./SearchSideBar";
import SideBarSearchItem from "./SideBarSearchItem";
import AccountDetails from "@models/AccountDetails";

export interface LobbyProps {
    joinRoom: (userName: string) => void;
    closeConnection: () => void;
    setCurrentChatId: (chatId: string) => void;
}

export default function SideBar(lobby: LobbyProps) {
    const [accountChat, setAccountsChats] = useState<AccountChats[]>([]);
    const [newAccountChat, setNewAccountsChats] = useState<AccountDetails[]>([]);
    const [search, setSearch] = useState("");

    async function GetChats(): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8020/v1/Chat/GetUserChats?idUser=${localStorage.getItem("id")}`;
        try {
            return fetch(url, { method: 'Get', headers: headers });
        }
        catch (error) { return null; }
    }

    useEffect(() => {
        async function fetchChat() {
            try {
                const data = await requestExecuter<AccountChats[]>(GetChats);
                if (data === null) {
                    console.log("Данные не получены")
                }
                else setAccountsChats(data.reverse());
            }
            catch (error) {
                console.log(error);
            }
        }
        if (accountChat.length == 0) fetchChat();
    }, []);
    return (
        <>
            <div className="col col-sm-3" style={{ padding: "20px" }}>
                <div style={sidebarStyle}>
                    <SearchSideBar search={search} setSearch={setSearch} setNewAccount={setNewAccountsChats} />
                    {
                        (newAccountChat
                            ?
                            <div>
                                {
                                    newAccountChat.map((chat, index) => (
                                        <SideBarSearchItem setSearch={setSearch} setNewAccount={setNewAccountsChats} chat={accountChat} newChat={chat} key={index} joinRoom={lobby.joinRoom} closeConnection={lobby.closeConnection} setCurrentChatId={lobby.setCurrentChatId} />
                                    ))
                                }
                                <hr></hr>
                            </div>

                            :

                            <div></div>

                        )
                    }

                    {
                        accountChat.map((chat, index) => (
                            <SideBarItem chat={chat} key={index} joinRoom={lobby.joinRoom} closeConnection={lobby.closeConnection} setCurrentChatId={lobby.setCurrentChatId} />
                        ))
                    }
                </div>
            </div>
        </>
    )
}

const sidebarStyle: React.CSSProperties = {
    backgroundColor: '#2D2D2D',
    color: 'white',
    height: '80vh',
    overflowY: 'auto',
    padding: '20px',
    border: '2px solid #000',
    borderRadius: '10px',
    transition: 'background-color 0.3s, color 0.3s',
    boxShadow: '0 2px 4px rgba(218, 223, 225, 0.7)',
};
