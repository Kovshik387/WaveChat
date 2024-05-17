import { MessageInfo } from "models/MessageInfo";
import MessageItem from "./MessageItem";

interface MessageListProps {
    messages: MessageInfo[];
  }

const MessageList = ({messages}: MessageListProps) => {
    return (
        <div className="flex flex-col overflow-auto scroll-smooth h-96 gap-3 pb-3">
            {messages.map((item, index) => (
                <MessageItem message = {item} key={index}/>
                ))}
        </div>
    );
};

export default MessageList;
    