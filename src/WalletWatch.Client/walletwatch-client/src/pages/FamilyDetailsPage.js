import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getFamilyById, fetchFamilyMembers, getFamilyMembersByFamilyId } from "../services/FamilyService";
import { getWalletsByFamilyId } from "../services/WalletService";
import { Card, Container, Row, Col } from "react-bootstrap";
import FamilyMembersList from "../components/Family/FamilyMembersList";
import AddFamilyMemberForm from "../components/Family/AddFamilyMemberForm";
import AddWalletForm from "../components/Wallets/AddWalletForm";
import WalletCard from "../components/Wallets/WalletCard";

function FamilyDetailsPage() {
    const { familyId } = useParams();
    const [family, setFamily] = useState(null);
    const [familyMembers, setFamilyMembers] = useState([]);
    const [wallets, setWallets] = useState([]);

    useEffect(() => {
        async function loadFamilyMembers() {
            try {
                const members = await getFamilyMembersByFamilyId(familyId);
                const headMember = members.find(member => member.role === "Head") || null; // ✅ Ищем главу семьи

                setFamily(prevFamily => ({ ...prevFamily, headMember }));
                setFamilyMembers(members);
            } catch (error) {
                console.error("Ошибка загрузки участников семьи:", error);
            }
        }
        loadFamilyMembers();
    }, [familyId]);

    useEffect(() => {
        async function loadWallets() {
            try {
                const data = await getWalletsByFamilyId(familyId);
                setWallets(data);
            } catch (error) {
                console.error("Ошибка загрузки кошельков:", error);
            }
        }
        loadWallets();
    }, [familyId]);

    useEffect(() => {
        async function fetchFamily() {
            try {
                const data = await getFamilyById(familyId);
                console.log("Загруженная семья:", data);
                setFamily(data);
            } catch (error) {
                console.error("Ошибка загрузки семьи:", error);
            }
        }
        fetchFamily();
    }, [familyId]);
    
    if (!family) {
        return <p>Загрузка...</p>;
    }

    return (
        <Container className="mt-4">
            <Card className="shadow-lg p-4">
                <Card.Header className="bg-primary text-white fw-bold fs-5">
                    🏡 {family.familyName}
                </Card.Header>

                <Card.Body>
                    <Row className="g-4">
                        {/* Левая колонка — члены семьи */}
                        <Col md={6}>
                            <Card className="p-3 border-start border-success">
                                <h5 className="fw-bold">👨‍👩‍👧‍👦 Члены семьи</h5>

                                {/* Форма добавления участника */}
                                <AddFamilyMemberForm familyId={family.id} onMemberAdded={(newMember) => {
                                    setFamilyMembers([...familyMembers, newMember]);

                                    // ✅ Если добавленный участник - "Head", обновляем главу семьи
                                    if (newMember.role === "Head") {
                                        setFamily({ ...family, headMember: newMember });
                                    }
                                }} />

                                {/* Список членов семьи */}
                                <FamilyMembersList members={familyMembers} />
                            </Card>
                        </Col>

                        {/* Правая колонка — кошельки */}
                        <Col md={6}>
                            <Card className="p-3 border-start border-warning">
                                <h5 className="fw-bold">💳 Кошельки семьи</h5>

                                {/* Форма добавления кошелька */}
                                <AddWalletForm family={family} onWalletAdded={(newWallet) => {
                                    setWallets([...wallets, newWallet]); // ✅ Обновляем список кошельков
                                }} />

                                {/* Список кошельков */}
                                <Card className="p-3 mt-3 bg-light">
                                    {wallets.length > 0 ? (
                                        wallets.map(wallet => (
                                            <WalletCard key={wallet.id} wallet={wallet} />
                                        ))
                                    ) : (
                                        <p className="text-muted">Кошельки отсутствуют</p>
                                    )}
                                </Card>
                            </Card>
                        </Col>
                    </Row>
                </Card.Body>
            </Card>
        </Container>

    );
}

export default FamilyDetailsPage;
