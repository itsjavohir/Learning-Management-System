import { useNavigate } from 'react-router-dom';
import { SEASONS, useThemeStore } from '../../entities/theme';
import './SettingsPage.css';

function ThemeSettingsPage() {
    const navigate = useNavigate();
    const { season, enabled, event, setEnabled, setManualTheme, syncWithDate } = useThemeStore();

    return (
        <div className="settings-page">
            <div className="settings-page-header">
                <div>
                    <p className="settings-kicker">Theme</p>
                    <h1>Appearance settings</h1>
                </div>
                <button type="button" className="settings-back-btn" onClick={() => navigate('/settings')}>
                    ← Back to settings
                </button>
            </div>

            <div className="settings-detail-panel">
                <div className="settings-card settings-card--violet settings-card--detail">
                    <span className="settings-card-icon">T</span>
                    <div>
                        <h3>Theme</h3>
                        <p>Adaptive styling backed by the current season.</p>
                    </div>
                </div>

                <div className="settings-option-group">
                    <label className="settings-option">
                        <span>Season decorations</span>
                        <input
                            type="checkbox"
                            checked={enabled}
                            onChange={(e) => setEnabled(e.target.checked)}
                        />
                    </label>

                    <label className="settings-option settings-option--select">
                        <span>Season</span>
                        <select
                            value={season || 'auto'}
                            onChange={(e) => e.target.value === 'auto' ? syncWithDate() : setManualTheme(e.target.value, event)}
                        >
                            <option value="auto">Auto</option>
                            {Object.values(SEASONS).map((seasonValue) => (
                                <option key={seasonValue} value={seasonValue}>{seasonValue}</option>
                            ))}
                        </select>
                    </label>
                </div>

                <div className="settings-preview-card">
                    <div className="settings-preview-header">
                        <span>Preview</span>
                        <strong>{enabled ? (season || 'Auto') : 'Off'}</strong>
                    </div>
                    <div className="settings-preview-swatch">
                        <div className="settings-preview-main" />
                        <div className="settings-preview-accent" />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ThemeSettingsPage;
