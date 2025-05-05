const API_GATEWAY_URL = "http://localhost:50500/e";

// ✅ Создать нового автора
export async function createAuthor(authorName) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/authors`, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({name: authorName}),
        });

        if (!response.ok) {
            throw new Error("Ошибка при создании автора");
        }

        return await response.json(); // ✅ Получаем объект автора с его `id`
    } catch (error) {
        console.error("Ошибка создания автора:", error);
        return null;
    }
}

// ✅ Создать новый расход
export async function createExpence(expence) {
    try {
        console.log("Creating expence", expence);
        
        const response = await fetch(`${API_GATEWAY_URL}/expenses`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(expence),
        });

        if (!response.ok) {
            throw new Error("Ошибка при создании расхода");
        }

        return await response.json();
    } catch (error) {
        console.error("Ошибка создания расхода:", error);
        return null;
    }
}

// ✅ Получить список расходов по кошельку
export async function getExpencesByWallet(walletId) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/expenses/wallet/${walletId}`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки расходов кошелька");
        }
        return await response.json();
    } catch (error) {
        console.error("Ошибка получения расходов:", error);
        return [];
    }
}

// ✅ Получить список расходов по категории
export async function getExpencesByCategory(categoryId) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/expences/category/${categoryId}`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки расходов категории");
        }
        return await response.json();
    } catch (error) {
        console.error("Ошибка получения расходов:", error);
        return [];
    }
}

// ✅ Получить все категории расходов
export async function getCategories() {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/categories`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки категорий");
        }
        return await response.json();
    } catch (error) {
        console.error("Ошибка получения категорий:", error);
        return [];
    }
}

// ✅ Создать новую категорию расходов
export async function createCategory(category) {
    try {
        const response = await fetch(`${API_GATEWAY_URL}/categories`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(category),
        });

        if (!response.ok) {
            throw new Error("Ошибка создания категории");
        }

        return await response.json();
    } catch (error) {
        console.error("Ошибка создания категории:", error);
        return null;
    }
}

export async function getAllExpences() 
{
    try {
        const response = await fetch(`${API_GATEWAY_URL}/expenses/period`);
        if (!response.ok) {
            throw new Error("Ошибка загрузки расходов кошелька");
        }
        return await response.json();
    } catch (error) {
        console.error("Ошибка получения расходов:", error);
        return [];
    }
}