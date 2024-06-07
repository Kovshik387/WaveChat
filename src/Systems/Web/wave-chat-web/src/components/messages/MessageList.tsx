import { MessageInfo } from "models/MessageInfo";
import MessageItem from "./MessageItem";
import React, { useEffect } from "react";

interface MessageListProps {
    messages: MessageInfo[];
}

const MessageList = ({ messages }: MessageListProps) => {
    useEffect(() => {
        console.log("update");
    },[]) 
    return (
        <>
            <div style={messageContainer}>
                {
                    messages.map((item, index) => (
                        <MessageItem message={item} key={index} />
                    ))
                }
            </div>
        </>
    );
};

const messageContainer: React.CSSProperties = {
    display: "flex",
    flexDirection: "column",
    margin: "10px",
    
}

export default MessageList;
