import React from "react";
import { Container } from "react-bootstrap";
import FamilyCard from "./FamilyCard";

function FamilyList({ families }) {
    return (
        <Container>
            {families.map(family => (
                <FamilyCard key={family.id} family={family} />
            ))}
        </Container>
    );
}

export default FamilyList;
