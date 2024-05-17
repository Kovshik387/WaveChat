import { useState } from "react";

interface SendMessageProps {
    sendMessage: (message: string) => void;
  }
  
  function SendMessage({ sendMessage }: SendMessageProps) {
    const [message, setMessage] = useState("");
  
    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
      setMessage(e.target.value);
    };
  
    const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      sendMessage(message);
      setMessage("");
    };
  
    return (
      <form onSubmit={onSubmitHandler}>
        <input
          className="send-message__input"
          onChange={onChangeHandler}
          value={message}
          type="text"
          placeholder="Сообщение"
        />
        <button type="submit" disabled={!message}>
        </button>
      </form>
    );
  }
  
  export default SendMessage;