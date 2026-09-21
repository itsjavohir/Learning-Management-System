import { useState } from 'react';
import {
    useCourses,
    useCreateCourse,
    useUpdateCourse,
    useDeleteCourse,
} from '../../entities/course';
import '../users-page/UsersPage.css';
import './CoursesPage.css';

const EMPTY_FORM = {
    name: '',
    description: '',
    durationWeeks: '',
    isActive: true,
};

function CoursesPage() {
    const [form, setForm] = useState(EMPTY_FORM);
    const [editingId, setEditingId] = useState(null);

    const { data: courses = [], isLoading, isError, error } = useCourses();
    const createCourse = useCreateCourse();
    const updateCourse = useUpdateCourse();
    const deleteCourse = useDeleteCourse();

    const isSaving = createCourse.isPending || updateCourse.isPending;
    const saveError = createCourse.error || updateCourse.error;

    const handleSubmit = (e) => {
        e.preventDefault();

        const payload = {
            name: form.name,
            description: form.description || null,
            durationWeeks: Number(form.durationWeeks),
        };

        if (editingId) {
            updateCourse.mutate(
                { id: editingId, ...payload, isActive: form.isActive },
                { onSuccess: () => {
                    setForm(EMPTY_FORM);
                    setEditingId(null);
                } },
            );
            return;
        }

        createCourse.mutate(payload, {
            onSuccess: () => setForm(EMPTY_FORM),
        });
    };

    const handleEdit = (course) => {
        setEditingId(course.id);
        setForm({
            name: course.name ?? '',
            description: course.description ?? '',
            durationWeeks: course.durationWeeks ?? '',
            isActive: course.isActive ?? true,
        });
    };

    const handleDelete = (id) => {
        if (!confirm('Delete this course?')) return;
        deleteCourse.mutate(id, {
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
                <h1>Courses</h1>
                <span className="users-count">{courses.length} total</span>
            </div>

            <div className="create-card">
                <h3>{editingId ? 'Edit course' : 'Add new course'}</h3>
                <form onSubmit={handleSubmit} className="create-form create-form--courses">
                    {saveError && (
                        <div className="create-form-error">
                            {saveError?.response?.data?.message || 'Error saving course'}
                        </div>
                    )}

                    <input
                        placeholder="Name"
                        value={form.name}
                        onChange={(e) => setForm({ ...form, name: e.target.value })}
                    />
                    <input
                        placeholder="Description"
                        value={form.description}
                        onChange={(e) => setForm({ ...form, description: e.target.value })}
                    />
                    <input
                        type="number"
                        min="1"
                        placeholder="Duration (weeks)"
                        value={form.durationWeeks}
                        onChange={(e) => setForm({ ...form, durationWeeks: e.target.value })}
                    />
                    {editingId && (
                        <label className="active-toggle">
                            <input
                                type="checkbox"
                                checked={form.isActive}
                                onChange={(e) => setForm({ ...form, isActive: e.target.checked })}
                            />
                            Active
                        </label>
                    )}
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
                            <th>Description</th>
                            <th>Duration</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* TODO: confirm Course response fields with backend */}
                        {courses.map((course) => (
                            <tr key={course.id}>
                                <td>{course.name}</td>
                                <td>{course.description || '—'}</td>
                                <td>{course.durationWeeks} weeks</td>
                                <td>
                                    <span className={`status-dot ${!course.isActive ? 'inactive' : ''}`} />
                                    {course.isActive ? 'Active' : 'Inactive'}
                                </td>
                                <td>
                                    <button type="button" className="edit-btn" onClick={() => handleEdit(course)}>
                                        Edit
                                    </button>
                                    <button
                                        type="button"
                                        className="delete-btn"
                                        onClick={() => handleDelete(course.id)}
                                        disabled={deleteCourse.isPending}
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

export default CoursesPage;
