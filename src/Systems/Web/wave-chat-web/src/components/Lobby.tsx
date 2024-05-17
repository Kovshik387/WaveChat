import React, { useState } from "react";

interface LobbyProps {
  joinRoom: (userName: string) => void;
}

const Lobby = ({ joinRoom }: LobbyProps) => {
  const [userName, setUserName] = useState("");

  const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUserName(e.target.value);
  };

  const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    joinRoom(userName);
  };

  return (
    <div >
      <h2>Чат</h2>
      <form onSubmit={onSubmitHandler}>
        <input
            type="text"
            placeholder="Сообщение"
            onChange={onChangeHandler}
        />
        <button  type="submit" disabled={!userName}>
          Отправить
        </button>
      </form>
    </div>
  );
};

export default Lobby;