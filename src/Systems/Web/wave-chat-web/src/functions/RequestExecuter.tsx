export default async function requestExecuter<Tdata>(func: () => Promise<Response | null>): Promise<Tdata | null> {
    try {
        let response = await func();

        if (!response) {
            await tryRefreshToken();
            response = await func();
        }

        if (!response) {
            return null;
        }

        return await response.json() as Tdata;
    } catch (error) {
        console.error("Initial request failed", error);
        try {
            await tryRefreshToken();
            const response = await func();
            if (!response) {
                return null;
            }
            return await response.json() as Tdata;
        } catch (newError) {
            console.error("Request failed after token refresh", newError);
            clearLocalStorage();
            return null;
        }
    }
}

async function tryRefreshToken(): Promise<boolean> {
    const refreshToken = localStorage.getItem("refreshToken");
    if (!refreshToken) {
        console.log("Refresh token is missing");
        clearLocalStorage();
        return false;
    }

    try {
        const response = await fetch(getRefreshTokenUrl(refreshToken), getRefreshTokenRequestOptions());
        return await handleRefreshTokenResponse(response);
    } catch (error) {
        console.error("Failed to refresh token", error);
        clearLocalStorage();
        return false;
    }
}

function getRefreshTokenUrl(refreshToken: string): string {
    return `http://localhost:8010/v1/Authorization/RefreshAccess?refreshToken=${refreshToken}`;
}

function getRefreshTokenRequestOptions(): RequestInit {
    return {
        method: 'GET',
        headers: new Headers({
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("accessToken")}`
        })
    };
}

async function handleRefreshTokenResponse(response: Response): Promise<boolean> {
    if (response.status === 200) {
        const result: AuthResponse = await response.json();
        if (result.errorMessage === "") {
            saveTokens(result.data);
            return true;
        } else {
            console.error("Error updating token:", result.errorMessage);
        }
    } else {
        console.error("Failed to update token, response status:", response.status);
    }
    clearLocalStorage();
    return false;
}

function saveTokens(data: data): void {
    localStorage.setItem("accessToken", data.accessToken!);
    localStorage.setItem("refreshToken", data.refreshToken!);
    localStorage.setItem("id", data.id!);
}

function clearLocalStorage(): void {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("id");
    localStorage.removeItem("name");
}

export type data = {
    id: string | null;
    accessToken: string | null;
    refreshToken: string | null;
}

export type AuthResponse = {
    data: data;
    errorMessage: string;
};
