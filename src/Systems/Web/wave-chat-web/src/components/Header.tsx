
import RequestExecuter from "@functions/RequestExecuter";
import { Navbar,Container,Nav} from "react-bootstrap";

export type HeaderProps = {
  state: [string, React.Dispatch<React.SetStateAction<string>>]
}

export default function HeaderNavigation(){      
  
  async function logout() : Promise<Response>{
      const url = `http://localhost:8010/v1/Authorization/Logout?refreshToken=${localStorage.getItem("refreshToken")}`;
      const headers = new Headers();
      headers.set("Authorization","Bearer "+localStorage.getItem("accessToken"));
      headers.set('Content-Type','application/json');
      headers.set('accept',"*/*");
      headers.set('Access-Control-Allow-Origin', '*');
      const response = await fetch(url,{method: 'DELETE',headers: headers});
      if (response !== null){
        localStorage.removeItem("id");
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('accessToken');
      }
      return response;
    }
      
    return (
    <>
        <Navbar bg="dark" data-bs-theme="dark" className="bg-body-tertiary">
        <Container >
          <Navbar.Brand href="/">Сайт</Navbar.Brand>
          <Nav>
            <Navbar.Toggle></Navbar.Toggle>
            {
              localStorage.getItem("id") === null ?

              <Navbar.Collapse className="justify-content-end">
                  <Nav.Link href="/signIn">Войти</Nav.Link>
                  <Nav.Link href="/signUp">Регистрация</Nav.Link>
              </Navbar.Collapse>

              :

              <Navbar.Collapse className="justify-content-end">  
                <Navbar.Text>
                    Привет! {localStorage.getItem("name")}
                </Navbar.Text>
                <Nav.Link onClick={() => {
                  RequestExecuter<void>(logout);
                }} >Выйти</Nav.Link>
              </Navbar.Collapse>
            }
          </Nav>
        </Container>
      </Navbar>
    </>
    )
}