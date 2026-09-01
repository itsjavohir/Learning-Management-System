import { useState } from 'react';
import { useUsers, useCreateUser, useDeleteUser } from '../../entities/user';
import './UsersPage.css';

const EMPTY_FORM = {
  firstName: '',
  lastName: '',
  phoneNumber: '',
  email: '',
  roleId: '',
};

function UsersPage() {
  const [form, setForm] = useState(EMPTY_FORM);

  const { data: users = [], isLoading, isError, error } = useUsers();
  const createUser = useCreateUser();
  const deleteUser = useDeleteUser();

  const handleCreate = (e) => {
    e.preventDefault();
    createUser.mutate(form, {
      onSuccess: () => setForm(EMPTY_FORM),
    });
  };

  const handleDelete = (id) => {
    if (!confirm('Delete this user?')) return;
    deleteUser.mutate(id);
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
          <span className="users-count">{users.length} total</span>
        </div>

        <div className="create-card">
          <h3>Add new user</h3>
          <form onSubmit={handleCreate} className="create-form">
            {createUser.isError && (
                <div className="create-form-error">
                  {createUser.error?.response?.data?.message || 'Error creating user'}
                </div>
            )}

            <input
                placeholder="First name"
                value={form.firstName}
                onChange={(e) => setForm({ ...form, firstName: e.target.value })}
            />
            <input
                placeholder="Last name"
                value={form.lastName}
                onChange={(e) => setForm({ ...form, lastName: e.target.value })}
            />
            <input
                placeholder="Phone"
                value={form.phoneNumber}
                onChange={(e) => setForm({ ...form, phoneNumber: e.target.value })}
            />
            <input
                placeholder="Email"
                value={form.email}
                onChange={(e) => setForm({ ...form, email: e.target.value })}
            />
            <input
                placeholder="Role ID"
                value={form.roleId}
                onChange={(e) => setForm({ ...form, roleId: e.target.value })}
            />
            <button type="submit" disabled={createUser.isPending}>
              {createUser.isPending ? 'Adding...' : 'Create'}
            </button>
          </form>
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
                    <button
                        className="delete-btn"
                        onClick={() => handleDelete(user.id)}
                        disabled={deleteUser.isPending}
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

export default UsersPage;
