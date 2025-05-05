import React, { useEffect, useState } from "react";
import { Table, Container, Button } from "react-bootstrap";
import { getWallets } from "../../services/WalletService";
import { Link } from "react-router-dom";

function WalletListForm() {
    const [wallets, setWallets] = useState([]);

    useEffect(() => {
        async function fetchWallets() {
            try {
                const data = await getWallets();
                console.log(data);
                setWallets(data.items);
            } catch (error) {
                console.error("Ошибка загрузки кошельков:", error);
            }
        }
        fetchWallets();
    }, []);

    return (
        <Container className="mt-4">
            <h2 className="fw-bold">💰 Список кошельков</h2>

            {wallets.length > 0 ? (
                <Table striped bordered hover>
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Описание</th>
                        <th>Баланс</th>
                        <th>Валюта</th>
                        <th>Действия</th>
                    </tr>
                    </thead>
                    <tbody>
                    {wallets.map(wallet => (
                        <tr key={wallet.id}>
                            <td>{wallet.id}</td>
                            <td>{wallet.description}</td>
                            <td>{wallet.balance}</td>
                            <td>{wallet.currency}</td>
                            <td>
                                <Link to={`/wallets/${wallet.id}`}
                                      className="btn btn-info btn-sm">Подробнее</Link> {/* ✅ Добавляем ссылку */}
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            ) : (
                <p className="text-muted">Кошельки отсутствуют</p>
            )}
        </Container>
    );
}

export default WalletListForm;
