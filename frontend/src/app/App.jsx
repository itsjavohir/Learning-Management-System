import { BrowserRouter, Routes, Route } from 'react-router-dom';
import UsersPage from '../pages/users-page/UsersPage';
import LoginPage from '../pages/login-page/LoginPage';
import ProtectedRoute from '../features/auth/ui/ProtectedRoute';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route
                    path="/"
                    element={
                        <ProtectedRoute>
                            <UsersPage />
                        </ProtectedRoute>
                    }
                />
            </Routes>
        </BrowserRouter>
    );
}

export default App;