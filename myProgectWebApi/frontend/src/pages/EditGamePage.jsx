import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";

function EditGamePage() {
    const { id } = useParams(); 
    const navigate = useNavigate();

    const [game, setGame] = useState({
        title: "",
        genre: "",
        description: "",
        price: ""
    });

    const [selectedFiles, setSelectedFiles] = useState([]);

    
    useEffect(() => {
        fetch(`https://localhost:5001/api/Game/${id}`)
            .then(res => res.json())
            .then(data => setGame(data))
            .catch(err => console.error(err));
    }, [id]);

    
    const handleChange = (e) => {
        setGame({ ...game, [e.target.name]: e.target.value });
    };

    
    const handleFileChange = (e) => {
        setSelectedFiles(Array.from(e.target.files));
    };

    
    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("title", game.title);
        formData.append("genre", game.genre);
        formData.append("description", game.description);
        formData.append("price", game.price);

        for (const file of selectedFiles) {
            formData.append("images", file);
        }

        const response = await fetch(`https://localhost:5001/api/Game/${id}`, {
            method: "PUT",
            body: formData
        });

        if (response.ok) {
            alert("Game is succesesfull!");
            navigate("/games"); 
        } else {
            alert("Eroor for createv!");
        }
    };

    return (
        <div style={{ maxWidth: "600px", margin: "0 auto" }}>
            <h2>Edit game</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name:</label>
                    <input type="text" name="title" value={game.title} onChange={handleChange} />
                </div>

                <div>
                    <label>Genre:</label>
                    <input type="text" name="genre" value={game.genre} onChange={handleChange} />
                </div>

                <div>
                    <label>Description:</label>
                    <textarea name="description" value={game.description} onChange={handleChange}></textarea>
                </div>

                <div>
                    <label>Price:</label>
                    <input type="number" name="price" value={game.price} onChange={handleChange} />
                </div>

                <div>
                    <label>New images:</label>
                    <input type="file" multiple onChange={handleFileChange} />
                </div>

                <button type="submit">Save changes</button>
            </form>
        </div>
    );
}

export default EditGamePage;