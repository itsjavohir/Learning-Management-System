import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import DashboardPage from '../pages/dashboard-page/DashboardPage';
import UsersPage from '../pages/users-page/UsersPage';
import LoginPage from '../pages/login-page/LoginPage';
import ForgotPasswordPage from '../pages/forgot-password-page/ForgotPasswordPage';
import ChangePasswordPage from '../pages/change-password-page/ChangePasswordPage';
import CoursesPage from '../pages/courses-page/CoursesPage';
import GroupsPage from '../pages/groups-page/GroupsPage';
import MentorProfilePage from '../pages/mentor-profile-page/MentorProfilePage';
import GraduatesPage from '../pages/graduates-page/GraduatesPage';
import EnrollPage from '../pages/enroll-page/EnrollPage';
import LeftCoursesPage from '../pages/left-courses-page/LeftCoursesPage';
import RewardsPage from '../pages/rewards-page/RewardsPage';
import EmployeesPage from '../pages/employees-page/EmployeesPage';
import TimeTablePage from '../pages/timetable-page/TimeTablePage';
import AdministrationPage from '../pages/administration-page/AdministrationPage';
import BranchesPage from '../pages/branches-page/BranchesPage';
import SmsMailingsPage from '../pages/sms-mailings-page/SmsMailingsPage';
import AccountingPage from '../pages/accounting-page/AccountingPage';
import { ProtectedRoute } from '../features/auth';
import { AdminLayout } from '../widgets/admin-layout';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                <Route
                    path="/change-password"
                    element={
                        <ProtectedRoute>
                            <ChangePasswordPage />
                        </ProtectedRoute>
                    }
                />
                <Route
                    element={
                        <ProtectedRoute>
                            <AdminLayout />
                        </ProtectedRoute>
                    }
                >
                    <Route path="/" element={<DashboardPage />} />
                    <Route path="/users" element={<UsersPage />} />
                    <Route path="/students/graduates" element={<GraduatesPage />} />
                    <Route path="/students/enroll" element={<EnrollPage />} />
                    <Route path="/students/left-courses" element={<LeftCoursesPage />} />
                    <Route path="/rewards" element={<RewardsPage />} />
                    <Route path="/groups" element={<GroupsPage />} />
                    <Route path="/employees" element={<EmployeesPage />} />
                    <Route path="/timetable" element={<TimeTablePage />} />
                    <Route path="/courses" element={<CoursesPage />} />
                    <Route path="/administration" element={<AdministrationPage />} />
                    <Route path="/branches" element={<BranchesPage />} />
                    <Route path="/sms-mailings" element={<SmsMailingsPage />} />
                    <Route path="/accounting" element={<AccountingPage />} />
                    <Route path="/mentor-profile" element={<MentorProfilePage />} />
                </Route>
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
