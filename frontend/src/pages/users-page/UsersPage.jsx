import { useState } from 'react';
import { useUsers, useCreateUser, useUpdateUser, useDeleteUser } from '../../entities/user';
import { Modal, Button, ConfirmDialog } from '../../shared/ui';
import './UsersPage.css';

const EMPTY_FORM = {
  firstName: '',
  lastName: '',
  phoneNumber: '',
  email: '',
  roleId: '',
};

function PlusIcon() {
  return (
      <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2.4">
        <path d="M12 5v14M5 12h14" />
      </svg>
  );
}

function EditIcon() {
  return (
      <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M12 20h9" />
        <path d="M16.5 3.5a2.1 2.1 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5Z" />
      </svg>
  );
}

function TrashIcon() {
  return (
      <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M4 7h16" />
        <path d="M9 7V4h6v3" />
        <path d="M6 7l1 13h10l1-13" />
      </svg>
  );
}

function UsersPage() {
  const { data: users = [], isLoading, isError, error } = useUsers();
  const createUser = useCreateUser();
  const updateUser = useUpdateUser();
  const deleteUser = useDeleteUser();

  // Create/Edit modal state
  const [formState, setFormState] = useState(null); // null | { mode: 'create' | 'edit', id?, form }
  // Delete confirm state
  const [deleteTarget, setDeleteTarget] = useState(null); // null | user

  const openCreateModal = () => setFormState({ mode: 'create', form: EMPTY_FORM });

  const openEditModal = (user) => setFormState({
    mode: 'edit',
    id: user.id,
    form: {
      firstName: user.firstName ?? '',
      lastName: user.lastName ?? '',
      phoneNumber: user.phoneNumber ?? '',
      email: user.email ?? '',
      roleId: user.roleId ?? '',
    },
  });

  const closeFormModal = () => setFormState(null);

  const isSaving = createUser.isPending || updateUser.isPending;
  const saveError = createUser.error || updateUser.error;

  const handleFormChange = (field, value) => {
    setFormState((prev) => ({ ...prev, form: { ...prev.form, [field]: value } }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!formState) return;

    if (formState.mode === 'edit') {
      updateUser.mutate(
          { id: formState.id, ...formState.form },
          { onSuccess: closeFormModal }
      );
      return;
    }

    createUser.mutate(formState.form, { onSuccess: closeFormModal });
  };

  const handleDeleteConfirm = () => {
    if (!deleteTarget) return;
    deleteUser.mutate(deleteTarget.id, {
      onSuccess: () => setDeleteTarget(null),
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
          <h1>Users</h1>
          <div className="users-header-actions">
            <span className="users-count">{users.length} total</span>
            <Button size="sm" onClick={openCreateModal}>
              <PlusIcon />
              Add user
            </Button>
          </div>
        </div>

        <div className="users-table-wrap">
          <table className="users-table">
            <thead>
            <tr>
              <th>Name</th>
              <th>Phone</th>
              <th>Email</th>
              <th>Role</th>
              <th>Status</th>
              <th></th>
            </tr>
            </thead>
            <tbody>
            {users.map((user) => (
                <tr key={user.id}>
                  <td>{user.firstName} {user.lastName}</td>
                  <td>{user.phoneNumber}</td>
                  <td>{user.email}</td>
                  <td><span className="role-badge">{user.roleName}</span></td>
                  <td>
                    <span className={`status-dot ${!user.isActive ? 'inactive' : ''}`} />
                    {user.isActive ? 'Active' : 'Inactive'}
                  </td>
                  <td>
                    <div className="row-actions">
                      <Button size="sm" variant="ghost" onClick={() => openEditModal(user)}>
                        <EditIcon />
                        Edit
                      </Button>
                      <Button size="sm" variant="ghost" className="row-actions-delete" onClick={() => setDeleteTarget(user)}>
                        <TrashIcon />
                        Delete
                      </Button>
                    </div>
                  </td>
                </tr>
            ))}
            {users.length === 0 && (
                <tr>
                  <td colSpan={6} className="users-empty">No users yet.</td>
                </tr>
            )}
            </tbody>
          </table>
        </div>

        {/* Create / Edit modal */}
        <Modal
            isOpen={!!formState}
            onClose={closeFormModal}
            title={formState?.mode === 'edit' ? 'Edit user' : 'Add new user'}
            footer={
              <>
                <Button variant="secondary" onClick={closeFormModal} disabled={isSaving}>
                  Cancel
                </Button>
                <Button type="submit" form="user-form" isLoading={isSaving}>
                  {formState?.mode === 'edit' ? 'Save changes' : 'Create user'}
                </Button>
              </>
            }
        >
          {formState && (
              <form id="user-form" className="user-form" onSubmit={handleSubmit}>
                {saveError && (
                    <div className="create-form-error">
                      {saveError?.response?.data?.message || 'Error saving user'}
                    </div>
                )}

                <div className="user-form-row">
                  <div className="user-form-field">
                    <label>First name</label>
                    <input
                        value={formState.form.firstName}
                        onChange={(e) => handleFormChange('firstName', e.target.value)}
                        autoFocus
                    />
                  </div>
                  <div className="user-form-field">
                    <label>Last name</label>
                    <input
                        value={formState.form.lastName}
                        onChange={(e) => handleFormChange('lastName', e.target.value)}
                    />
                  </div>
                </div>

                <div className="user-form-field">
                  <label>Phone number</label>
                  <input
                      value={formState.form.phoneNumber}
                      onChange={(e) => handleFormChange('phoneNumber', e.target.value)}
                  />
                </div>

                <div className="user-form-field">
                  <label>Email</label>
                  <input
                      type="email"
                      value={formState.form.email}
                      onChange={(e) => handleFormChange('email', e.target.value)}
                  />
                </div>

                <div className="user-form-field">
                  <label>Role ID</label>
                  <input
                      value={formState.form.roleId}
                      onChange={(e) => handleFormChange('roleId', e.target.value)}
                  />
                </div>
              </form>
          )}
        </Modal>

        {/* Delete confirmation */}
        <ConfirmDialog
            isOpen={!!deleteTarget}
            onClose={() => setDeleteTarget(null)}
            onConfirm={handleDeleteConfirm}
            title="Delete user"
            description={
              deleteTarget
                  ? `This will permanently delete ${deleteTarget.firstName} ${deleteTarget.lastName}. This action cannot be undone.`
                  : ''
            }
            confirmLabel="Delete"
            isLoading={deleteUser.isPending}
            error={deleteUser.isError ? (deleteUser.error?.response?.data?.message || 'Error deleting user') : null}
        />
      </div>
  );
}

export default UsersPage;
