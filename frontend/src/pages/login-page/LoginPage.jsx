import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useLogin } from '../../features/auth';
import './LoginPage.css';

function LockBadgeIcon() {
    return (
        <svg viewBox="0 0 24 24" width="34" height="34" fill="none" stroke="currentColor" strokeWidth="1.8">
            <rect x="4" y="10" width="16" height="11" rx="3" />
            <path d="M8 10V7a4 4 0 0 1 8 0v3" />
            <circle cx="12" cy="15" r="1.6" fill="currentColor" stroke="none" />
        </svg>
    );
}

function UserIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
            <circle cx="12" cy="8" r="4" />
            <path d="M4 21c0-4 3.5-7 8-7s8 3 8 7" />
        </svg>
    );
}

function LockIcon() {
    return (
        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
            <rect x="4" y="10" width="16" height="11" rx="2" />
            <path d="M8 10V7a4 4 0 0 1 8 0v3" />
        </svg>
    );
}

function LoginPage() {
    const [phoneNumber, setPhoneNumber] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();
    const { mutate, isPending, error } = useLogin();

    const handleSubmit = (e) => {
        e.preventDefault();

        mutate(
            { phoneNumber, password },
            {
                onSuccess: (data) => {
                    navigate(data.mustChangePassword ? '/change-password' : '/');
                },
            }
        );
    };

    return (
        <main className="login-screen">
            <div className="neu-card">

                <div className="neu-icon-badge">
                    <LockBadgeIcon />
                </div>

                <div className="login-form-header">
                    <h1>Welcome <span>back</span></h1>
                    <p>Sign in to continue to your CRM account.</p>
                </div>

                <form onSubmit={handleSubmit}>
                    {error && (
                        <div className="login-error">
                            Invalid phone number or password
                        </div>
                    )}

                    <div className="field">
                        <label htmlFor="phone">Phone number</label>
                        <div className="input-wrapper">
                            <span className="input-icon"><UserIcon /></span>
                            <input
                                id="phone"
                                type="text"
                                placeholder="+992 90 000 00 00"
                                value={phoneNumber}
                                onChange={(e) => setPhoneNumber(e.target.value)}
                                autoComplete="tel"
                                autoFocus
                            />
                        </div>
                    </div>

                    <div className="field">
                        <label htmlFor="password">Password</label>
                        <div className="input-wrapper">
                            <span className="input-icon"><LockIcon /></span>
                            <input
                                id="password"
                                type="password"
                                placeholder="••••••••"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                autoComplete="current-password"
                            />
                        </div>
                    </div>

                    <button type="submit" className="login-submit" disabled={isPending}>
                        {isPending ? 'Signing in...' : 'Sign in'}
                    </button>

                    <div className="forgot-password">
                        <button type="button" onClick={() => navigate('/forgot-password')}>
                            Forgot password?
                        </button>
                    </div>
                </form>

            </div>
        </main>
    );
}

export default LoginPage;
