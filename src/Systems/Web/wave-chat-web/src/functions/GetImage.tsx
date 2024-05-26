export default async function getImage(id: string): Promise<string> {
    const headers = new Headers();
    headers.set('Access-Control-Allow-Origin', '*');
    headers.set('Content-Type', 'application/json');
    const url = `http://localhost:8090/v1/Storage/GetUserProfile?userId=${id}`
    const response = await fetch(url, { method: 'GET', headers: headers });
    return response.text();
}