import { useEffect, useState } from "react";
import SideBarItem from "./SideBarItem"
import AccountChats from "@models/Chat";
import requestExecuter from "@functions/RequestExecuter";
import { Search } from "react-bootstrap-icons";
import SearchSideBar from "./SearchSideBar";

export interface LobbyProps {
    joinRoom: (userName: string) => void;
    closeConnection: () => void;
    setCurrentChatId: (chatId: string) => void;
}

export default function SideBar(lobby: LobbyProps) {
    const [accountChat, setAccountsChats] = useState<AccountChats[]>([]);

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
                else setAccountsChats(data);
            }
            catch (error) {
                console.log(error);
            }
        }
        fetchChat();
    }, []);
    return (
        <>
            <div className="col col-sm-3">
                <div style={sidebarStyle}>
                    <SearchSideBar />
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
    height: '85vh',
    overflowY: 'auto',
    padding: '20px',
};

const sideBar: React.CSSProperties = {
    backgroundColor: "#242424",
    alignItems: "center",
    paddingTop: "20px",
    border: "black 10px double",
    height: "500px",
    overflowY: "scroll"
}
