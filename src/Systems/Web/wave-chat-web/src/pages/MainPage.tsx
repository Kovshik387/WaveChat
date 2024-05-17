import ChatRoom from "@components/ChatRoom";
import Lobby from "@components/Lobby";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Account from "@models/Account";
import { MessageInfo } from "@models/MessageInfo";
import React, { useState } from "react";

export default function MainPage(){
    const [connection, setConnection] = useState<any>();
    const [messages, setMessages] = useState<MessageInfo[]>([]);
    const [accounts, setAccounts] = useState<Account[]>([]);

    const joinRoom = async (userName: string) => {
        try{
            const connection = new HubConnectionBuilder()
        .withUrl("http://localhost:8020/v1/chat")
        .configureLogging(LogLevel.Information)
        .build();

        connection.on("ReceiveMessage", (message: MessageInfo) => {
            setMessages((messages) => [...messages, message]);
        });

        connection.on("ReceiveConnectedUsers", (users: Account[]) => {
            setAccounts(users);
        });

        connection.onclose((e) => {
            setConnection(undefined);
            setMessages([]);
            setAccounts([]);
        });

        await connection.start();
        await connection.invoke("JoinRoom", userName);
        setConnection(connection);
        } catch (e) {
        console.error(e);
        }

    };

    const sendMessage = async (message: string) => {
        try {
            await connection.invoke("SendMessage", message);
        } catch (e) {
          console.error(e);
        }
    };

    const closeConnection = async () => {
        try {
            await connection.stop();
        } catch (e) {
            console.error(e);
        }
      };

    if (localStorage.getItem("id") !== null){
        

    }
    
    return <>
    <div style={{flex: 1}}>
        {
            localStorage.getItem("id") !== null ?
            
            <div>
                {!connection ? (<Lobby joinRoom={joinRoom}/>)
                :
                <ChatRoom
                    account={accounts}
                    messages={messages}
                    sendMessage={sendMessage}
                    closeConnection={closeConnection}
                />
                }
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

const CenterText : React.CSSProperties = {
    textAlign: 'center'
}