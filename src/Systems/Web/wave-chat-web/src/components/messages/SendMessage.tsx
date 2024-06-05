import { useState } from "react";
import { Button } from "react-bootstrap";

interface SendMessageProps {
    sendMessage: (userId: string,content: string, chatId:string, username:string) => void;
  }
  
  function SendMessage({ sendMessage }: SendMessageProps) {
    const [message, setMessage] = useState("");
    
    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
      setMessage(e.target.value);
    };
  
    const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      sendMessage(localStorage.getItem("id")!,message,localStorage.getItem("idChat")!,localStorage.getItem("name")!);
      setMessage("");
    };
  
    return (
      <form onSubmit={onSubmitHandler}>
        <div className="input-group mb-3">
          <input
            className="form-control"
            onChange={onChangeHandler}
            value={message}
            type="text"
            placeholder="Сообщение"
          />
          <div className="input-group-append">
            <Button type="submit" className="btn btn-primary" variant="secondary"  disabled={!message}>
              Отправить
            </Button>
          </div>
        </div>
      </form>
    );
  }
  
  export default SendMessage;