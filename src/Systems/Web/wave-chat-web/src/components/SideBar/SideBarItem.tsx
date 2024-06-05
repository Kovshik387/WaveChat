import AccountChats from "@models/Chat";
import getImage from "@functions/GetImage";
import { useEffect, useState } from "react";
import { MessageInfo } from "@models/MessageInfo";

interface SideBarItemProps {
    closeConnection: () => void;
    joinRoom: (chatId: string) => void; // Убедитесь, что параметр - chatId
    setCurrentChatId: (chatId: string) => void;
    chat: AccountChats;
    messages: MessageInfo[];
}

export default function SideBarItem(item: SideBarItemProps) {
    const [userUrl, setUserUrl] = useState("");
    const [anotherUserUrl, setAnotherUserUrl] = useState("");
    const [isHovered, setIsHovered] = useState(false);
    let anotherUserId = "";

    if (item.chat.users.length !== 0) {
        anotherUserId = item.chat.users[0].uid;
    }

    useEffect(() => {
        async function getImages() {
            if (anotherUserId !== "") {
                setAnotherUserUrl( await getImage(anotherUserId));
            }
            setUserUrl(await getImage(localStorage.getItem("id")!));
        }
        getImages();
    }, [anotherUserId]);

    return (
        <div
            style={{
                ...sideBarItem,
                ...(isHovered ? sidebarItemHoverStyle : {})
            }}
            onMouseEnter={() => setIsHovered(true)}
            onMouseLeave={() => setIsHovered(false)}
            onClick={async () => {
                if (item.chat.uid === localStorage.getItem("idChat")) {
                    return;
                }
                localStorage.setItem("idChat", item.chat.uid);
                await item.joinRoom(item.chat.uid); // Убедитесь, что используете await
                item.setCurrentChatId(item.chat.uid);
            }}
        >
            <div>
                <img
                    style={imageStyle}
                    src={anotherUserUrl === "" || anotherUserUrl === null ? "https://cdn-icons-png.flaticon.com/512/149/149452.png" : anotherUserUrl}
                    alt={item.chat.name}
                />
            </div>
            <div style={contentStyle}>
                <span style={{ fontWeight: "bold" }}>
                    {item.chat.users.length < 1 ? item.chat.name : `${item.chat.users[0].name} ${item.chat.users[0].surname}`}
                </span>
                <div style={lastMessageStyle}>
                    <p>{item.chat.lastMessage}</p>
                </div>
            </div>
        </div>
    );
}

const contentStyle: React.CSSProperties = {
    display: "flex",
    flexDirection: "column",
    paddingTop: "10px"
};

const imageStyle: React.CSSProperties = {
    width: '50px',
    height: '50px',
    borderRadius: '50%',
    marginRight: '15px',
};

const lastMessageStyle: React.CSSProperties = {
    fontSize: '14px',
    color: '#bdc3c7',
};

const sideBarItem: React.CSSProperties = {
    display: "flex",
    alignItems: "center",
    marginBottom: "20px",
    padding: "10px",
    height: "60px",
    backgroundColor: "#1E1E1E",
    borderRadius: "10px",
    transition: 'background-color 0.4s ease',
};

const sidebarItemHoverStyle: React.CSSProperties = {
    backgroundColor: '#3d3d3d',
};
