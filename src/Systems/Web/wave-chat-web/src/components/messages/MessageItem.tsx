import { MessageInfo } from "models/MessageInfo";
import { useState } from "react";

interface MessageItemProps {
	message: MessageInfo;
}

export default function Message({ message }: MessageItemProps) {

	return (
		<>
			{
				localStorage.getItem("id") == message.uidUser ?
					<div style={rightFloat}>
						<div>
							<span style={{ fontWeight: "bold" }}>Вы</span>
							<p style={messageContent}>
								{message.content}
							</p>
							<p style={{ color: "" }}>
								{formatDate(new Date(message.sendDate))}
							</p>
						</div>
					</div>
					:
					<div style={leftFloat}>
						<div>
							<span style={{ fontWeight: "bold", color: "black" }}>{message.name}</span>
							<p style={messageContent}>
								{message.content}
							</p>
							<p style={{ color: "" }}>
								{formatDate(new Date(message.sendDate))}
							</p>
						</div>
					</div>
			}
		</>
	);
};

function formatDate(date: Date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // месяцы нумеруются с 0, поэтому добавляем 1
    const day = String(date.getDate()).padStart(2, '0'); // добавим день, если нужно
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');
    
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}

const leftFloat: React.CSSProperties = {
	alignSelf: "flex-start",
	width: "50%",
	padding: "10px",
}

const rightFloat: React.CSSProperties = {
	alignSelf: "flex-end",
	width: "50%",
	padding: "10px",

}

const messageContent: React.CSSProperties = {
	border: "1px solid rgba(0, 0, 0, 0.125)",
	boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
	maxWidth: "100%",
	padding: "10px",
	borderRadius: "10px",
	marginBottom: "10px",
	wordWrap: "break-word",
	background: "gray"
}