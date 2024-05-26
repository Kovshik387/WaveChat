import ChatRoom from "@components/Rooms/ChatRoom";
import SideBar from "@components/SideBar/SideBar";
import requestExecuter from "@functions/RequestExecuter";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Account from "@models/Account";
import { MessageInfo } from "@models/MessageInfo";
import React, { useEffect, useState } from "react";

async function getMessages() {
    const headers = new Headers();
    headers.set('Access-Control-Allow-Origin', '*');
    headers.set('Content-Type', 'application/json');
    headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
    const url = `http://localhost:8020/v1/Chat/GetChatMessage?idChat=${localStorage.getItem("idChat")}`;
    try {
        const response = await fetch(url, { method: 'Get', headers: headers });
        if (!response.ok) {
            console.log("Ответ сети был не ок");
        }
        return response;
    }
    catch (error) { return null; }
}

export default function MainPage() {
    const [connection, setConnection] = useState<any>(null);
    const [messages, setMessages] = useState<MessageInfo[]>([]);
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [currentChatId, setCurrentChatId] = useState<string | null>(null);
    useEffect(() => {
        if (currentChatId){
            async function fetchMessages() {
                const response = await requestExecuter<MessageInfo[]>(getMessages);
                if (response !== null) setMessages(response);
            }
            fetchMessages();
        }
    },[currentChatId]);

    const joinRoom = async (chatId: string) => {
        if (connection) {
            await connection.stop();
        }

        try {
            const connection = new HubConnectionBuilder()
                .withUrl("http://localhost:8020/chat")
                .configureLogging(LogLevel.Information)
                .build();
                connection.on("ReceiveMessage", (message: MessageInfo) => {
                setMessages((messages) => [...messages, message]);
            });

            connection.onclose((e) => {
                setConnection(undefined);
                setMessages([]);
                setAccounts([]);
            });

            await connection.start();
            await connection.invoke("JoinChat", chatId);
            setConnection(connection);

        } catch (e) {
            console.log(e);
        }
    };

    const sendMessage = async (userId: string, content: string, chatId: string, username: string) => {
        try {
            await connection.invoke("SendMessage", userId, content, chatId, username);
        } catch (e) {
            console.error(e);
        }
    };

    const closeConnection = async () => {
        try {
            await connection.stop();
            setConnection(null);
            setMessages([]);
            setAccounts([]);
        } catch (e) {
            console.log(e);
        }
    };

    return <>
        <div style={{ flex: 1 }}>
            {
                localStorage.getItem("id") !== null ?
                    <div className="row">
                        <SideBar closeConnection={closeConnection} joinRoom={joinRoom} setCurrentChatId={setCurrentChatId} />
                        <div className="col col-sm-9">
                            {!connection ?
                                (
                                    <h1 style={{textAlign:"center"}}></h1>
                                )
                                :
                                (
                                    <ChatRoom
                                        messages={messages}
                                        sendMessage={sendMessage}
                                        account={[]}
                                        closeConnection={function (): void {
                                            throw new Error("Function not implemented.");
                                        }} />
                                )}
                        </div>
                    </div>

                    :

                    <div style={CenterText}>
                        <h1>WaveChat</h1>
                        <h2>Добро пожаловать</h2>
                    </div>
            }
        </div>
    </>
}

const CenterText: React.CSSProperties = {
    textAlign: 'center'
}