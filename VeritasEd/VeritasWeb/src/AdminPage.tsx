import { useAuth } from "./AuthContext";

function AdminPage() {
  const { user } = useAuth();

  if (!user)
    return (
      <div className="app-container card-panel">
        You must be logged in to view this page.
      </div>
    );

  if (user.role !== "Teacher") {
    return (
      <div className="app-container card-panel">
        Unauthorized — you are logged in as {user.username} but not an admin.
      </div>
    );
  }

  return (
    <div className="app-container card-panel">
      <h1>Admin Dashboard</h1>
      <p>Welcome back, {user.username}.</p>
      <p>Here is admin-only content.</p>
    </div>
  );
}

export default AdminPage;
