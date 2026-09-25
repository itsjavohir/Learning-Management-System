import './AuthLayout.css';
import SeasonDecoration from './SeasonDecoration/SeasonDecoration';
import backgroundImage from './backgrounds/winter-village-vector-background.jpg';

function AuthLayout({ children }) {
    return (
        <main className="auth-layout">

            {/* Background image */}
            <div
                className="auth-layout__background"
                style={{
                    '--season-background-image': `url(${backgroundImage})`,
                }}
            />

            {/* Background overlay */}
            <div className="auth-layout__overlay" />

            {/* Seasonal effects */}
            <SeasonDecoration />

            {/* Login / Forgot / Reset / Change Password */}
            <div className="auth-layout__content">
                {children}
            </div>

        </main>
    );
}

export default AuthLayout;