import { useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authApi, AUTH_CHANNEL, useAuthStore } from '../../entities/auth';
import AuthLayout from '../../widgets/AuthLayout/AuthLayout';
import './../login-page/LoginPage.css';

const CODE_LENGTH = 5;

function KeyBadgeIcon() {
    return (
        <svg viewBox="0 0 24 24" width="34" height="34" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="8" cy="15" r="4" />
            <path d="M11 12 20 3M20 3h-4M20 3v4" />
        </svg>
    );
}

function ShieldBadgeIcon() {
    return (
        <svg viewBox="0 0 24 24" width="34" height="34" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M12 3l7 3v6c0 4.5-3 7.5-7 9-4-1.5-7-4.5-7-9V6l7-3Z" />
            <path d="M9 12l2 2 4-4" />
        </svg>
    );
}

function maskContact(value) {
    if (!value) return '';
    if (value.includes('@')) {
        const [name, domain] = value.split('@');
        if (name.length <= 2) return `${name}***@${domain}`;
        return `${name.slice(0, 2)}***@${domain}`;
    }
    return value.length > 4 ? `${value.slice(0, -4).replace(/\d/g, 'X')}${value.slice(-4)}` : value;
}

function ForgotPasswordPage() {
    const [step, setStep] = useState('request'); // 'request' | 'reset'
    const [channel, setChannel] = useState(AUTH_CHANNEL.TELEGRAM);

    const [phoneNumber, setPhoneNumber] = useState('');
    const [email, setEmail] = useState('');

    const [code, setCode] = useState(Array(CODE_LENGTH).fill(''));
    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const [error, setError] = useState(null);
    const [isPending, setIsPending] = useState(false);

    const codeRefs = useRef([]);
    const navigate = useNavigate();
    const setSession = useAuthStore((state) => state.setSession);

    const contact = channel === AUTH_CHANNEL.TELEGRAM ? phoneNumber : email;

    const handleRequestCode = async (e) => {
        e.preventDefault();
        setError(null);

        if (channel === AUTH_CHANNEL.TELEGRAM && !phoneNumber.trim()) {
            setError('Enter your phone number');
            return;
        }
        if (channel === AUTH_CHANNEL.EMAIL && !email.trim()) {
            setError('Enter your email');
            return;
        }

        setIsPending(true);
        try {
            await authApi.forgotPassword({ channel, phoneNumber, email });
            // Backend response is intentionally the same whether the account
            // exists or not (anti-enumeration) — never say "user not found".
            setStep('reset');
            setTimeout(() => codeRefs.current[0]?.focus(), 0);
        } catch (err) {
            setError(err.response?.data?.message || 'Something went wrong, try again');
        } finally {
            setIsPending(false);
        }
    };

    const handleCodeChange = (index, rawValue) => {
        const digit = rawValue.replace(/\D/g, '').slice(-1);

        setCode((prev) => {
            const next = [...prev];
            next[index] = digit;
            return next;
        });

        if (digit && index < CODE_LENGTH - 1) {
            codeRefs.current[index + 1]?.focus();
        }
    };

    const handleCodeKeyDown = (index, e) => {
        if (e.key === 'Backspace' && !code[index] && index > 0) {
            codeRefs.current[index - 1]?.focus();
        }
    };

    const handleCodePaste = (e) => {
        const pasted = e.clipboardData.getData('text').replace(/\D/g, '').slice(0, CODE_LENGTH);
        if (!pasted) return;
        e.preventDefault();

        setCode((prev) => {
            const next = [...prev];
            for (let i = 0; i < CODE_LENGTH; i++) next[i] = pasted[i] || '';
            return next;
        });

        codeRefs.current[Math.min(pasted.length, CODE_LENGTH - 1)]?.focus();
    };

    const handleResetPassword = async (e) => {
        e.preventDefault();
        setError(null);

        const verifyCode = code.join('');

        if (verifyCode.length !== CODE_LENGTH) {
            setError(`Enter the ${CODE_LENGTH}-digit code`);
            return;
        }

        if (newPassword !== confirmPassword) {
            setError('Passwords do not match');
            return;
        }

        setIsPending(true);
        try {
            const data = await authApi.resetPassword({
                channel,
                phoneNumber,
                email,
                verifyCode,
                newPassword,
                confirmPassword,
            });

            // User is authenticated right away, same as a normal login
            setSession(data.user, data.accessToken, data.refreshToken);
            navigate(data.mustChangePassword ? '/change-password' : '/');
        } catch (err) {
            setError(err.response?.data?.message || 'Invalid or expired code');
        } finally {
            setIsPending(false);
        }
    };

    return (
        <AuthLayout>
            <div className="neu-card">

                {step === 'request' ? (
                    <>
                        <div className="neu-icon-badge">
                            <KeyBadgeIcon />
                        </div>

                        <div className="login-form-header">
                            <h1>Forgot <span>password</span></h1>
                            <p>Choose how you'd like to receive the code.</p>
                        </div>

                        <div className="channel-switch">
                            <button
                                type="button"
                                className={channel === AUTH_CHANNEL.TELEGRAM ? 'active' : ''}
                                onClick={() => setChannel(AUTH_CHANNEL.TELEGRAM)}
                            >
                                Telegram
                            </button>
                            <button
                                type="button"
                                className={channel === AUTH_CHANNEL.EMAIL ? 'active' : ''}
                                onClick={() => setChannel(AUTH_CHANNEL.EMAIL)}
                            >
                                Email
                            </button>
                        </div>

                        <form onSubmit={handleRequestCode}>
                            {error && <div className="login-error">{error}</div>}

                            {channel === AUTH_CHANNEL.TELEGRAM ? (
                                <div className="field">
                                    <label htmlFor="phoneNumber">Phone number</label>
                                    <div className="input-wrapper">
                                        <input
                                            id="phoneNumber"
                                            type="text"
                                            placeholder="+992 90 000 00 00"
                                            value={phoneNumber}
                                            onChange={(e) => setPhoneNumber(e.target.value)}
                                            autoComplete="tel"
                                            autoFocus
                                        />
                                    </div>
                                </div>
                            ) : (
                                <div className="field">
                                    <label htmlFor="email">Email</label>
                                    <div className="input-wrapper">
                                        <input
                                            id="email"
                                            type="email"
                                            placeholder="you@example.com"
                                            value={email}
                                            onChange={(e) => setEmail(e.target.value)}
                                            autoComplete="email"
                                            autoFocus
                                        />
                                    </div>
                                </div>
                            )}

                            <button type="submit" className="login-submit" disabled={isPending}>
                                {isPending ? 'Sending...' : 'Send code'}
                            </button>

                            <div className="forgot-password">
                                <button type="button" onClick={() => navigate('/login')}>
                                    Back to login
                                </button>
                            </div>
                        </form>
                    </>
                ) : (
                    <>
                        <div className="neu-icon-badge">
                            <ShieldBadgeIcon />
                        </div>

                        <div className="login-form-header">
                            <h1>Verify Your <span>Code</span></h1>
                            <p>
                                We've sent a {CODE_LENGTH}-digit code to<br />
                                <strong>{maskContact(contact) || 'your contact'}</strong>
                            </p>
                        </div>

                        <form onSubmit={handleResetPassword}>
                            {error && <div className="login-error">{error}</div>}

                            <div className="otp-inputs" onPaste={handleCodePaste}>
                                {code.map((digit, index) => (
                                    <input
                                        key={index}
                                        ref={(el) => (codeRefs.current[index] = el)}
                                        className="otp-box"
                                        type="text"
                                        inputMode="numeric"
                                        maxLength={1}
                                        value={digit}
                                        onChange={(e) => handleCodeChange(index, e.target.value)}
                                        onKeyDown={(e) => handleCodeKeyDown(index, e)}
                                        autoFocus={index === 0}
                                    />
                                ))}
                            </div>

                            <div className="field">
                                <label htmlFor="newPassword">New password</label>
                                <div className="input-wrapper">
                                    <input
                                        id="newPassword"
                                        type="password"
                                        value={newPassword}
                                        onChange={(e) => setNewPassword(e.target.value)}
                                        autoComplete="new-password"
                                    />
                                </div>
                            </div>

                            <div className="field">
                                <label htmlFor="confirmPassword">Confirm new password</label>
                                <div className="input-wrapper">
                                    <input
                                        id="confirmPassword"
                                        type="password"
                                        value={confirmPassword}
                                        onChange={(e) => setConfirmPassword(e.target.value)}
                                        autoComplete="new-password"
                                    />
                                </div>
                            </div>

                            <button type="submit" className="login-submit" disabled={isPending}>
                                {isPending ? 'Verifying...' : 'Verify & reset'}
                            </button>

                            <div className="forgot-password">
                                <button type="button" onClick={() => { setError(null); setCode(Array(CODE_LENGTH).fill('')); setStep('request'); }}>
                                    Resend code
                                </button>
                            </div>
                        </form>
                    </>
                )}

            </div>
        </AuthLayout>
    );
}

export default ForgotPasswordPage;
