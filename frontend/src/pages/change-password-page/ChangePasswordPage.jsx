import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authApi } from '../../entities/auth';

function ChangePasswordPage() {
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState(null);
    const [isPending, setIsPending] = useState(false);

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);

        if (newPassword !== confirmPassword) {
            setError('Passwords do not match');
            return;
        }

        setIsPending(true);
        try {
            await authApi.changePassword(
                oldPassword,
                newPassword,
                confirmPassword
            );
            navigate('/');
        } catch (err) {
            setError(err.response?.data?.message || 'Failed to change password');
        } finally {
            setIsPending(false);
        }
    };

    return (
        <main className="login-screen">
            <div className="login-card" style={{ maxWidth: 460 }}>
                <section className="login-form-side" style={{ flex: 1 }}>
                    <div className="login-form-header">
                        <h1>Change <span>password</span></h1>
                        <p>You must set a new password before continuing.</p>
                    </div>

                    <form onSubmit={handleSubmit}>
                        {error && <div className="login-error">{error}</div>}

                        <div className="field">
                            <label htmlFor="oldPassword">Current password</label>
                            <div className="input-wrapper">
                                <input
                                    id="oldPassword"
                                    type="password"
                                    value={oldPassword}
                                    onChange={(e) => setOldPassword(e.target.value)}
                                />
                            </div>
                        </div>

                        <div className="field">
                            <label htmlFor="newPassword">New password</label>
                            <div className="input-wrapper">
                                <input
                                    id="newPassword"
                                    type="password"
                                    value={newPassword}
                                    onChange={(e) => setNewPassword(e.target.value)}
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
                                />
                            </div>
                        </div>

                        <button type="submit" className="login-submit" disabled={isPending}>
                            {isPending ? 'Saving...' : 'Change password'}
                        </button>
                    </form>
                </section>
            </div>
        </main>
    );
}

export default ChangePasswordPage;