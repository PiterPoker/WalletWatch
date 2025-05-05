import React from "react";
import { Card, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

function FamilyCard({ family }) {
    const navigate = useNavigate();

    const handleDetailsClick = () => {
        navigate(`/families/${family.id}`); 
    };


    return (
        <Card className="m-3 shadow-lg rounded">
            <Card.Body>
                <Card.Title className="fw-bold">{family.familyName}</Card.Title>
                {family.membersCount && <Card.Text>Число участников: {family.membersCount}</Card.Text>}
                <Button variant="outline-primary" onClick={handleDetailsClick}>Подробнее</Button>
            </Card.Body>
        </Card>
    );
}

export default FamilyCard;
