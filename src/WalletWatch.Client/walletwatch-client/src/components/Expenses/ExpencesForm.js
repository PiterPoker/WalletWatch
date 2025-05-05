import React, { useState, useEffect } from "react";
import { Form, Button } from "react-bootstrap";
import { createExpence, getCategories } from "../../services/ExpencesService";
import { getWallets } from "../../services/WalletService";

function ExpencesForm({ onExpenceAdded }) {
    const [amount, setAmount] = useState("");
    const [description, setDescription] = useState("");
    const [walletId, setWalletId] = useState("");
    const [authorId, setAuthorId] = useState(""); // ✅ Теперь пользователь вводит GUID автора вручную
    const [categoryId, setCategoryId] = useState("");
    const [categories, setCategories] = useState([]);
    const [wallets, setWallets] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                setCategories(await getCategories());
                setWallets((await getWallets()).items);
            } catch (error) {
                console.error("Ошибка загрузки данных:", error);
            }
        }
        fetchData();
    }, []);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const newExpence = {
            amount: parseFloat(amount),
            description,
            transactionDate: new Date().toISOString(), // ✅ Текущая дата
            walletId,
            authorId, // ✅ Теперь вводится вручную
            categoryId,
            currency: "BYN"
        };

        try {
            const createdExpence = await createExpence(newExpence);
            if (createdExpence) {
                onExpenceAdded(createdExpence);
                setAmount("");
                setDescription("");
                setWalletId("");
                setAuthorId("");
                setCategoryId("");
            }
        } catch (error) {
            console.error("Ошибка при создании затрат:", error);
        }
    };

    return (
        <Form onSubmit={handleSubmit} className="mb-3">
            <Form.Group>
                <Form.Label>Сумма</Form.Label>
                <Form.Control type="number" value={amount} onChange={(e) => setAmount(e.target.value)} required />
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>Описание</Form.Label>
                <Form.Control type="text" value={description} onChange={(e) => setDescription(e.target.value)} required />
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>Кошелек</Form.Label>
                    <Form.Select value={walletId} onChange={(e) => setWalletId(e.target.value)} required>
                            <option value="">Выберите кошелек</option>
                                {Array.isArray(wallets) ? ( // ✅ Проверяем, что wallets — массив
                                    wallets.map(wallet => (
                                        <option key={wallet.id} value={wallet.id}>{wallet.description}</option>
                                    ))
                                ) : (
                            <option value="" disabled>Ошибка загрузки кошельков</option> // ✅ Предотвращаем ошибку
                        )}
                    </Form.Select>
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>GUID автора</Form.Label> {/* ✅ Теперь вводится вручную */}
                <Form.Control type="text" value={authorId} onChange={(e) => setAuthorId(e.target.value)} required />
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>Категория</Form.Label>
                <Form.Select value={categoryId} onChange={(e) => setCategoryId(e.target.value)} required>
                    <option value="">Выберите категорию</option>
                    {categories.map(category => (
                        <option key={category.id} value={category.id}>{category.name}</option>
                    ))}
                </Form.Select>
            </Form.Group>

            <Button className="mt-3" type="submit" variant="success">Добавить расход</Button>
        </Form>
    );
}

export default ExpencesForm;
