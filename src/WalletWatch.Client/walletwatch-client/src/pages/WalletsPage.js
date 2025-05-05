import { Container, Card } from "react-bootstrap";
import WalletListForm from "../components/Wallets/WalletListForm";

function WalletsPage() {
    return (
        <Container className="mt-4">
            <Card className="shadow-lg p-4">
                <Card.Body>
                    <WalletListForm /> 
                </Card.Body>
            </Card>
        </Container>
    );
}

export default WalletsPage;
