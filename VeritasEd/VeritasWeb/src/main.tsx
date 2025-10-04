import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./index.css";
import MainPage from "./MainPage";
import AdminPage from "./AdminPage";
import StudentPage from "./StudentPage";
import ProtectedRoute from "./ProtectedRoute";
import { AuthProvider } from "./AuthContext";
import Forbidden from "./Forbidden";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route
            path="/admin"
            element={
              <ProtectedRoute allowedRoles={["Teacher"]}>
                <AdminPage />
              </ProtectedRoute>
            }
          />
          <Route
            path="/student"
            element={
              <ProtectedRoute allowedRoles={["Student"]}>
                <StudentPage />
              </ProtectedRoute>
            }
          />
          <Route path="/forbidden" element={<Forbidden />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  </StrictMode>
);
