import React, { useState } from "react";
import { v4 as uuidv4 } from "uuid"; // Добавляем генерацию GUID
import { createFamily } from "../../services/FamilyService";

function CreateFamilyForm({ families, setFamilies }) {
    const [familyName, setFamilyName] = useState("");
    const [familyHeadUserName, setFamilyHeadUserName] = useState("");

    const handleSubmit = async (event) => {
        event.preventDefault();
        const familyHeadUserId = uuidv4();

        try {
            const newFamily = await createFamily({ familyName, familyHeadUserId, familyHeadUserName });

            setFamilies([...families, newFamily]);
            setFamilyName("");
            setFamilyHeadUserName("");
        } catch (error) {
            console.error("Ошибка при создании семьи:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-4">
            <div className="mb-3">
                <label className="form-label">Название семьи</label>
                <input type="text" className="form-control" value={familyName} onChange={(e) => setFamilyName(e.target.value)} required />
            </div>
            <div className="mb-3">
                <label className="form-label">Имя главы семьи</label>
                <input type="text" className="form-control" value={familyHeadUserName} onChange={(e) => setFamilyHeadUserName(e.target.value)} required />
            </div>
            <button type="submit" className="btn btn-success">Создать семью</button>
        </form>
    );
}


export default CreateFamilyForm;
