import { Navigate } from 'react-router-dom';
import { tokenStorage } from '../../../shared/lib/tokenStorage';

function ProtectedRoute({ children }) {
    if (!tokenStorage.isAuthenticated()) {
        return <Navigate to="/login" replace />;
    }
    return children;
}

export default ProtectedRoute;