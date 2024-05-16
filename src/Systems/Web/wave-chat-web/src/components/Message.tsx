import { Message } from "@pages/ChatPage";

export const Message = (message:Message) => {
	console.log("name: " + userName + " message: " + message)
	return (
		<div className="w-fit ">
			<span className="text-sm text-slate-600">{userName}</span>
			<div className="p-2 bg-gray-100 rounded-lg shadow-md">
				{message}
			</div>
		</div>
	);
};
