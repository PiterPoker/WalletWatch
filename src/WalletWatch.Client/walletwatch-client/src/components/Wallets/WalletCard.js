import React from "react";
import { Card } from "react-bootstrap";
import { Link } from "react-router-dom";

function WalletCard({ wallet }) {
    return (
        <Card className="shadow-sm p-3 mb-3">
            <Card.Body>
                <Card.Title>{wallet.description}</Card.Title>
                <Card.Subtitle className="text-muted">Баланс: {wallet.balance} {wallet.currency}</Card.Subtitle>
            </Card.Body>
        </Card>
    );
}

export default WalletCard;
