import { Link } from 'react-router-dom';
import { useThemeStore } from '../../entities/theme';
import './SettingsPage.css';

const SETTINGS_CARDS = [
    { title: 'Theme', path: '/settings/theme', description: 'Colors, season, layout tone', accent: 'violet' },
    { title: 'Security', path: '/settings/theme', description: 'Passwords and access', accent: 'blue' },
    { title: 'Notifications', path: '/settings/theme', description: 'Alerts and reminders', accent: 'amber' },
    { title: 'Language', path: '/settings/theme', description: 'Interface language', accent: 'green' },
    { title: 'Profile', path: '/settings/theme', description: 'User preferences', accent: 'rose' },
    { title: 'System', path: '/settings/theme', description: 'Platform defaults', accent: 'slate' },
];

function SettingsCard({ title, description, accent, path }) {
    return (
        <Link to={path} className={`settings-card settings-card--${accent}`}>
            <span className="settings-card-icon">{title.slice(0, 1)}</span>
            <div>
                <h3>{title}</h3>
                <p>{description}</p>
            </div>
            <span className="settings-card-arrow">→</span>
        </Link>
    );
}

function SettingsPage() {
    const { season, enabled } = useThemeStore();

    return (
        <div className="settings-page">
            <div className="settings-page-header">
                <div>
                    <p className="settings-kicker">System</p>
                    <h1>Settings</h1>
                </div>
                <div className="settings-header-badge">
                    <span className="settings-header-dot" />
                    {enabled ? 'Season on' : 'Season off'}
                </div>
            </div>

            <div className="settings-summary">
                <div className="settings-summary-card">
                    <span>Active season</span>
                    <strong>{season || 'Auto'}</strong>
                </div>
                <div className="settings-summary-card">
                    <span>Theme mode</span>
                    <strong>Adaptive</strong>
                </div>
                <div className="settings-summary-card">
                    <span>Sections</span>
                    <strong>06</strong>
                </div>
            </div>

            <div className="settings-grid">
                {SETTINGS_CARDS.map((card) => (
                    <SettingsCard key={card.title} {...card} />
                ))}
            </div>
        </div>
    );
}

export default SettingsPage;
