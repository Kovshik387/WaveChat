import { useEffect, useState } from "react";
import SideBarItem from "./SideBarItem";
import AccountChats from "@models/Chat";
import SearchSideBar from "./SearchSideBar";
import SideBarSearchItem from "./SideBarSearchItem";
import AccountDetails from "@models/AccountDetails";
import { MessageInfo } from "@models/MessageInfo";
import requestExecuter from "@functions/RequestExecuter";

export interface LobbyProps {
    connection: any;
    joinRoom: (userName: string) => void;
    closeConnection: () => void;
    setCurrentChatId: (chatId: string) => void;
    messages: { [key: string]: MessageInfo[] };
    setMessages: React.Dispatch<React.SetStateAction<{ [key: string]: MessageInfo[] }>>;
}

export default function SideBar(lobby: LobbyProps) {
    const [accountChat, setAccountsChats] = useState<AccountChats[]>([]);
    const [newAccountChat, setNewAccountsChats] = useState<AccountDetails[]>([]);
    const [search, setSearch] = useState("");

    async function getMessages(chatId: string): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8020/v1/Chat/GetChatMessage?idChat=${chatId}`;
        try {
            const response = await fetch(url, { method: 'GET', headers: headers });
            if (!response.ok) {
                console.log("Network response was not ok");
                return null;
            }
            return response;
        } catch (error) {
            console.error("Failed to fetch messages:", error);
            return null;
        }
    }

    async function GetChats(): Promise<Response | null> {
        const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8020/v1/Chat/GetUserChats?idUser=${localStorage.getItem("id")}`;
        try {
            const response = await fetch(url, { method: 'GET', headers: headers });
            return response;
        } catch (error) {
            console.error("Failed to fetch chats:", error);
            return null;
        }
    }

    async function fetchChats() {
        try {
            const response = await GetChats();
            if (response !== null) {
                const data = await response.json() as AccountChats[];
                if (data.length > 0) {
                    setAccountsChats(data.reverse());
                } else {
                    console.log("No chats found");
                }
            } else {
                console.log("Failed to fetch chats");
            }
        } catch (error) {
            console.error("Error fetching chats:", error);
        }
    }

    useEffect(() => {
        fetchChats();
    }, []);

    useEffect(() => {
        async function fetchMessages() {
            for (const chat of accountChat) {
                const response = await requestExecuter<MessageInfo[]>(() => getMessages(chat.uid));
                if (response !== null) {
                    const messagesData = response;
                    lobby.setMessages(prevMessages => ({ ...prevMessages, [chat.uid]: messagesData }));
                    console.log(`Messages for chat ${chat.uid}:`, messagesData);
                }
            }
        }

        if (accountChat.length > 0) {
            fetchMessages();
        }
    }, [accountChat]);

    useEffect(() => {
        if (lobby.connection) {
            lobby.connection.on("NewMessageNotification", (message: MessageInfo) => {
                console.log("New message notification:", message);
                // Обновление списка чатов
                fetchChats();
            });

            return () => {
                lobby.connection.off("NewMessageNotification");
            };
        }
    }, [lobby.connection]);

    return (
        <>
            <div style={{ padding: "20px" }}>
                <div style={sidebarStyle}>
                    <SearchSideBar search={search} setSearch={setSearch} setNewAccount={setNewAccountsChats} />
                    {newAccountChat.length > 0 && (
                        <div>
                            {newAccountChat.map((chat, index) => (
                                <SideBarSearchItem
                                    setSearch={setSearch}
                                    setNewAccount={setNewAccountsChats}
                                    chat={accountChat}
                                    newChat={chat}
                                    key={index}
                                    joinRoom={lobby.joinRoom}
                                    closeConnection={lobby.closeConnection}
                                    setCurrentChatId={lobby.setCurrentChatId}
                                />
                            ))}
                            <hr />
                        </div>
                    )}
                    {accountChat.map((chat, index) => (
                        <SideBarItem
                            chat={chat}
                            key={index}
                            joinRoom={lobby.joinRoom}
                            closeConnection={lobby.closeConnection}
                            setCurrentChatId={lobby.setCurrentChatId}
                            messages={lobby.messages[chat.uid]}
                        />
                    ))}
                </div>
            </div>
        </>
    );
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
    boxShadow: "10px 4px 8px rgba(108, 122, 137, 0.5)",
};