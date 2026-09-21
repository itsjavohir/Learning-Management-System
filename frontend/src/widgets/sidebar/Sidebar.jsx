import { NavLink, useNavigate } from 'react-router-dom';
import { useAuthStore } from '../../entities/auth';
import './Sidebar.css';

const NAV_ITEMS = [
    { to: '/', label: 'Users', end: true },
    { to: '/courses', label: 'Courses' },
    { to: '/groups', label: 'Groups' },
    { to: '/mentor-profile', label: 'Mentor Profile' },
];

function Sidebar() {
    const navigate = useNavigate();
    const clearSession = useAuthStore((state) => state.clearSession);

    const handleLogout = () => {
        clearSession();
        navigate('/login');
    };

    return (
        <aside className="sidebar">
            <p className="sidebar-brand">
                CRM <span>Admin</span>
            </p>

            <nav className="sidebar-nav">
                {NAV_ITEMS.map((item) => (
                    <NavLink
                        key={item.to}
                        to={item.to}
                        end={item.end}
                        className={({ isActive }) =>
                            isActive ? 'sidebar-link active' : 'sidebar-link'
                        }
                    >
                        {item.label}
                    </NavLink>
                ))}
            </nav>

            <button type="button" className="sidebar-logout" onClick={handleLogout}>
                Logout
            </button>
        </aside>
    );
}

export default Sidebar;
