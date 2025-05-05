const API_GATEWAY_URL = "http://localhost:50500/w";

export async function getWallets() {
    const response = await fetch(`${API_GATEWAY_URL}/Wallets`);
    if (!response.ok) throw new Error("Ошибка загрузки кошельков");
    return await response.json();
}

export async function addWallet(newWallet) {
    console.log("Отправляемый JSON:", newWallet);

    const response = await fetch(`${API_GATEWAY_URL}/Wallets`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newWallet),
    });

    if (!response.ok) throw new Error("Ошибка создания кошелька");

    return await response.json();
}

export async function getWalletsByFamilyId(familyId) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/wallets?familyId=${familyId}`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки кошельков");
        }
        const data = await response.json();
        return data.items || []; // ✅ Если API возвращает массив внутри `items`
    } catch (error) {
        console.error("Ошибка при получении кошельков:", error);
        return [];
    }
}

export async function getWalletById(walletId) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/wallets/${walletId}`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки кошелька");
        }
        return await response.json(); // ✅ Получаем данные о кошельке
    } catch (error) {
        console.error("Ошибка при получении кошелька:", error);
        return null;
    }
}

