import React, { useState } from "react";

const RegisterPage = () => {
    const [form, setForm] = useState({ email: "", password: "", fullName: "" });
    const [message, setMessage] = useState("");
    const [token, setToken] = useState(localStorage.getItem("token") || null);

    const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage("");

        try {
            const response = await fetch("http://localhost:5000/api/auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form),
            });

            const data = await response.json();

            if (response.ok && data.success) {
                localStorage.setItem("token", data.token);
                setToken(data.token);
                setMessage("Реєстрація успішна!");
            } else {
                setMessage(data.message || "Помилка");
            }
        } catch {
            setMessage("Сервер недоступний");
        }
    };

    if (token) {
        return (
            <div>
                <h2>Ви авторизовані ✅</h2>
                <p>Ваш токен: {token}</p>
                <button onClick={() => { localStorage.removeItem("token"); setToken(null); }}>
                    Вийти
                </button>
            </div>
        );
    }

    return (
        <div style={{ maxWidth: "400px", margin: "0 auto" }}>
            <h2>Реєстрація</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" name="fullName" placeholder="Ім'я" value={form.fullName} onChange={handleChange} required />
                <input type="email" name="email" placeholder="Email" value={form.email} onChange={handleChange} required />
                <input type="password" name="password" placeholder="Пароль" value={form.password} onChange={handleChange} required />
                <button type="submit">Зареєструватися</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default RegisterPage;