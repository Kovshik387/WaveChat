import CheckToken from "@functions/CheckToken";
import { Navbar,Container,Nav} from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export type HeaderProps = {
  state: [string, React.Dispatch<React.SetStateAction<string>>]
}


export default function HeaderNavigation(){    
    async function logout(){
        console.log('hi');
        const url = `http://localhost:8010/v1/Authorization/Logout?refreshToken=${localStorage.getItem("refreshToken")}`;
        const headers = new Headers();
        headers.set("Authorization","Bearer "+localStorage.getItem("accessToken"));
        headers.set('Content-Type','application/json');
        headers.set('accept',"*/*");
        headers.set('Access-Control-Allow-Origin', '*');
        try{
            await fetch(url,{method: 'DELETE',headers: headers});
        }
        catch(error){
            await CheckToken();
            await fetch(`http://localhost:8010/v1/Authorization/Logout?refreshToken=${localStorage.getItem("refreshToken")}`,{method: 'DELETE',headers: headers});
        }

        localStorage.removeItem("id");
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('accessToken');
        // if (response.status == 401){
        //     console.log('unath')
        //     CheckToken();
        //     await fetch(url,{method: 'DELETE',headers: headers});
        // }
        // console.log('complete')
        // await fetch(url,{method: 'DELETE',headers: headers});
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
                    Привет! {localStorage.getItem("id")}
                </Navbar.Text>
                <Nav.Link onClick={() => {
                  logout();
                  window.location.reload();
                }} >Выйти</Nav.Link>
              </Navbar.Collapse>
            }
          </Nav>
        </Container>
      </Navbar>
    </>
    )
}