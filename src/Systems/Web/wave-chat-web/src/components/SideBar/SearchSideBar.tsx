import { useEffect, useState } from "react";
import { Form, InputGroup } from "react-bootstrap";

export default function SearchSideBar() {
    const [search, setSearch] = useState("");
    useEffect(() => {

    }, []);

    const handleSubmit = async (event: React.SyntheticEvent<HTMLFormElement>) => {
        event.preventDefault();
        event.stopPropagation();
    }
    return (
        <>
            <Form onSubmit={handleSubmit} style={sideBarSearch}>
                <div style={sideBarSearch}>
                    
                </div>
            </Form>
        </>
    )
}
const sideBarSearch: React.CSSProperties = {
    display: "flex",
    alignItems: "center",
    marginBottom: "20px",
    height: "40px",
    backgroundColor: "#1E1E1E",
    borderRadius: "10px",
    transition: 'background-color 0.4s ease',
}