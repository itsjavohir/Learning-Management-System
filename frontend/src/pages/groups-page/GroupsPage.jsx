import { useState } from 'react';
import { useCourses } from '../../entities/course';
import { useMentors } from '../../entities/mentor';
import {
    useGroups,
    useCreateGroup,
    useUpdateGroup,
    useDeleteGroup,
} from '../../entities/group';
import { Modal, Button, ConfirmDialog } from '../../shared/ui';
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
    const [modalState, setModalState] = useState(null);
    const [deleteTarget, setDeleteTarget] = useState(null);

    const { data: groups = [], isLoading, isError, error } = useGroups();
    const { data: courses = [] } = useCourses();
    const { data: mentors = [] } = useMentors();

    const createGroup = useCreateGroup();
    const updateGroup = useUpdateGroup();
    const deleteGroup = useDeleteGroup();

    const isSaving = createGroup.isPending || updateGroup.isPending;
    const saveError = createGroup.error || updateGroup.error;

    const openCreateModal = () => {
        setEditingId(null);
        setForm(EMPTY_FORM);
        setModalState({ mode: 'create' });
    };

    const openEditModal = (group) => {
        setEditingId(group.id);
        setForm({
            name: group.name ?? '',
            courseId: group.courseId ?? '',
            mentorId: group.mentorId ?? '',
            startDate: toDateInputValue(group.startDate),
            maxStudents: group.maxStudents ?? '',
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

        if (editingId) {
            updateGroup.mutate(
                {
                    id: editingId,
                    name: form.name,
                    startDate: form.startDate,
                    maxStudents: Number(form.maxStudents),
                },
                { onSuccess: closeModal },
            );
            return;
        }

        createGroup.mutate(
            {
                name: form.name,
                courseId: form.courseId,
                mentorId: form.mentorId,
                startDate: form.startDate,
                maxStudents: Number(form.maxStudents),
            },
            { onSuccess: closeModal },
        );
    };

    const handleDeleteConfirm = () => {
        if (!deleteTarget) return;
        deleteGroup.mutate(deleteTarget.id, {
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
                <h1>Groups</h1>
                <div className="users-header-actions">
                    <span className="users-count">{groups.length} total</span>
                    <Button size="sm" onClick={openCreateModal}>Add group</Button>
                </div>
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
                        {groups.map((group) => (
                            <tr key={group.id}>
                                <td>{group.name}</td>
                                <td>{group.courseName || group.courseId}</td>
                                <td>{group.mentorName || group.mentorFullName || group.mentorId}</td>
                                <td>{toDateInputValue(group.startDate)}</td>
                                <td>{group.maxStudents}</td>
                                <td>
                                    <div className="row-actions">
                                        <Button size="sm" variant="ghost" onClick={() => openEditModal(group)}>
                                            Edit
                                        </Button>
                                        <Button size="sm" variant="ghost" className="row-actions-delete" onClick={() => setDeleteTarget(group)}>
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
                title={editingId ? 'Edit group' : 'Add new group'}
                footer={
                    <>
                        <Button variant="secondary" onClick={closeModal} disabled={isSaving}>Cancel</Button>
                        <Button type="submit" form="group-form" isLoading={isSaving}>
                            {editingId ? 'Save changes' : 'Create group'}
                        </Button>
                    </>
                }
            >
                <form id="group-form" className="user-form" onSubmit={handleSubmit}>
                    {saveError && (
                        <div className="create-form-error">
                            {saveError?.response?.data?.message || 'Error saving group'}
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

                    <div className="user-form-row">
                        <div className="user-form-field">
                            <label>Course</label>
                            <select
                                value={form.courseId}
                                onChange={(e) => setForm({ ...form, courseId: e.target.value })}
                            >
                                <option value="">Select course</option>
                                {courses.map((course) => (
                                    <option key={course.id} value={course.id}>{course.name}</option>
                                ))}
                            </select>
                        </div>
                        <div className="user-form-field">
                            <label>Mentor</label>
                            <select
                                value={form.mentorId}
                                onChange={(e) => setForm({ ...form, mentorId: e.target.value })}
                            >
                                <option value="">Select mentor</option>
                                {mentors.map((mentor) => (
                                    <option key={mentor.id} value={mentor.id}>{mentor.firstName} {mentor.lastName}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div className="user-form-row">
                        <div className="user-form-field">
                            <label>Start date</label>
                            <input
                                type="date"
                                value={form.startDate}
                                onChange={(e) => setForm({ ...form, startDate: e.target.value })}
                            />
                        </div>
                        <div className="user-form-field">
                            <label>Max students</label>
                            <input
                                type="number"
                                min="1"
                                value={form.maxStudents}
                                onChange={(e) => setForm({ ...form, maxStudents: e.target.value })}
                            />
                        </div>
                    </div>
                </form>
            </Modal>

            <ConfirmDialog
                isOpen={!!deleteTarget}
                onClose={() => setDeleteTarget(null)}
                onConfirm={handleDeleteConfirm}
                title="Delete group"
                description={
                    deleteTarget
                        ? `This will permanently delete the group ${deleteTarget.name}. This action cannot be undone.`
                        : ''
                }
                confirmLabel="Delete"
                isLoading={deleteGroup.isPending}
                error={deleteGroup.isError ? (deleteGroup.error?.response?.data?.message || 'Error deleting group') : null}
            />
        </div>
    );
}

export default GroupsPage;
