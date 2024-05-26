export default async function requestExecuter<Tdata>(func: () => Promise<Response | null>) : Promise<Tdata | null> {

    try{
        const response = await func();
        if (response === null){
            updateToken();
        }
        const newResposne = await func();
        return newResposne!.json() as Tdata;
    }
    catch (error) {
        try{
            await updateToken();
            console.log("update token");
            const response = await func();
            console.log(response!.status);
            return await response!.json() as Tdata;
        }
        catch(newError){
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("id");
            localStorage.removeItem("name");
            return null;
        }
    }
}

async function updateToken() {
    const headers = new Headers();

    if (localStorage.getItem("refreshToken") === null) {
        console.log("refresh пуст")
        return;
    };
    console.log("Внутри что-то есть")
    headers.set('Access-Control-Allow-Origin', '*');
    headers.set('Content-Type','application/json');
    headers.set("Authorization","Bearer "+ localStorage.getItem("accessToken")!);
    console.log("refresh: " + localStorage.getItem("refreshToken"));
    const url = `http://localhost:8010/v1/Authorization/RefreshAccess?refreshToken=${localStorage.getItem("refreshToken")}`;
    console.log("обновление")
    const response = await fetch(url,{method: 'Get',headers: headers});
    if (response.status == 200){
        var result: AuthResponse = await response.json()
        if(result.errorMessage === ""){
            console.log("Данные обновлены");
            localStorage.setItem("accessToken",result.data.accessToken!);
            localStorage.setItem("refreshToken",result.data.refreshToken!);
        }
        else{
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("id");
        }
    }
    else{
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("id");
    }
    return 
}

export type data = {
    id: string | null,
    accessToken: string | null,
    refreshToken: string | null
}

export type AuthResponse = {
    data: data
    errorMessage: string
};