import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./App.css";
import { useAuth } from "./AuthContext";
import apiClient from "./apiClient";

function MainPage() {
  const navigate = useNavigate();
  const {
    user: currentUser,
    login: authLogin,
    logout: authLogout,
    error: authError,
  } = useAuth();
  // component state
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState<string | null>(null);

  // login handler
  const login = async (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    setMessage(null);
    setLoading(true);
    try {
      const user = await authLogin(username, password);
      setMessage("Login successful");
      if (user.role === "Teacher") {
        navigate("/admin");
        return;
      } else if (user.role === "Student") {
        navigate("/student");
        return;
      }
    } catch (err: any) {
      console.error(err);
      const text =
        err?.response?.data?.message || err.message || "Login failed";
      setMessage(String(text));
    } finally {
      setLoading(false);
    }
  };

  // register handler
  const register = async () => {
    setMessage(null);
    setLoading(true);
    try {
      const resp = await apiClient.post("/register", { username, password });
      setMessage("Registration successful. You can now log in.");
      console.log("register response:", resp.data);
    } catch (err: any) {
      console.error(err);
      const text =
        err?.response?.data?.message || err.message || "Registration failed";
      setMessage(String(text));
    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    authLogout();
    navigate("/");
  };

  return (
    <div className="app-container">
      <header className="app-header">
        <div className="brand">VeritasWeb</div>
        {currentUser ? (
          <>
            <div>Signed in as {currentUser.username}</div>
            {currentUser.role === "Teacher" && (
              <button onClick={() => navigate("/admin")}>Admin</button>
            )}
            {currentUser.role === "Student" && (
              <button onClick={() => navigate("/student")}>Student</button>
            )}
            <button onClick={logout}>Logout</button>
          </>
        ) : (
          <div style={{ marginLeft: "auto" }}>Please log in</div>
        )}
      </header>

      <div className="card-panel">
        <form onSubmit={login} style={{ display: "grid", gap: 8 }}>
          <input
            type="text"
            id="username"
            placeholder="Please fill in your username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="password"
            name="password"
            id="password"
            placeholder="Please fill in your password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <div className="form-actions">
            <button type="submit" disabled={loading}>
              {loading ? "Please wait..." : "Login"}
            </button>
            <button type="button" onClick={register} disabled={loading}>
              Register
            </button>
          </div>
          {message && (
            <div className="status" role="status">
              {message}
            </div>
          )}
          {authError && (
            <div className="status" role="alert">
              {authError}
            </div>
          )}
        </form>
      </div>
    </div>
  );
}

export default MainPage;
