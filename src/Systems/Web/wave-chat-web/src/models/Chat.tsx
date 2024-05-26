import User from "./User";

export default interface AccountChats {
    uid: string;
    name: string;
    url: string;
    users: User[];
    lastMessage: string
}

