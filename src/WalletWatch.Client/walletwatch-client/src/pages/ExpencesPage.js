import React, { useState, useEffect } from "react";
import { Container, Card, Button } from "react-bootstrap";
import { getExpencesByWallet, getAllExpences } from "../services/ExpencesService";
import ExpencesList from "../components/Expenses/ExpencesList";
import CategoryModal from "../components/Expenses/CategoryModal"; // ✅ Импортируем модальное окно для категорий
import ExpencesForm from "../components/Expenses/ExpencesForm";

function ExpensesPage() {
    const [expences, setExpences] = useState([]);
    const [showCategoryModal, setShowCategoryModal] = useState(false); // ✅ Управление модальным окном

    useEffect(() => {
        async function fetchExpences() {
            try {
                const data = await getAllExpences();
                setExpences(data);
            } catch (error) {
                console.error("Ошибка загрузки расходов:", error);
            }
        }
        fetchExpences();
    }, []);

    return (
        <Container className="mt-4">
            <Card className="shadow-lg p-4">
                <Card.Body>
                    {/* Кнопка для открытия модального окна */}
                    <Button variant="primary" onClick={() => setShowCategoryModal(true)}>Управление категориями</Button>

                    <ExpencesForm onExpenceAdded={(newExpence) => setExpences([...expences, newExpence])} />
                    
                    {/* Список затрат */}
                    <ExpencesList expences={expences} />

                    {/* Модальное окно для управления категориями */}
                    <CategoryModal show={showCategoryModal} onClose={() => setShowCategoryModal(false)} />
                </Card.Body>
            </Card>
        </Container>
    );
}

export default ExpensesPage;
