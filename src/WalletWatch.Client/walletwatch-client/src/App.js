import "bootstrap/dist/css/bootstrap.min.css"; // ✅ Bootstrap стили
import { Routes, Route } from "react-router-dom";
import AppNavbar from "./components/Navbar";
import HomePage from "./pages/HomePage";
import FamilyPage from "./pages/FamilyPage";
import FamilyDetailsPage from "./pages/FamilyDetailsPage";
import WalletsPage from "./pages/WalletsPage";
import WalletDetailsPage from "./pages/WalletDetailsPage";
import ExpensesPage from "./pages/ExpencesPage";


function App() {
    return (
        <>
            <AppNavbar />
            <div className="container mt-4">
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/families" element={<FamilyPage />} />
                    <Route path="/families/:familyId" element={<FamilyDetailsPage />} />
                    <Route path="/wallets" element={<WalletsPage />} />
                    <Route path="/wallets/:walletId" element={<WalletDetailsPage />} />
                    <Route path="/expenses" element={<ExpensesPage />} />
                </Routes>
            </div>
        </>
    );
}

export default App;
