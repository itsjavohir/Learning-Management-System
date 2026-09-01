import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useLogin } from '../../features/auth';
import './LoginPage.css';

function UserIcon() {
    return (
        <svg
            viewBox="0 0 24 24"
            width="20"
            height="20"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
        >
            <circle cx="12" cy="8" r="4" />
            <path d="M4 21c0-4 3.5-7 8-7s8 3 8 7" />
        </svg>
    );
}

function LockIcon() {
    return (
        <svg
            viewBox="0 0 24 24"
            width="20"
            height="20"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
        >
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

            <div className="login-card">

                {/* ================= LEFT ================= */}

                <section className="login-panel">

                    <div className="login-brand">
                        CRM<span>.</span>
                    </div>

                    {/* Decorative planet */}
                    <div className="planet"></div>

                    {/* Orbit lines */}
                    <div className="orbit orbit-one"></div>
                    <div className="orbit orbit-two"></div>

                    {/* Orbit dots */}
                    <span className="orbit-dot dot-one"></span>
                    <span className="orbit-dot dot-two"></span>

                    <div className="login-panel-bottom">
                        <h2>
                            Manage your <span>CRM</span>
                        </h2>

                        <p>
                            Manage students, mentors and courses
                            in one place.
                        </p>
                    </div>

                </section>


                {/* ================= RIGHT ================= */}

                <section className="login-form-side">

                    <div className="login-form-header">

                        <h1>
                            <span>Welcome</span> back
                        </h1>

                        <p>
                            Sign in to continue to your account.
                        </p>

                    </div>


                    <form onSubmit={handleSubmit}>

                        {error && (
                            <div className="login-error">
                                Invalid phone number or password
                            </div>
                        )}


                        {/* PHONE */}

                        <div className="field">

                            <label htmlFor="phone">
                                Phone number
                            </label>

                            <div className="input-wrapper">

                                <span className="input-icon">
                                    <UserIcon />
                                </span>

                                <input
                                    id="phone"
                                    type="text"
                                    placeholder="+992 90 000 00 00"
                                    value={phoneNumber}
                                    onChange={(e) =>
                                        setPhoneNumber(e.target.value)
                                    }
                                    autoComplete="tel"
                                />

                            </div>

                        </div>


                        {/* PASSWORD */}

                        <div className="field">

                            <label htmlFor="password">
                                Password
                            </label>

                            <div className="input-wrapper">

                                <span className="input-icon">
                                    <LockIcon />
                                </span>

                                <input
                                    id="password"
                                    type="password"
                                    placeholder="••••••••"
                                    value={password}
                                    onChange={(e) =>
                                        setPassword(e.target.value)
                                    }
                                    autoComplete="current-password"
                                />

                            </div>

                        </div>


                        <div className="forgot-password">
                            <button type="button">
                                Forgot password?
                            </button>
                        </div>


                        <button
                            type="submit"
                            className="login-submit"
                            disabled={isPending}
                        >
                            {isPending ? 'Signing in...' : 'Sign in'}
                        </button>


                        {/* SOCIAL */}

                        <div className="divider">
                            <span></span>
                            <p>or continue with</p>
                            <span></span>
                        </div>


                        <div className="social-buttons">

                            <button
                                type="button"
                                className="social-button"
                                aria-label="Google"
                            >
                                G
                            </button>

                            <button
                                type="button"
                                className="social-button"
                                aria-label="GitHub"
                            >
                                <svg
                                    viewBox="0 0 24 24"
                                    width="21"
                                    height="21"
                                    fill="currentColor"
                                >
                                    <path d="M12 .5a12 12 0 0 0-3.79 23.39c.6.11.82-.26.82-.58v-2.05c-3.34.73-4.04-1.61-4.04-1.61-.55-1.4-1.34-1.77-1.34-1.77-1.09-.75.08-.74.08-.74 1.2.08 1.84 1.23 1.84 1.23 1.07 1.83 2.8 1.3 3.48.99.11-.77.42-1.3.76-1.6-2.67-.3-5.47-1.34-5.47-5.95 0-1.31.47-2.38 1.23-3.22-.12-.3-.53-1.52.12-3.18 0 0 1-.32 3.3 1.23a11.5 11.5 0 0 1 6 0c2.3-1.55 3.3-1.23 3.3-1.23.65 1.66.24 2.88.12 3.18.77.84 1.23 1.91 1.23 3.22 0 4.62-2.81 5.64-5.49 5.94.43.37.81 1.1.81 2.22v3.29c0 .32.22.7.83.58A12 12 0 0 0 12 .5Z" />
                                </svg>
                            </button>

                        </div>

                    </form>

                </section>

            </div>

        </main>
    );
}

export default LoginPage;
