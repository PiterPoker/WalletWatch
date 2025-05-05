import React, { useEffect, useState } from "react";
import { getCategories } from "../../services/ExpencesService";
import { Table, Container } from "react-bootstrap";

function CategoryListForm() {
    const [categories, setCategories] = useState([]); // ✅ Инициализируем пустым массивом

    useEffect(() => {
        async function fetchCategories() {
            try {
                const data = await getCategories();
                setCategories(data || []); // ✅ Гарантируем массив, даже если `undefined`
            } catch (error) {
                console.error("Ошибка загрузки категорий:", error);
                setCategories([]); // ✅ Предотвращаем `undefined`
            }
        }
        fetchCategories();
    }, []);

    return (
        <Container className="mt-4">
            <h3 className="fw-bold">📂 Список категорий расходов</h3>

            {categories?.length > 0 ? ( // ✅ Проверяем наличие массива
                <Table striped bordered hover>
                    <thead>
                    <tr>
                        <th>Название</th>
                        <th>Цвет</th>
                    </tr>
                    </thead>
                    <tbody>
                    {categories.map(category => (
                        <tr key={category.id}>
                            <td>{category.name}</td>
                            <td>
                                    <span style={{ backgroundColor: category.color, padding: "5px 15px", borderRadius: "5px", display: "inline-block" }}>
                                        {category.color}
                                    </span>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            ) : (
                <p className="text-muted">Категории отсутствуют</p>
            )}
        </Container>
    );
}

export default CategoryListForm;
