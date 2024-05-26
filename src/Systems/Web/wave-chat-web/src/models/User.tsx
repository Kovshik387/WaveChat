import { UUID } from "crypto";

export default interface User {
    name: string;
    surname: string;
    uid: string;
}