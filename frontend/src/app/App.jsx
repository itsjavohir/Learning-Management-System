import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import UsersPage from '../pages/users-page/UsersPage';
import LoginPage from '../pages/login-page/LoginPage';
import ChangePasswordPage from '../pages/change-password-page/ChangePasswordPage';
import { ProtectedRoute } from '../features/auth';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route
                    path="/change-password"
                    element={
                        <ProtectedRoute>
                            <ChangePasswordPage />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/"
                    element={
                        <ProtectedRoute>
                            <UsersPage />
                        </ProtectedRoute>
                    }
                />
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
