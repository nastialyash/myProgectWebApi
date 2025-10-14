import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

function GameDetailsPage() {
    const { id } = useParams();
    const [game, setGame] = useState(null);

    useEffect(() => {
        fetch(`http://localhost:5000/api/game/${id}`)
            .then(res => res.json())
            .then(data => setGame(data.data))
            .catch(err => console.error(err));
    }, [id]);

    if (!game) {
        return <p>Завантаження...</p>;
    }

    return (
        <div style={{ padding: "20px" }}>
            <Link to="/">← Назад до списку</Link>
            <h2>{game.title}</h2>
            <p><strong>Жанр:</strong> {game.genre}</p>
            <p><strong>Опис:</strong> {game.description}</p>
            <p><strong>Ціна:</strong> ${game.price}</p>

            {game.imagePaths && game.imagePaths.length > 0 && (
                <div>
                    <h3>Зображення:</h3>
                    <div style={{ display: "flex", flexWrap: "wrap", gap: "10px" }}>
                        {game.imagePaths.map((img, index) => (
                            <img
                                key={index}
                                src={`http://localhost:5000/${img}`}
                                alt={`screenshot-${index}`}
                                width="200"
                            />
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
}

export default GameDetailsPage;