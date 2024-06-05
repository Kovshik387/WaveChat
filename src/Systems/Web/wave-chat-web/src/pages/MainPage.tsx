import ChatRoom from "@components/Rooms/ChatRoom";
import SideBar from "@components/SideBar/SideBar";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { MessageInfo } from "@models/MessageInfo";
import React, { useEffect, useState } from "react";

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

export default function MainPage() {
    const [connection, setConnection] = useState<any>(null);
    const [messages, setMessages] = useState<{ [key: string]: MessageInfo[] }>({});
    const [currentChatId, setCurrentChatId] = useState<string | null>(null);

    useEffect(() => {
        const connect = new HubConnectionBuilder()
            .withUrl("http://localhost:8020/chat")
            .configureLogging(LogLevel.Information)
            .build();

        connect.on("ReceiveMessage", (message: MessageInfo) => {
            console.log("Received message:", message);
            setMessages(prevMessages => {
                console.log('received');
                const chatMessages = prevMessages[message.uidChannel] || [];
                return { ...prevMessages, [message.uidChannel]: [...chatMessages, message] };
            });
        });

        connect.on("NewMessageNotification", (message: MessageInfo) => {
            console.log("New message notification:", message);
            updateChatList(message);
        });

        connect.onclose((e) => {
            console.log("Connection closed: ", e);
            setConnection(null);
            setMessages({});
        });

        connect.start()
            .then(() => {
                console.log("Connected to SignalR");
                setConnection(connect);
            })
            .catch(err => console.log('Error while establishing connection: ' + err));

        return () => {
            if (connection) {
                connection.stop().catch((err: string) => console.log('Error while stopping connection: ' + err));
            }
        };
    }, []);

    useEffect(() => {
        if (currentChatId) {
            async function fetchMessages() {
                const response = await getMessages(currentChatId!);
                if (response !== null) {
                    const messagesData: MessageInfo[] = await response.json();
                    console.log(messagesData);
                    setMessages(prevMessages => ({ ...prevMessages, [currentChatId!]: messagesData }));
                }
            }
            fetchMessages();
        }
    }, [currentChatId]);

    const joinRoom = async (chatId: string) => {
        if (connection) {
            console.log(`Connection state: ${connection.state}`);
            if (connection.state === "Connected") {
                try {
                    await connection.invoke("JoinChat", chatId);
                    console.log(`Joined chat room with id: ${chatId}`);
                    setCurrentChatId(chatId);
                } catch (e) {
                    console.error("Failed to join room:", e);
                }
            } else {
                console.log("Connection is not established.");
            }
        } else {
            console.log("Connection is null.");
        }
    };

    const sendMessage = async (userId: string, content: string, chatId: string, username: string) => {
        if (connection && connection.state === "Connected") {
            try {
                await connection.invoke("SendMessage", userId, content, chatId, username);
            } catch (e) {
                console.error(e);
            }
        } else {
            console.log("Connection is not established.");
        }
    };

    const closeConnection = async () => {
        if (connection && connection.state === "Connected") {
            try {
                await connection.stop();
            } catch (e) {
                console.log(e);
            }
            setConnection(null);
            setMessages({});
        }
    };

    const updateChatList = (message: MessageInfo) => {

    };

    return (
        <div style={{ flex: 1 }}>
            {localStorage.getItem("id") !== null ? (
                <div className="row" style={{ justifyContent: 'center',}}>
                    <div className="col col-sm-2">
                        <SideBar
                            connection={connection}
                            closeConnection={closeConnection}
                            joinRoom={joinRoom}
                            setCurrentChatId={setCurrentChatId}
                            messages={messages}
                            setMessages={setMessages}

                        />
                    </div>
                    <div className="col col-sm-7">
                        {currentChatId && connection ? (
                            <ChatRoom
                                messages={messages[currentChatId] || []}
                                sendMessage={sendMessage}
                                account={[]}
                                closeConnection={closeConnection}
                            />
                        ) : (
                            <h1 style={{ textAlign: "center",top: "45%", left:"60%",position: "absolute",transform: "translate(-50%,-50%)" }}>Выберите чат</h1>
                        )}
                    </div>
                </div>
            ) : (
                <div style={{...CenterText,top: "45%", left:"50%",position: "absolute",transform: "translate(-50%,-50%)" }}>
                    <h1>WaveChat</h1>
                    <h2>Добро пожаловать</h2>
                </div>
            )}
        </div>
    );
}

const CenterText: React.CSSProperties = {
    textAlign: 'center',
};
