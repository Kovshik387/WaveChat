import { useEffect, useRef } from "react";
import Account from "@models/Account";
import { MessageInfo } from "@models/MessageInfo";
import SendMessage from "./messages/SendMessage";
import MessageList from "./messages/MessageList";


interface ChatRoomProps {
  account: Account[];
  messages: MessageInfo[];
  sendMessage: (message: string) => void;
  closeConnection: () => void;
}

const ChatRoom = ({
  messages,
  sendMessage,
  closeConnection,
  account,
}: ChatRoomProps) => {
  const messageRef = useRef<any>();

  useEffect(() => {
    if (messageRef && messageRef.current) {
      const { scrollHeight, clientHeight } = messageRef.current;
      messageRef.current.scrollTo({
        left: 0,
        top: scrollHeight - clientHeight,
        behavior: "smooth",
      });
    }
  }, [messages]);

  return (
    <div >
      <div >
        <div >
          <span ></span>
          <div className="toolbar">
            <span className="search-icon" onClick={closeConnection}>
            </span>
          </div>
        </div>
        <div className="">
          {/* <AccountList users={users} /> */}
        </div>
      </div>
      <div className="">
        <div className="">
          <span className=""></span>
        </div>
        <div className="" ref={messageRef}>
          <MessageList messages={messages} />
        </div>
        <div className="">
          <SendMessage sendMessage={sendMessage} />
        </div>
      </div>
    </div>
  );
};

export default ChatRoom;