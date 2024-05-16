export default async function getImage() {
    const headers = new Headers();
    headers.set('Access-Control-Allow-Origin', '*');
    const url = `http://localhost:8090/v1/Storage/GetUserProfile?userId=${localStorage.getItem("id")}`
    const response = await fetch(url,{method: 'GET',headers: headers});
    console.log(url);
    return response.text();
}