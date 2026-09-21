import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import UsersPage from '../pages/users-page/UsersPage';
import LoginPage from '../pages/login-page/LoginPage';
import ForgotPasswordPage from '../pages/forgot-password-page/ForgotPasswordPage';
import ChangePasswordPage from '../pages/change-password-page/ChangePasswordPage';
import CoursesPage from '../pages/courses-page/CoursesPage';
import GroupsPage from '../pages/groups-page/GroupsPage';
import MentorProfilePage from '../pages/mentor-profile-page/MentorProfilePage';
import { ProtectedRoute } from '../features/auth';
import { AdminLayout } from '../widgets/admin-layout';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                <Route
                    path="/change-password"
                    element={
                        <ProtectedRoute>
                            <ChangePasswordPage />
                        </ProtectedRoute>
                    }
                />
                <Route
                    element={
                        <ProtectedRoute>
                            <AdminLayout />
                        </ProtectedRoute>
                    }
                >
                    <Route path="/" element={<UsersPage />} />
                    <Route path="/courses" element={<CoursesPage />} />
                    <Route path="/groups" element={<GroupsPage />} />
                    <Route path="/mentor-profile" element={<MentorProfilePage />} />
                </Route>
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
