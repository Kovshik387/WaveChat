
import RequestExecuter from "@functions/RequestExecuter";
import { RootState } from "stores/store";
import { useEffect} from "react";
import { Navbar, Container, Nav } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {logout} from "./../stores/accountSlice"

export type HeaderProps = {
  state: [string, React.Dispatch<React.SetStateAction<string>>]
}

export default function HeaderNavigation() {
  const username =   useSelector((state: RootState) => state.account.username);
  const isLoggedIn = useSelector((state: RootState) => state.account.isLoggedIn);
  const dispatch = useDispatch();
  async function logoutRequest(): Promise<Response> {
    const url = `http://localhost:8010/v1/Authorization/Logout?refreshToken=${localStorage.getItem("refreshToken")}`;
    const headers = new Headers();
    headers.set("Authorization", "Bearer " + localStorage.getItem("accessToken"));
    headers.set('Content-Type', 'application/json');
    headers.set('accept', "*/*");
    headers.set('Access-Control-Allow-Origin', '*');
    const response = await fetch(url, { method: 'DELETE', headers: headers });
    if (response !== null) {
      localStorage.removeItem("id");
      localStorage.removeItem('refreshToken');
      localStorage.removeItem('accessToken');
      localStorage.removeItem('name');
    }
    return response;
  }

  useEffect(() => {

  }, []);

  return (
    <>
      <Navbar bg="dark" data-bs-theme="dark" className="bg-body-tertiary">
        <Container >
          <Navbar.Brand href="/">Сайт</Navbar.Brand>
          <Nav>
            <Navbar.Toggle></Navbar.Toggle>
            {
              !isLoggedIn ?
                <Navbar.Collapse className="justify-content-end">
                  <Nav.Link href="/signIn">Войти</Nav.Link>
                  <Nav.Link href="/signUp">Регистрация</Nav.Link>
                </Navbar.Collapse>
                :
                <Nav>
                  <Navbar.Collapse className="justify-content-end">
                    <Nav.Link href="/profile">Личный кабинет | </Nav.Link>
                  </Navbar.Collapse>

                  <Navbar.Collapse className="justify-content-end">
                    <Navbar.Text>
                      Привет! {username} |
                    </Navbar.Text>
                    <Nav.Link  onClick={async () => {
                      await RequestExecuter<void>(logoutRequest);
                      dispatch(logout());
                      window.location.reload()
                    }} >Выйти</Nav.Link>
                  </Navbar.Collapse>
                </Nav>
            }
          </Nav>
        </Container>
      </Navbar>
    </>
  )
}