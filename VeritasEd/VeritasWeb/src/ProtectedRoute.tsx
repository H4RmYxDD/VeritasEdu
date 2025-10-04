import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthContext";

type Props = {
  children: React.ReactElement;
  // optional list of allowed roles; if omitted any logged-in user is allowed
  allowedRoles?: string[];
};

export default function ProtectedRoute({ children, allowedRoles }: Props) {
  const { user } = useAuth();

  if (!user) return <Navigate to="/" replace />;

  if (
    allowedRoles &&
    allowedRoles.length > 0 &&
    !allowedRoles.includes(user.role)
  ) {
    // role mismatch — redirect to forbidden page
    return <Navigate to="/forbidden" replace />;
  }

  return children;
}
