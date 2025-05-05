import React, { useEffect, useState } from "react";
import { getFamilies, createFamily } from "../services/FamilyService";
import FamilyList from "../components/Family/FamilyList";
import CreateFamilyForm from "../components/Family/CreateFamilyForm";
import { Container, Card } from "react-bootstrap"; // Добавляем Bootstrap

function FamilyPage() {
    const [families, setFamilies] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const data = await getFamilies();
                setFamilies(data);
            } catch (error) {
                console.error("Ошибка загрузки семей:", error);
            }
        }
        fetchData();
    }, []);

    const handleCreateFamily = async (familyData) => {
        try {
            const newFamily = await createFamily(familyData);
            setFamilies([...families, newFamily]);
        } catch (error) {
            console.error("Ошибка создания семьи:", error);
        }
    };

    return (
        <Container className="mt-4">
            <Card className="p-4 shadow-sm">
                <Card.Body>
                    <h1 className="text-center mb-4">Семейная бухгалтерия</h1>
                    <CreateFamilyForm onSuccess={handleCreateFamily} />
                    <hr />
                    <FamilyList families={families} />
                </Card.Body>
            </Card>
        </Container>
    );
}

export default FamilyPage;
