import React, { useState } from "react";
import { addFamilyMember } from "../../services/FamilyService";

function AddFamilyMemberForm({ familyId, onMemberAdded }) {
    const [name, setName] = useState("");
    const [role, setRole] = useState("Adult"); // ✅ Устанавливаем "Adult" по умолчанию

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const newMember = await addFamilyMember(familyId, name, role);
            onMemberAdded(newMember);
            setName("");
            setRole("Adult"); // Сбрасываем в стандартное значение
        } catch (error) {
            console.error("Ошибка:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="d-flex flex-column gap-3 mt-3">
            <div className="d-flex flex-column gap-2">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Имя"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    required
                />

                <select
                    className="form-select"
                    value={role}
                    onChange={(e) => setRole(e.target.value)}
                >
                    <option value="Head">Head (Глава)</option>
                    <option value="Adult">Adult (Взрослый)</option>
                    <option value="Child">Child (Ребёнок)</option>
                </select>
            </div>

            <button type="submit" className="btn btn-success mt-2">Добавить члена</button>
            {/* ✅ Кнопка теперь внизу */}
        </form>
    );
}

export default AddFamilyMemberForm;
