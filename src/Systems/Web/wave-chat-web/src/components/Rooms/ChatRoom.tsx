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
    <div >
      <div style={MessageListCss}>
        <MessageList messages={messages} />
        <div className="" ref={messageRef}>
        </div>
      </div>
      <div className="">
        <SendMessage sendMessage={sendMessage} />
      </div>
    </div>
  );
};

const MessageListCss: React.CSSProperties = {
  border: "1px solid ",
  padding: "20px",
  height: '75vh',
  overflowY: 'scroll',
  backgroundColor: 'white'
}

export default ChatRoom;