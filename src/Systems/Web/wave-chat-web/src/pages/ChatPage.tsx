import { Message } from "@components/Message";
import { WaitingRoom } from "@components/WaitingRoom";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { SetStateAction, useState } from "react";
import { Chat } from "react-bootstrap-icons";
import { Message } from "types/MessageType";



export default function ChatPage() {
    const [connection, setConnection] = useState<HubConnection | undefined>();
	const [messages, setMessages] = useState<Message>();
	const [chatRoom, setChatRoom] = useState([]);

	const joinChat = async (userName: any, chatRoom: SetStateAction<never[]>) => {
		var connection = new HubConnectionBuilder()
			.withUrl("http://localhost:8010/v1/chat")
			.withAutomaticReconnect()
			.build();

		connection.on("ReceiveMessage", Message) => {
			
            setMessages();
		});

		try {
            if (connection === undefined) return;
			await connection.start();
			await connection.invoke("JoinChat", { userName, chatRoom });

			setConnection(connection);
			setChatRoom(chatRoom);
		} catch (error) {
			console.log(error);
		}
	};

	const sendMessage = async (message: any) => {
		await connection.invoke("SendMessage", message);
	};

	const closeChat = async () => {
		await connection.stop();
		setConnection(null);
	};



	return (
		<div className="min-h-screen flex items-center justify-center bg-gray-100">
			{connection ? (
				<Chat
					messages={messages}
					sendMessage={sendMessage}
					closeChat={closeChat}
					chatRoom={chatRoom}
				/>
			) : (
				<WaitingRoom joinChat={joinChat} />
			)}
		</div>
	);
};
