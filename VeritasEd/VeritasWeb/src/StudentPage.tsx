import { useAuth } from "./AuthContext";

function StudentPage() {
  const { user } = useAuth();

  if (!user)
    return (
      <div className="app-container card-panel">
        You must be logged in to view this page.
      </div>
    );

  if (user.role !== "Student") {
    return (
      <div className="app-container card-panel">
        Unauthorized — you are logged in as {user.username} but not a student.
      </div>
    );
  }

  return (
    <div className="app-container card-panel">
      <h1>Student Dashboard</h1>
      <p>Welcome back, {user.username}.</p>
      <p>Here is student-only content.</p>
    </div>
  );
}

export default StudentPage;
