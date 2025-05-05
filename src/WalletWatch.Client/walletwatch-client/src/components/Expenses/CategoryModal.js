import React from "react";
import { Modal, Button } from "react-bootstrap";
import CategoryForm from "./CategoryForm";
import CategoryListForm from "./CategoryListForm";

function CategoryModal({ show, onClose }) {
    return (
        <Modal show={show} onHide={onClose}>
            <Modal.Header closeButton>
                <Modal.Title>📂 Управление категориями</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {/* Форма добавления категории */}
                <CategoryForm onCategoryAdded={() => {}} />

                {/* Список категорий */}
                <CategoryListForm />
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClose}>Закрыть</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default CategoryModal;
