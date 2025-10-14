import React from "react";
import RegisterPage from "./pages/RegisterPage";
 import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
    import GamesListPage from "./pages/GamesListPage";
    import GameDetailsPage from "./pages/GameDetailsPage";

function App() {
    return <RegisterPage />;
   
   
    
        return (
            <Router>
                <Routes>
                    <Route path="/" element={<GamesListPage />} />
                    <Route path="/game/:id" element={<GameDetailsPage />} />
                </Routes>
            </Router>
        );
    }



export default App;