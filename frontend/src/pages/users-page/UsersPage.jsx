import { useEffect, useState } from 'react';
import { userApi } from '../../entities/user/api/userApi';
import './UsersPage.css';

function UsersPage() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [form, setForm] = useState({
    firstName: '', lastName: '', phoneNumber: '', email: '', roleId: '',
  });

  const loadUsers = () => {
    setLoading(true);
    userApi.getAll().then(setUsers).catch((err) => setError(err.message)).finally(() => setLoading(false));
  };

  useEffect(() => { loadUsers(); }, []);

  const handleCreate = async (e) => {
    e.preventDefault();
    try {
      await userApi.create(form);
      setForm({ firstName: '', lastName: '', phoneNumber: '', email: '', roleId: '' });
      loadUsers();
    } catch (err) {
      alert(err.response?.data?.message || 'Error creating user');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Delete this user?')) return;
    await userApi.delete(id);
    loadUsers();
  };

  if (loading) return <p style={{ padding: 40 }}>Loading...</p>;
  if (error) return <p style={{ padding: 40, color: 'var(--color-danger)' }}>Error: {error}</p>;

  return (
      <div className="users-page">
        <div className="users-header">
          <h1>Users</h1>
          <span className="users-count">{users.length} total</span>
        </div>

        <div className="create-card">
          <h3>Add new user</h3>
          <form onSubmit={handleCreate} className="create-form">
            <input placeholder="First name" value={form.firstName}
                   onChange={(e) => setForm({ ...form, firstName: e.target.value })} />
            <input placeholder="Last name" value={form.lastName}
                   onChange={(e) => setForm({ ...form, lastName: e.target.value })} />
            <input placeholder="Phone" value={form.phoneNumber}
                   onChange={(e) => setForm({ ...form, phoneNumber: e.target.value })} />
            <input placeholder="Email" value={form.email}
                   onChange={(e) => setForm({ ...form, email: e.target.value })} />
            <input placeholder="Role ID" value={form.roleId}
                   onChange={(e) => setForm({ ...form, roleId: e.target.value })} />
            <button type="submit">Create</button>
          </form>
        </div>

        <div className="users-table-wrap">
          <table className="users-table">
            <thead>
            <tr>
              <th>Name</th><th>Phone</th><th>Email</th><th>Role</th><th>Status</th><th></th>
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
                  <td><button className="delete-btn" onClick={() => handleDelete(user.id)}>Delete</button></td>
                </tr>
            ))}
            </tbody>
          </table>
        </div>
      </div>
  );
}

export default UsersPage;