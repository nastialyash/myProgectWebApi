import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function GamesListPage() {
    const [games, setGames] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        fetch("http://localhost:5000/api/game/all")
            .then(res => res.json())
            .then(data => setGames(data.data || []))
            .catch(err => console.error(err));
    }, []);

    return (
        <div style={{ padding: "20px" }}>
            <h2>Список ігор</h2>
            {games.map(game => (
                <div key={game.id} style={{ marginBottom: "10px", border: "1px solid #ccc", padding: "10px" }}>
                    <h3>{game.title}</h3>
                    <p>Жанр: {game.genre}</p>
                    <button onClick={() => navigate(`/game/${game.id}`)}>Переглянути</button>
                </div>
            ))}
        </div>
    );
}

export default GamesListPage;