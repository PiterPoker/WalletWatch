import React from "react";
import { Table, Container } from "react-bootstrap";

function ExpencesList({ expences }) {
    return (
        <Container className="mt-4">
            <h3 className="fw-bold">💰 Список затрат</h3>

            {expences.length > 0 ? (
                <Table striped bordered hover>
                    <thead>
                    <tr>
                        <th>Дата</th>
                        <th>Описание</th>
                        <th>Сумма</th>
                        <th>Категория</th>
                        <th>Кошелек</th>
                    </tr>
                    </thead>
                    <tbody>
                    {expences.map(expence => (
                        <tr key={expence.id}>
                            <td>{new Date(expence.transactionDate).toLocaleDateString()}</td>
                            <td>{expence.description}</td>
                            <td>{expence.amount} {expence.currency}</td>
                            <td style={{ backgroundColor: expence.category.color }}>{expence.category.name}</td>
                            <td>{expence.wallet.name}</td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            ) : (
                <p className="text-muted">Расходы отсутствуют</p>
            )}
        </Container>
    );
}

export default ExpencesList;
