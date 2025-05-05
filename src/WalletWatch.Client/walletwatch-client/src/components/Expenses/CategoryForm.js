import React, { useState } from "react";
import { createCategory, createAuthor } from "../../services/ExpencesService";
import { Form, Button } from "react-bootstrap";

function CategoryForm({ onCategoryAdded }) {
    const [name, setName] = useState("");
    const [color, setColor] = useState("red");
    const [authorName, setAuthorName] = useState(""); // ✅ Поле для имени автора

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            // ✅ Создаём автора и получаем его `id`
            const author = await createAuthor(authorName);
            if (!author) {
                console.error("Ошибка создания автора!");
                return;
            }

            // ✅ Создаём категорию с `authorId`
            const newCategory = { name, color, authorId: author.id };
            const createdCategory = await createCategory(newCategory);

            if (createdCategory) {
                onCategoryAdded(createdCategory);
                setName("");
                setColor("red");
                setAuthorName(""); // ✅ Очищаем поля после добавления
            }
        } catch (error) {
            console.error("Ошибка при создании категории:", error);
        }
    };

    return (
        <Form onSubmit={handleSubmit} className="mb-3">
            <Form.Group>
                <Form.Label>Имя автора</Form.Label>
                <Form.Control type="text" value={authorName} onChange={(e) => setAuthorName(e.target.value)} required />
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>Название категории</Form.Label>
                <Form.Control type="text" value={name} onChange={(e) => setName(e.target.value)} required />
            </Form.Group>

            <Form.Group className="mt-2">
                <Form.Label>Цвет категории</Form.Label>
                <Form.Select value={color} onChange={(e) => setColor(e.target.value)}>
                    <option value="red">🔴 Красный</option>
                    <option value="yellow">🟡 Желтый</option>
                    <option value="green">🟢 Зеленый</option>
                </Form.Select>
            </Form.Group>

            <Button className="mt-3" type="submit" variant="primary">Создать автора и категорию</Button>
        </Form>
    );
}

export default CategoryForm;
