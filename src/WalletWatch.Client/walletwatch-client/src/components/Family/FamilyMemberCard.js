import React from "react";
import { Card } from "react-bootstrap";

function FamilyMemberCard({ member }) {
    return (
        <Card className="m-2 shadow-sm">
            <Card.Body>
                <Card.Title>{member.name}</Card.Title>
                <Card.Subtitle className="text-muted">{member.role}</Card.Subtitle>
                <small className="text-muted">ID пользователя: {member.userId}</small>
            </Card.Body>
        </Card>
    );
}

export default FamilyMemberCard;
