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

    return (
        <div className="users-page">
            <div className="users-header">
                <h1>Mentor Profile</h1>
            </div>

            <div className="create-card mentor-profile-card">
                <h3>
                    {profile?.firstName} {profile?.lastName}
                </h3>
                <p className="mentor-profile-meta">
                    {profile?.phoneNumber}
                    {profile?.email ? ` · ${profile.email}` : ''}
                </p>

                <form onSubmit={handleSubmit} className="create-form create-form--profile">
                    {updateProfile.isError && (
                        <div className="create-form-error">
                            {updateProfile.error?.response?.data?.message || 'Error updating profile'}
                        </div>
                    )}
                    {updateProfile.isSuccess && (
                        <div className="create-form-success">Profile saved</div>
                    )}

                    <input
                        placeholder="Specialization"
                        value={form.specialization}
                        onChange={(e) => setForm({ ...form, specialization: e.target.value })}
                    />
                    <input
                        type="number"
                        min="0"
                        placeholder="Experience (years)"
                        value={form.experienceYears}
                        onChange={(e) => setForm({ ...form, experienceYears: e.target.value })}
                    />
                    <textarea
                        placeholder="Bio"
                        rows={4}
                        value={form.bio}
                        onChange={(e) => setForm({ ...form, bio: e.target.value })}
                    />
                    <button type="submit" disabled={updateProfile.isPending}>
                        {updateProfile.isPending ? 'Saving...' : 'Save profile'}
                    </button>
                </form>
            </div>
        </div>
    );
}

export default MentorProfilePage;
