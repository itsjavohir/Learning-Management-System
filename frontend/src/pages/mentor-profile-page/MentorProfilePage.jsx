import { useEffect, useState } from 'react';
import { useMentorProfile } from '../../entities/mentor';
import { useUpdateMentorProfile } from '../../features/mentor';
import '../users-page/UsersPage.css';
import './MentorProfilePage.css';

const EMPTY_FORM = {
    specialization: '',
    bio: '',
    experienceYears: '',
};

function MentorProfilePage() {
    const [form, setForm] = useState(EMPTY_FORM);
    const { data: profile, isLoading, isError, error } = useMentorProfile();
    const updateProfile = useUpdateMentorProfile();

    useEffect(() => {
        if (!profile) return;

        setForm({
            specialization: profile.specialization ?? '',
            bio: profile.bio ?? '',
            experienceYears: profile.experienceYears ?? '',
        });
    }, [profile]);

    const handleSubmit = (e) => {
        e.preventDefault();

        updateProfile.mutate({
            specialization: form.specialization || null,
            bio: form.bio || null,
            experienceYears: Number(form.experienceYears),
        });
    };

    if (isLoading) return <p style={{ padding: 40 }}>Loading...</p>;
    if (isError) {
        return (
            <p style={{ padding: 40, color: 'var(--color-danger)' }}>
                Error: {error?.response?.data?.message || error?.message}
            </p>
        );
    }

    const displayName = [profile?.firstName, profile?.lastName].filter(Boolean).join(' ') || 'Admin';
    const initials = displayName.split(' ').map((part) => part[0]).slice(0, 2).join('').toUpperCase() || 'A';

    return (
        <div className="mentor-profile-page">
            <div className="mentor-profile-cover" />

            <div className="mentor-profile-shell">
                <div className="mentor-profile-header">
                    <div className="mentor-profile-avatar-wrap">
                        <div className="mentor-profile-avatar">{initials}</div>
                    </div>

                    <div className="mentor-profile-summary">
                        <div className="mentor-profile-topline">
                            <div>
                                <p className="mentor-profile-kicker">Mentor profile</p>
                                <h1>{displayName}</h1>
                            </div>
                            <button type="button" className="mentor-profile-btn mentor-profile-btn--primary">
                                Edit profile
                            </button>
                        </div>

                        <p className="mentor-profile-handle">@{(profile?.email || 'admin').split('@')[0]}</p>
                        <p className="mentor-profile-bio">
                            {form.bio || 'Helping students grow with practical learning, structure, and real mentorship.'}
                        </p>

                        <div className="mentor-profile-stats">
                            <div><strong>128</strong><span>Students</span></div>
                            <div><strong>12</strong><span>Programs</span></div>
                            <div><strong>7 yrs</strong><span>Experience</span></div>
                        </div>
                    </div>
                </div>

                <div className="mentor-profile-grid">
                    <div className="mentor-profile-card mentor-profile-panel">
                        <div className="mentor-profile-panel-header">
                            <h3>About</h3>
                            <span>Profile</span>
                        </div>

                        <form onSubmit={handleSubmit} className="mentor-profile-form">
                            {updateProfile.isError && (
                                <div className="create-form-error">
                                    {updateProfile.error?.response?.data?.message || 'Error updating profile'}
                                </div>
                            )}
                            {updateProfile.isSuccess && (
                                <div className="create-form-success">Profile saved</div>
                            )}

                            <div className="mentor-profile-form-row">
                                <label className="mentor-field">
                                    <span>Specialization</span>
                                    <input
                                        placeholder="Specialization"
                                        value={form.specialization}
                                        onChange={(e) => setForm({ ...form, specialization: e.target.value })}
                                    />
                                </label>

                                <label className="mentor-field">
                                    <span>Experience</span>
                                    <input
                                        type="number"
                                        min="0"
                                        placeholder="Experience (years)"
                                        value={form.experienceYears}
                                        onChange={(e) => setForm({ ...form, experienceYears: e.target.value })}
                                    />
                                </label>
                            </div>

                            <label className="mentor-field mentor-field--full">
                                <span>Bio</span>
                                <textarea
                                    placeholder="Bio"
                                    rows={5}
                                    value={form.bio}
                                    onChange={(e) => setForm({ ...form, bio: e.target.value })}
                                />
                            </label>

                            <div className="mentor-profile-actions">
                                <button type="button" className="mentor-profile-btn mentor-profile-btn--secondary">
                                    Share
                                </button>
                                <button type="submit" className="mentor-profile-btn mentor-profile-btn--primary" disabled={updateProfile.isPending}>
                                    {updateProfile.isPending ? 'Saving...' : 'Save profile'}
                                </button>
                            </div>
                        </form>
                    </div>

                    <div className="mentor-profile-card mentor-profile-panel">
                        <div className="mentor-profile-panel-header">
                            <h3>Contact</h3>
                            <span>Details</span>
                        </div>

                        <div className="mentor-profile-contact-list">
                            <div className="mentor-contact-item">
                                <span className="mentor-contact-label">Phone</span>
                                <strong>{profile?.phoneNumber || '+998 90 000 00 00'}</strong>
                            </div>
                            <div className="mentor-contact-item">
                                <span className="mentor-contact-label">Email</span>
                                <strong>{profile?.email || 'admin@crm.local'}</strong>
                            </div>
                            <div className="mentor-contact-item">
                                <span className="mentor-contact-label">Focus</span>
                                <strong>{form.specialization || 'Product design'}</strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MentorProfilePage;
