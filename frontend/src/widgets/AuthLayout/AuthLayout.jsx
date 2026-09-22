import './AuthLayout.css';
import SeasonDecoration from './SeasonDecoration/SeasonDecoration';

// Shared shell for every auth page (login, forgot-password, change-password):
// seasonal background color + a lazy-loaded decorative effect behind the form.
// No form logic lives here — only layout and the decoration slot.
function AuthLayout({ children }) {
    return (
        <main className="auth-layout">
            <SeasonDecoration />
            <div className="auth-layout__content">
                {children}
            </div>
        </main>
    );
}

export default AuthLayout;
