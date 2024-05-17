import { MessageInfo } from "models/MessageInfo";

interface MessageItemProps {
	message: MessageInfo;
  }

export default function Message ({message}: MessageItemProps) {
	console.log("name: " + message.userId + " message: " + message.content)
	return (
		<div className="w-fit ">
			<span className="text-sm text-slate-600">{message.userId}</span>
			<div className="p-2 bg-gray-100 rounded-lg shadow-md">
				{message.content}
			</div>
		</div>
	);
};
