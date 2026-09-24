import { Outlet } from 'react-router-dom';
import { Sidebar } from '../sidebar';
import { Topbar } from '../topbar';
import './AdminLayout.css';

function AdminLayout() {
    return (
        <div className="admin-layout">
            <Sidebar />
            <div className="admin-layout-main">
                <Topbar />
                <main className="admin-layout-content">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}

export default AdminLayout;
