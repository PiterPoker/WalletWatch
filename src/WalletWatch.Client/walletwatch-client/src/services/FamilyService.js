export const API_GATEWAY_URL = "http://localhost:50500/f"; // Заменить на твой реальный URL

export async function createFamily(familyData) {
    console.log("Отправка данных:", familyData); // Логируем для проверки

    const response = await fetch(`${API_GATEWAY_URL}/families`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(familyData),
    });

    if (!response.ok) {
        throw new Error("Ошибка при создании семьи");
    }

    return await response.json();
}

export async function addFamilyMember(familyId, name, role) {
    const newMember = {
        userId: crypto.randomUUID(),
        name,
        role
    };
    console.log(newMember);

    const response = await fetch(`${API_GATEWAY_URL}/families/${familyId}/members`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newMember),
    });

    if (!response.ok) {
        throw new Error("Ошибка добавления участника семьи");
    }

    return await response.json();
}

export async function getFamilies() {
    const response = await fetch(`${API_GATEWAY_URL}/families`);
    console.log("Полученные семьи:", response);
    return await response.json();
}

export async function getFamilyById(familyId) {
    const response = await fetch(`${API_GATEWAY_URL}/families/${familyId}`);

    if (!response.ok) {
        throw new Error("Ошибка загрузки семьи");
    }

    return await response.json();
}

export async function getFamilyMembersByFamilyId(familyId) {
    const response = await fetch(`${API_GATEWAY_URL}/families/${familyId}/members`);
    const data = await response.json();
    console.log("Полученные участники семьи:", data); // ✅ Логируем ответ от API

    return data;
}

export async function fetchFamilyMembers(familyId) {
    try {
        const members = await getFamilyMembersByFamilyId(familyId);
        console.log("Все участники семьи:", members); // ✅ Логируем всех участников

        const headMember = members.find(member => member.role === "Head");
        console.log("Определённый глава семьи:", headMember); // ✅ Логируем главу семьи

        return { members, headMember };
    } catch (error) {
        console.error("Ошибка загрузки участников семьи:", error);
        return { members: [], headMember: null };
    }
}

