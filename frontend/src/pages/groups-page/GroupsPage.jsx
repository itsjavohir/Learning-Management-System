import { useState } from 'react';
import { useCourses } from '../../entities/course';
import { useMentors } from '../../entities/mentor';
import {
    useGroups,
    useCreateGroup,
    useUpdateGroup,
    useDeleteGroup,
} from '../../entities/group';
import '../users-page/UsersPage.css';
import './GroupsPage.css';

const EMPTY_FORM = {
    name: '',
    courseId: '',
    mentorId: '',
    startDate: '',
    maxStudents: '',
};

function toDateInputValue(value) {
    if (!value) return '';
    return String(value).slice(0, 10);
}

function GroupsPage() {
    const [form, setForm] = useState(EMPTY_FORM);
    const [editingId, setEditingId] = useState(null);

    const { data: groups = [], isLoading, isError, error } = useGroups();
    const { data: courses = [] } = useCourses();
    const { data: mentors = [] } = useMentors();

    const createGroup = useCreateGroup();
    const updateGroup = useUpdateGroup();
    const deleteGroup = useDeleteGroup();

    const isSaving = createGroup.isPending || updateGroup.isPending;
    const saveError = createGroup.error || updateGroup.error;

    const handleSubmit = (e) => {
        e.preventDefault();

        if (editingId) {
            updateGroup.mutate(
                {
                    id: editingId,
                    name: form.name,
                    startDate: form.startDate,
                    maxStudents: Number(form.maxStudents),
                },
                {
                    onSuccess: () => {
                        setForm(EMPTY_FORM);
                        setEditingId(null);
                    },
                },
            );
            return;
        }

        createGroup.mutate(
            {
                name: form.name,
                courseId: form.courseId,
                // TODO: backend CreateGroup looks up mentor by UserId, not Mentor.Id.
                // Sending mentor.id from GET /mentors until the backend is fixed.
                mentorId: form.mentorId,
                startDate: form.startDate,
                maxStudents: Number(form.maxStudents),
            },
            { onSuccess: () => setForm(EMPTY_FORM) },
        );
    };

    const handleEdit = (group) => {
        setEditingId(group.id);
        setForm({
            name: group.name ?? '',
            courseId: group.courseId ?? '',
            mentorId: group.mentorId ?? '',
            startDate: toDateInputValue(group.startDate),
            maxStudents: group.maxStudents ?? '',
        });
    };

    const handleDelete = (id) => {
        if (!confirm('Delete this group?')) return;
        deleteGroup.mutate(id, {
            onSuccess: () => {
                if (editingId === id) {
                    setForm(EMPTY_FORM);
                    setEditingId(null);
                }
            },
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
                <h1>Groups</h1>
                <span className="users-count">{groups.length} total</span>
            </div>

            <div className="create-card">
                <h3>{editingId ? 'Edit group' : 'Add new group'}</h3>
                <form onSubmit={handleSubmit} className="create-form create-form--groups">
                    {saveError && (
                        <div className="create-form-error">
                            {saveError?.response?.data?.message || 'Error saving group'}
                        </div>
                    )}

                    <input
                        placeholder="Name"
                        value={form.name}
                        onChange={(e) => setForm({ ...form, name: e.target.value })}
                    />
                    {!editingId && (
                        <>
                            <select
                                value={form.courseId}
                                onChange={(e) => setForm({ ...form, courseId: e.target.value })}
                            >
                                <option value="">Course</option>
                                {courses.map((course) => (
                                    <option key={course.id} value={course.id}>
                                        {course.name}
                                    </option>
                                ))}
                            </select>
                            <select
                                value={form.mentorId}
                                onChange={(e) => setForm({ ...form, mentorId: e.target.value })}
                            >
                                <option value="">Mentor</option>
                                {mentors.map((mentor) => (
                                    <option key={mentor.id} value={mentor.id}>
                                        {mentor.firstName} {mentor.lastName}
                                    </option>
                                ))}
                            </select>
                        </>
                    )}
                    <input
                        type="date"
                        value={form.startDate}
                        onChange={(e) => setForm({ ...form, startDate: e.target.value })}
                    />
                    <input
                        type="number"
                        min="1"
                        placeholder="Max students"
                        value={form.maxStudents}
                        onChange={(e) => setForm({ ...form, maxStudents: e.target.value })}
                    />
                    <button type="submit" disabled={isSaving}>
                        {isSaving ? 'Saving...' : editingId ? 'Save' : 'Create'}
                    </button>
                    {editingId && (
                        <button
                            type="button"
                            className="cancel-btn"
                            onClick={() => {
                                setEditingId(null);
                                setForm(EMPTY_FORM);
                            }}
                        >
                            Cancel
                        </button>
                    )}
                </form>
            </div>

            <div className="users-table-wrap">
                <table className="users-table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Course</th>
                            <th>Mentor</th>
                            <th>Start date</th>
                            <th>Max students</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* TODO: confirm Group response fields with backend (mentorName vs mentorFullName) */}
                        {groups.map((group) => (
                            <tr key={group.id}>
                                <td>{group.name}</td>
                                <td>{group.courseName || group.courseId}</td>
                                <td>{group.mentorName || group.mentorFullName || group.mentorId}</td>
                                <td>{toDateInputValue(group.startDate)}</td>
                                <td>{group.maxStudents}</td>
                                <td>
                                    <button type="button" className="edit-btn" onClick={() => handleEdit(group)}>
                                        Edit
                                    </button>
                                    <button
                                        type="button"
                                        className="delete-btn"
                                        onClick={() => handleDelete(group.id)}
                                        disabled={deleteGroup.isPending}
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default GroupsPage;
