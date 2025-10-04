import React, { createContext, useContext, useState } from "react";
import apiClient from "./apiClient";
import type { User } from "./types/User";

type AuthContextType = {
  user: User | null;
  login: (username: string, password: string) => Promise<User>;
  logout: () => void;
  loading: boolean;
  error: string | null;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<User | null>(() => {
    try {
      const raw = localStorage.getItem("currentUser");
      return raw ? (JSON.parse(raw) as User) : null;
    } catch {
      return null;
    }
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const login = async (username: string, password: string) => {
    setLoading(true);
    setError(null);
    try {
      const resp = await apiClient.post("/User/login", { username, password });
      const u: User | null = resp.data?.user ?? resp.data ?? null;
      if (!u) throw new Error("No user returned from login");
      setUser(u);
      localStorage.setItem("currentUser", JSON.stringify(u));
      return u;
    } catch (err: any) {
      const text =
        err?.response?.data?.message || err.message || "Login failed";
      setError(String(text));
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem("currentUser");
    setError(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, loading, error }}>
      {children}
    </AuthContext.Provider>
  );
};

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be used inside AuthProvider");
  return ctx;
}
