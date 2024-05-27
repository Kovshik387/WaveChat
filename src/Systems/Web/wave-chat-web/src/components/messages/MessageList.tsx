import { MessageInfo } from "models/MessageInfo";
import MessageItem from "./MessageItem";
import React from "react";

interface MessageListProps {
    messages: MessageInfo[];
}

const MessageList = ({ messages }: MessageListProps) => {
    //const groupedMessages = groupMessages(messages);
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

const groupMessages = (messages: MessageInfo[]): MessageInfo[][] => {
    let currentGroup: MessageInfo[] = [];
    let groupedMessages: MessageInfo[][] = [];
    messages.forEach((message, index) => {
        if (currentGroup.length === 0 || currentGroup[0].uid === message.uidUser) {
            currentGroup.push(message);
        } else {
            groupedMessages.push(currentGroup);
            currentGroup = [message];
        }

        if (index === messages.length - 1) {
            groupedMessages.push(currentGroup);
        }
    });

    return groupedMessages;
}

const messageContainer: React.CSSProperties = {
    display: "flex",
    flexDirection: "column",
    margin: "10px",
    
}

export default MessageList;
