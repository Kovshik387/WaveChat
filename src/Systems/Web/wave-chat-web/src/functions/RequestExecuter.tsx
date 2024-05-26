export default async function requestExecuter<Tdata>(func: () => Promise<Response | null>) : Promise<Tdata | null> {
    try {
        let response = await func();

        if (response === null) {
            await updateToken();
            response = await func();
        }

        if (response === null) {
            return null;
        }

        return await response.json() as Tdata;
    } catch (error) {
        try {
            await updateToken();
            console.log("Token updated");
            const response = await func();

            if (response === null) {
                return null;
            }

            return await response.json() as Tdata;
        } catch (newError) {
            console.error("Request failed after token update", newError);
            clearLocalStorage();
            return null;
        }
    }
}

async function updateToken() {
    const refreshToken = localStorage.getItem("refreshToken");

    if (!refreshToken) {
        console.log("Refresh token is missing");
        return;
    }

    console.log("Attempting to update token");
    const headers = new Headers({
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem("accessToken")}`
    });

    const url = `http://localhost:8010/v1/Authorization/RefreshAccess?refreshToken=${refreshToken}`;
    const response = await fetch(url, { method: 'GET', headers: headers });

    if (response.status === 200) {
        const result: AuthResponse = await response.json();
        if (!result.errorMessage) {
            localStorage.setItem("accessToken", result.data.accessToken!);
            localStorage.setItem("refreshToken", result.data.refreshToken!);
            localStorage.setItem("id",result.data.id!);
        } else {
            console.error("Error updating token:", result.errorMessage);
            clearLocalStorage();
        }
    } else {
        console.error("Failed to update token, response status:", response.status);
        clearLocalStorage();
    }
}

function clearLocalStorage() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("id");
    localStorage.removeItem("name");
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
