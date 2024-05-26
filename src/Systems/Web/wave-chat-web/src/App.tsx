import Footer from "@components/Footer";
import HeaderNavigation from "@components/Header";
import AccountInfo from "@pages/Account/Account";
import SignInPage from "@pages/Authorization/SignIn";
import SignUpPage from "@pages/Authorization/SignUp";
import MainPage from "@pages/MainPage";
import { useState } from "react";
import * as ReactRouter from "react-router-dom";


export default function App(){
	
	//const [header,setHeader] = useState("");

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
		path: "/profile",
		element: <AccountInfo/>
	}
	]);
	return (
		<>
			<HeaderNavigation />
				<div style={mainContent}>
					<div className="row">
						<div>
							<ReactRouter.RouterProvider router={router}/>
						</div>
					</div>
				</div>
			<Footer/>
		</>
	);
	

}
const mainContent : React.CSSProperties = {
	display: "flex",
	justifyContent: "flex-end",
	flexDirection: "column",
	width: "100%",
	height: "max-content",
	flex: 1
}