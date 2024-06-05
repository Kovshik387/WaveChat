import AccountChats from "@models/Chat";
import { LobbyProps } from "./SideBar";
import getImage from "@functions/GetImage";
import { useEffect, useState } from "react";
import AccountDetails from "@models/AccountDetails";

interface SideBarItemProps extends LobbyProps {
    closeConnection: () => void;
    joinRoom: (userName: string) => void;
    setCurrentChatId: (chatId: string) => void;
    newChat: AccountDetails;
    chat: AccountChats[];
    setNewAccount: (value: AccountDetails[]) => void;
    setSearch: (value: string) => void;
}

async function createNewChat(idUser: String, idAnotherUser: String) {
    const headers = new Headers();
        headers.set('Access-Control-Allow-Origin', '*');
        headers.set('Content-Type', 'application/json');
        headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken")!);
        const url = `http://localhost:8020/v1/Chat/NewChat?idUser=${idUser}&idAnotherUser=${idAnotherUser}`;
        try {
            return fetch(url, { method: 'Post', headers: headers });
        }
        catch (error) { return null; }
}

export default function SideBarSearchItem(item: SideBarItemProps) {

    const [userUrl, setUserUrl] = useState("");
    const [anotherUserUrl, setAnotherUserUrl] = useState("");
    const [isHovered, setIsHovered] = useState(false);
    let anotherUserId = "";
    anotherUserId = item.newChat.uid;

    useEffect(() => {
        async function getImages() {
            if (anotherUserId !== "") {
                setAnotherUserUrl(await getImage(anotherUserId))
            }
            setUserUrl(await getImage(localStorage.getItem("id")!));
        }
        getImages();
    }, [anotherUserId])
    return (
        <>
            <div style={{
                ...sideBarItem,
                ...(isHovered ? sidebarItemHoverStyle : {})
            }}
                onMouseEnter={() => setIsHovered(true)}
                onMouseLeave={() => setIsHovered(false)}
                onClick={async () => {
                    if (item.newChat.uid === localStorage.getItem("idChat")){
                        return;
                    }
                    localStorage.removeItem("idChat");
                    item.closeConnection();
                    let idChat = await createNewChat(localStorage.getItem("id")!,anotherUserId);
                    let data = await idChat?.json() as AccountChats;
                    if (!data) return;

                    item.chat.push(data);
                    item.setNewAccount([]);
                    item.setSearch("");
                    localStorage.setItem("idChat",data.uid);
                    
                    item.joinRoom(data.uid);
                    item.setCurrentChatId(data.uid);
                }}>
                <div>
                    <img
                        style={imageStyle}
                        src={anotherUserUrl === "" || anotherUserUrl === null ? "https://cdn-icons-png.flaticon.com/512/149/149452.png" : anotherUserUrl}
                        alt={item.newChat.name}
                    />
                </div>
                <div style={contentStyle}>
                    <span
                        style={{ fontWeight: "bold" }}>
                        {item.newChat.name}
                    </span>
                    <div style={lastMessageStyle}>
                    </div>
                </div>
            </div>
        </>
    )
}

const contentStyle: React.CSSProperties = {
    flexDirection: "column",
    paddingTop: "10px"
}

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
}

const sidebarItemHoverStyle: React.CSSProperties = {
    backgroundColor: '#3d3d3d',
};
