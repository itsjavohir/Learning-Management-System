import { useState } from 'react';
import {
    useCourses,
    useCreateCourse,
    useUpdateCourse,
    useDeleteCourse,
} from '../../entities/course';
import { Modal, Button, ConfirmDialog } from '../../shared/ui';
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
    const [modalState, setModalState] = useState(null);
    const [deleteTarget, setDeleteTarget] = useState(null);

    const { data: courses = [], isLoading, isError, error } = useCourses();
    const createCourse = useCreateCourse();
    const updateCourse = useUpdateCourse();
    const deleteCourse = useDeleteCourse();

    const isSaving = createCourse.isPending || updateCourse.isPending;
    const saveError = createCourse.error || updateCourse.error;

    const openCreateModal = () => {
        setEditingId(null);
        setForm(EMPTY_FORM);
        setModalState({ mode: 'create' });
    };

    const openEditModal = (course) => {
        setEditingId(course.id);
        setForm({
            name: course.name ?? '',
            description: course.description ?? '',
            durationWeeks: course.durationWeeks ?? '',
            isActive: course.isActive ?? true,
        });
        setModalState({ mode: 'edit' });
    };

    const closeModal = () => {
        setModalState(null);
        setEditingId(null);
        setForm(EMPTY_FORM);
    };

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
                { onSuccess: closeModal },
            );
            return;
        }

        createCourse.mutate(payload, { onSuccess: closeModal });
    };

    const handleDeleteConfirm = () => {
        if (!deleteTarget) return;
        deleteCourse.mutate(deleteTarget.id, {
            onSuccess: () => {
                setDeleteTarget(null);
                if (editingId === deleteTarget.id) {
                    closeModal();
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
                <div className="users-header-actions">
                    <span className="users-count">{courses.length} total</span>
                    <Button size="sm" onClick={openCreateModal}>Add course</Button>
                </div>
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
                                    <div className="row-actions">
                                        <Button size="sm" variant="ghost" onClick={() => openEditModal(course)}>
                                            Edit
                                        </Button>
                                        <Button size="sm" variant="ghost" className="row-actions-delete" onClick={() => setDeleteTarget(course)}>
                                            Delete
                                        </Button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            <Modal
                isOpen={!!modalState}
                onClose={closeModal}
                title={editingId ? 'Edit course' : 'Add new course'}
                footer={
                    <>
                        <Button variant="secondary" onClick={closeModal} disabled={isSaving}>Cancel</Button>
                        <Button type="submit" form="course-form" isLoading={isSaving}>
                            {editingId ? 'Save changes' : 'Create course'}
                        </Button>
                    </>
                }
            >
                <form id="course-form" className="user-form" onSubmit={handleSubmit}>
                    {saveError && (
                        <div className="create-form-error">
                            {saveError?.response?.data?.message || 'Error saving course'}
                        </div>
                    )}

                    <div className="user-form-field">
                        <label>Name</label>
                        <input
                            value={form.name}
                            onChange={(e) => setForm({ ...form, name: e.target.value })}
                            autoFocus
                        />
                    </div>

                    <div className="user-form-field">
                        <label>Description</label>
                        <input
                            value={form.description}
                            onChange={(e) => setForm({ ...form, description: e.target.value })}
                        />
                    </div>

                    <div className="user-form-field">
                        <label>Duration (weeks)</label>
                        <input
                            type="number"
                            min="1"
                            value={form.durationWeeks}
                            onChange={(e) => setForm({ ...form, durationWeeks: e.target.value })}
                        />
                    </div>

                    {editingId && (
                        <label className="settings-option">
                            <span>Active</span>
                            <input
                                type="checkbox"
                                checked={form.isActive}
                                onChange={(e) => setForm({ ...form, isActive: e.target.checked })}
                            />
                        </label>
                    )}
                </form>
            </Modal>

            <ConfirmDialog
                isOpen={!!deleteTarget}
                onClose={() => setDeleteTarget(null)}
                onConfirm={handleDeleteConfirm}
                title="Delete course"
                description={
                    deleteTarget
                        ? `This will permanently delete ${deleteTarget.name}. This action cannot be undone.`
                        : ''
                }
                confirmLabel="Delete"
                isLoading={deleteCourse.isPending}
                error={deleteCourse.isError ? (deleteCourse.error?.response?.data?.message || 'Error deleting course') : null}
            />
        </div>
    );
}

export default CoursesPage;
