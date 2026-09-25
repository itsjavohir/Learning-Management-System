import { Link, useLocation } from 'react-router-dom';
import { useColorMode } from '../../shared/lib/useColorMode';
import './Topbar.css';

const PAGE_TITLES = {
    '/': 'Dashboard',
    '/users': 'Students',
    '/students/graduates': 'Graduates',
    '/students/enroll': 'Enroll',
    '/students/left-courses': 'Left Courses',
    '/rewards': 'Rewards',
    '/groups': 'Groups',
    '/employees': 'Employees',
    '/timetable': 'TimeTable',
    '/courses': 'Courses',
    '/administration': 'Administration',
    '/branches': 'Branches',
    '/sms-mailings': 'SMS mailings',
    '/accounting': 'Accounting',
    '/mentor-profile': 'Mentor Profile',
};

function SearchIcon() {
    return (
        <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2">
            <circle cx="11" cy="11" r="7" />
            <path d="m20 20-3.5-3.5" />
        </svg>
    );
}

function BellIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M6 9a6 6 0 0 1 12 0c0 4 1.5 5.5 1.5 5.5H4.5S6 13 6 9Z" />
            <path d="M10 19a2 2 0 0 0 4 0" />
        </svg>
    );
}

function SunIcon() {
    return (
        <svg viewBox="0 0 24 24" width="17" height="17" fill="none" stroke="currentColor" strokeWidth="1.9">
            <circle cx="12" cy="12" r="4.2" />
            <path d="M12 2.5v2.4M12 19v2.4M4.5 12H2M22 12h-2.5M5.6 5.6l1.7 1.7M16.7 16.7l1.7 1.7M18.4 5.6l-1.7 1.7M7.3 16.7l-1.7 1.7" />
        </svg>
    );
}

function MoonIcon() {
    return (
        <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="1.9">
            <path d="M20 14.5A8.5 8.5 0 1 1 9.5 4a7 7 0 0 0 10.5 10.5Z" />
        </svg>
    );
}

function ChevronDownIcon() {
    return (
        <svg viewBox="0 0 24 24" width="12" height="12" fill="none" stroke="currentColor" strokeWidth="2.4">
            <path d="m6 9 6 6 6-6" />
        </svg>
    );
}

function Topbar() {
    const location = useLocation();
    const pageTitle = PAGE_TITLES[location.pathname] || 'Dashboard';
    const { mode, toggle } = useColorMode();

    return (
        <header className="topbar">
            <div className="topbar-breadcrumb">
                <span>Platform</span>
                <span className="topbar-breadcrumb-sep">/</span>
                <span className="topbar-breadcrumb-current">{pageTitle}</span>
            </div>

            <div className="topbar-actions">
                <div className="topbar-search">
                    <SearchIcon />
                    <input type="text" placeholder="Quick search..." disabled />
                </div>

                {/* TODO backend/i18n: no translation layer yet — visual only */}
                <button type="button" className="topbar-lang" disabled title="Coming soon">
                    <span>ENG</span>
                    <ChevronDownIcon />
                </button>

                <button
                    type="button"
                    className="topbar-icon-btn"
                    onClick={toggle}
                    title={mode === 'dark' ? 'Switch to light mode' : 'Switch to dark mode'}
                >
                    {mode === 'dark' ? <SunIcon /> : <MoonIcon />}
                </button>

                <button type="button" className="topbar-icon-btn" title="Notifications (soon)" disabled>
                    <BellIcon />
                </button>

                <Link to="/mentor-profile" className="topbar-user" aria-label="Open profile">
                    <span className="topbar-user-avatar">A</span>
                    <div className="topbar-user-info">
                        <span className="topbar-user-name">Admin</span>
                        <span className="topbar-user-role">Administrator</span>
                    </div>
                </Link>
            </div>
        </header>
    );
}

export default Topbar;
