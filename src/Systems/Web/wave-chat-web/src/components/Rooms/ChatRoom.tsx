import { useEffect, useRef } from "react";
import Account from "@models/Account";
import { MessageInfo } from "@models/MessageInfo";
import SendMessage from "../Messages/SendMessage";
import MessageList from "../Messages/MessageList";

interface ChatRoomProps {
  account: Account[];
  messages: MessageInfo[];
  sendMessage: (userId: string, content: string, chatId: string, username: string) => void;
  closeConnection: () => void;
}

const ChatRoom = ({
  messages,
  sendMessage,
}: ChatRoomProps) => {
  const messageRef = useRef<any>();

  useEffect(() => {
    if (messageRef && messageRef.current) {
      messageRef.current.scrollIntoView({
        behavior: "smooth",
      });
    }
  }, [messages]);

  return (
    <div style={{padding: "20px"}}>
      <div style={modernCardStyle} >
        <MessageList messages={messages} />
        <div className="" ref={messageRef}>
        </div>
      </div>
      
      <div style={{paddingTop: "25px"}}>
        <SendMessage sendMessage={sendMessage} />
      </div>
    </div>
  );
};

const modernCardStyle: React.CSSProperties = {
  border: "1px solid rgba(0, 0, 0, 0.125)",
  borderRadius: "10px",
  boxShadow: "10px 4px 8px rgba(108, 122, 137, 0.5)",
  padding: "20px",
  background: "white",
  transition: "transform 0.2s",
  overflowY: 'scroll',
  height: '73vh',
};

export default ChatRoom;