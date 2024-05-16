import Footer from "@components/Footer";
import HeaderNavigation from "@components/Header";
import SignInPage from "@pages/Authorization/SignIn";
import SignUpPage from "@pages/Authorization/SignUp";
import ChatPage from "@pages/ChatPage";
import MainPage from "@pages/MainPage";
import * as ReactRouter from "react-router-dom";


export default function App(){
	
	const router = ReactRouter.createBrowserRouter([
		{
      path: "/",
      element: <MainPage/>
    },
    {
      path: "/signIn",
      element: <SignInPage/>
    },
    {
      path: "/signUp",
      element: <SignUpPage/>
    },
    {
      path: "/chat/:chat",
      element: <ChatPage/>
    }
	]);
	return (
		<div style={mainContent}>
			<HeaderNavigation/>
			<ReactRouter.RouterProvider router={router} />
			<Footer/>
		</div>
	);
	

}
const mainContent : React.CSSProperties = {
	display: "flex",
	justifyContent: "flex-end",
	flexDirection: "column",
	width: "100%",
	height: "100%",
	flex: 1
}