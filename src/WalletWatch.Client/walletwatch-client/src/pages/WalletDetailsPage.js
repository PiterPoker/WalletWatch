import React, { useEffect, useState } from "react";
import {Link, useParams} from "react-router-dom";
import { Container, Card } from "react-bootstrap";
import { getWalletById } from "../services/WalletService";
import { getExpencesByWallet } from "../services/ExpencesService";

function WalletDetailsPage() {
    const { walletId } = useParams(); // ✅ Получаем ID из URL
    const [wallet, setWallet] = useState(null);
    const [expences, setExpences] = useState([]);

    useEffect(() => {
        async function fetchExpences() {
            try {
                const expences = await getExpencesByWallet(walletId);
                setExpences(expences);
            } catch (error) {
                console.error("Ошибка загрузки расходов:", error);
            }
        }
        fetchExpences();
    }, [walletId]);
    
    useEffect(() => {
        async function fetchWallet() {
            try {
                const wallet = await getWalletById(walletId);
                setWallet(wallet);
            } catch (error) {
                console.error("Ошибка загрузки кошелька:", error);
            }
        }
        fetchWallet();
    }, [walletId]);

    if (!wallet) {
        return <p>Загрузка...</p>;
    }

    return (
        <Container className="mt-4">
            <Card className="shadow-lg p-4">
                <Card.Body>
                    <Card.Title className="fw-bold">💳 {wallet.description}</Card.Title>
                    <Card.Subtitle className="text-muted">ID: {wallet.id}</Card.Subtitle>
                    <p><strong>Баланс:</strong> {wallet.balance} {wallet.currency}</p>

                    <p><strong>Семья:</strong> {wallet.family.name}
                        <Link to={`/families/${wallet.family.id}`} className="btn btn-primary btn-sm ms-2">
                            Перейти к семье
                        </Link>
                    </p>
                </Card.Body>
            </Card>
        </Container>
    );
}

export default WalletDetailsPage;
