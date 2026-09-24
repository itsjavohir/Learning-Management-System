import { useState } from 'react';
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import { useAuthStore } from '../../entities/auth';
import './Sidebar.css';

/* ---------- icons ---------- */

function DashboardIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <rect x="3.5" y="3.5" width="7.5" height="7.5" rx="1.6" />
            <rect x="13" y="3.5" width="7.5" height="7.5" rx="1.6" />
            <rect x="3.5" y="13" width="7.5" height="7.5" rx="1.6" />
            <rect x="13" y="13" width="7.5" height="7.5" rx="1.6" />
        </svg>
    );
}
function StudentsIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M12 4 2 9l10 5 10-5-10-5Z" />
            <path d="M6 11.5V17c0 1.4 2.7 3 6 3s6-1.6 6-3v-5.5" />
        </svg>
    );
}
function RewardsIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="12" cy="9" r="5.2" />
            <path d="M9 13.5 7.5 21 12 18.5 16.5 21 15 13.5" />
        </svg>
    );
}
function GroupsIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="8" cy="9" r="3" />
            <circle cx="16" cy="9" r="3" />
            <path d="M2 20c0-3 2.7-5.5 6-5.5s6 2.5 6 5.5" />
            <path d="M12.5 14.6c3 .3 5.5 2.6 5.5 5.4" />
        </svg>
    );
}
function EmployeesIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <rect x="3.5" y="7.5" width="17" height="12" rx="2" />
            <path d="M8.5 7.5V6a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v1.5" />
        </svg>
    );
}
function TimeTableIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="12" cy="12.5" r="8" />
            <path d="M12 8v4.5l3 2" />
            <path d="M9 2.5h6" />
        </svg>
    );
}
function CoursesIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M4 5.5c2-1 6-1 8 0v13c-2-1-6-1-8 0v-13Z" />
            <path d="M20 5.5c-2-1-6-1-8 0v13c2-1 6-1 8 0v-13Z" />
        </svg>
    );
}
function AdministrationIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M12 3l7 3v6c0 4.5-3 7.5-7 9-4-1.5-7-4.5-7-9V6l7-3Z" />
        </svg>
    );
}
function BranchesIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M12 21s7-6.5 7-11.5A7 7 0 0 0 5 9.5C5 14.5 12 21 12 21Z" />
            <circle cx="12" cy="9.5" r="2.4" />
        </svg>
    );
}
function SmsIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M4 5.5h16v10.5H9l-4 3.5V16H4V5.5Z" />
        </svg>
    );
}
function AccountingIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <rect x="3.5" y="6" width="17" height="12" rx="2.2" />
            <circle cx="12" cy="12" r="2.4" />
        </svg>
    );
}
function ProfileIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="12" cy="8" r="4" />
            <path d="M4 21c0-4.4 3.6-8 8-8s8 3.6 8 8" />
        </svg>
    );
}
function LogoutIcon() {
    return (
        <svg viewBox="0 0 24 24" width="17" height="17" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M9 4H6a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h3" />
            <path d="M15 16l4-4-4-4" />
            <path d="M19 12H9" />
        </svg>
    );
}
function ChevronIcon() {
    return (
        <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" strokeWidth="2.2">
            <path d="m9 6 6 6-6 6" />
        </svg>
    );
}

/* ---------- nav structure ---------- */

const NAV_GROUPS = [
    {
        label: 'Main',
        items: [
            { to: '/', label: 'Dashboard', end: true, icon: DashboardIcon },
            {
                label: 'Students',
                icon: StudentsIcon,
                children: [
                    { to: '/users', label: 'All students' },
                    { to: '/students/graduates', label: 'Graduates' },
                    { to: '/students/enroll', label: 'Enroll' },
                    { to: '/students/left-courses', label: 'Left Courses' },
                ],
            },
            { to: '/rewards', label: 'Rewards', icon: RewardsIcon },
            { to: '/groups', label: 'Groups', icon: GroupsIcon },
            { to: '/employees', label: 'Employees', icon: EmployeesIcon },
            { to: '/timetable', label: 'TimeTable', icon: TimeTableIcon },
            { to: '/courses', label: 'Courses', icon: CoursesIcon },
            { to: '/administration', label: 'Administration', icon: AdministrationIcon },
            { to: '/branches', label: 'Branches', icon: BranchesIcon },
            { to: '/sms-mailings', label: 'SMS mailings', icon: SmsIcon },
            { to: '/accounting', label: 'Accounting', icon: AccountingIcon },
        ],
    },
];

function CollapsibleNavItem({ item }) {
    const location = useLocation();
    const hasActiveChild = item.children.some((child) => location.pathname === child.to);
    const [isOpen, setIsOpen] = useState(hasActiveChild);
    const Icon = item.icon;

    return (
        <div className="sidebar-collapsible">
            <button
                type="button"
                className={hasActiveChild ? 'sidebar-link sidebar-link--toggle active-parent' : 'sidebar-link sidebar-link--toggle'}
                onClick={() => setIsOpen((prev) => !prev)}
            >
                <Icon />
                <span>{item.label}</span>
                <span className={isOpen ? 'sidebar-chevron sidebar-chevron--open' : 'sidebar-chevron'}>
                    <ChevronIcon />
                </span>
            </button>

            {isOpen && (
                <div className="sidebar-submenu">
                    {item.children.map((child) => (
                        <NavLink
                            key={child.to}
                            to={child.to}
                            className={({ isActive }) =>
                                isActive ? 'sidebar-sublink active' : 'sidebar-sublink'
                            }
                        >
                            {child.label}
                        </NavLink>
                    ))}
                </div>
            )}
        </div>
    );
}

function Sidebar() {
    const navigate = useNavigate();
    const clearSession = useAuthStore((state) => state.clearSession);

    const handleLogout = () => {
        clearSession();
        navigate('/login');
    };

    return (
        <aside className="sidebar">
            <div className="sidebar-brand">
                <span className="sidebar-logo">C</span>
                <div className="sidebar-brand-text">
                    <span className="sidebar-brand-name">CRM</span>
                    <span className="sidebar-brand-badge">Admin</span>
                </div>
            </div>

            <nav className="sidebar-nav">
                {NAV_GROUPS.map((group) => (
                    <div className="sidebar-group" key={group.label}>
                        <p className="sidebar-group-label">{group.label}</p>
                        {group.items.map((item) => {
                            if (item.children) {
                                return <CollapsibleNavItem item={item} key={item.label} />;
                            }

                            const Icon = item.icon;
                            return (
                                <NavLink
                                    key={item.to}
                                    to={item.to}
                                    end={item.end}
                                    className={({ isActive }) =>
                                        isActive ? 'sidebar-link active' : 'sidebar-link'
                                    }
                                >
                                    <Icon />
                                    <span>{item.label}</span>
                                </NavLink>
                            );
                        })}
                    </div>
                ))}
            </nav>

            <button type="button" className="sidebar-logout" onClick={handleLogout}>
                <LogoutIcon />
                <span>Sign Out</span>
            </button>
        </aside>
    );
}

export default Sidebar;
