import { useState } from 'react';
import { useDashboardSummary } from '../../entities/dashboard';
import { Button } from '../../shared/ui';
import './DashboardPage.css';

function GraduationCapIcon() {
    return (
        <svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M12 4 2 9l10 5 10-5-10-5Z" />
            <path d="M6 11.5V17c0 1.4 2.7 3 6 3s6-1.6 6-3v-5.5" />
        </svg>
    );
}

function MentorsIcon() {
    return (
        <svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="9" cy="8" r="3" />
            <circle cx="17" cy="9" r="2.4" />
            <path d="M3 20c0-3.3 2.7-6 6-6s6 2.7 6 6" />
            <path d="M15.5 14.4c2.3.4 4.3 2.3 4.3 5.6" />
        </svg>
    );
}

function UserIcon() {
    return (
        <svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" strokeWidth="1.8">
            <circle cx="12" cy="8" r="4" />
            <path d="M4 21c0-4.4 3.6-8 8-8s8 3.6 8 8" />
        </svg>
    );
}

function ExcelIcon() {
    return (
        <svg viewBox="0 0 24 24" width="15" height="15" fill="none" stroke="currentColor" strokeWidth="2">
            <rect x="3" y="4" width="18" height="16" rx="2" />
            <path d="m8 9 8 8M16 9l-8 8" />
        </svg>
    );
}

function WordIcon() {
    return (
        <svg viewBox="0 0 24 24" width="15" height="15" fill="none" stroke="currentColor" strokeWidth="2">
            <rect x="3" y="4" width="18" height="16" rx="2" />
            <path d="M7 9v6M12 9v6M17 9v6" />
        </svg>
    );
}

const STAT_CARDS = (summary) => [
    { label: 'Students', value: summary.studentsCount, icon: GraduationCapIcon, tone: 'accent' },
    { label: 'Mentors', value: summary.mentorsCount, icon: MentorsIcon, tone: 'accent' },
    { label: 'User', value: summary.usersCount, icon: UserIcon, tone: 'accent' },
];

function DashboardPage() {
    // TODO backend: feed these into useDashboardSummary(branchId, date) once
    // GET /api/branches exists — for now the selector is local-only.
    const [branchId, setBranchId] = useState('all');
    const [date, setDate] = useState(() => new Date().toISOString().slice(0, 10));

    const { data: summary } = useDashboardSummary(branchId, date);

    return (
        <div className="dashboard-page">
            <div className="dashboard-toolbar">
                <div>
                    <h1>Dashboard</h1>
                </div>

                <div className="dashboard-toolbar-controls">
                    <label className="dashboard-control">
                        <span>Select Branch</span>
                        {/* TODO backend: GET /api/branches */}
                        <select value={branchId} onChange={(e) => setBranchId(e.target.value)}>
                            <option value="all">All Branches</option>
                        </select>
                    </label>

                    <label className="dashboard-control">
                        <span>Date</span>
                        <input type="date" value={date} onChange={(e) => setDate(e.target.value)} />
                    </label>
                </div>
            </div>

            <div className="dashboard-stats">
                {STAT_CARDS(summary).map(({ label, value, icon: Icon }) => (
                    <div className="stat-card" key={label}>
                        <div className="stat-card-top">
                            <span className="stat-card-icon"><Icon /></span>
                        </div>
                        <span className="stat-card-value">{value.toLocaleString()}</span>
                        <span className="stat-card-label">{label}</span>
                    </div>
                ))}
            </div>

            <div className="dashboard-grid">
                <div className="dashboard-col-left">
                    <div className="attendance-summary">
                        <div className="attendance-pill attendance-pill--present">
                            <span>Present</span>
                            <strong>{summary.attendance.present}</strong>
                        </div>
                        <div className="attendance-pill attendance-pill--absent">
                            <span>Absent</span>
                            <strong>{summary.attendance.absent}</strong>
                        </div>
                        <div className="attendance-pill attendance-pill--late">
                            <span>Late</span>
                            <strong>{summary.attendance.late}</strong>
                        </div>

                        <div className="attendance-export">
                            {/* TODO backend: GET /api/dashboard/attendance/export?format=xlsx|docx */}
                            <Button size="sm" variant="secondary" disabled title="Coming soon">
                                <ExcelIcon /> Export Excel
                            </Button>
                            <Button size="sm" disabled title="Coming soon">
                                <WordIcon /> Export Word
                            </Button>
                        </div>
                    </div>

                    <div className="dashboard-card dashboard-log-card">
                        <table className="dashboard-log-table">
                            <thead>
                            <tr>
                                <th>Full name</th>
                                <th>Reason</th>
                                <th>Phone</th>
                            </tr>
                            </thead>
                            <tbody>
                            {summary.attendanceLog.map((row) => (
                                <tr key={row.id}>
                                    <td>
                                        <div className="log-name">{row.fullName}</div>
                                        <div className="log-meta">
                                            <span className="log-role">{row.roleName}</span>
                                            <span className="log-group">{row.groupName}</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div className="log-reason">{row.reason}</div>
                                        {row.commentTime && (
                                            <div className="log-comment-time">Comment time: {row.commentTime}</div>
                                        )}
                                    </td>
                                    <td className="log-phone">{row.phone}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                </div>

                <div className="dashboard-card dashboard-groups-card">
                    <div className="dashboard-card-header">
                        <h3>Groups</h3>
                        <span className="dashboard-card-count">{summary.groups.length}</span>
                    </div>

                    <div className="groups-list">
                        {summary.groups.map((group) => (
                            <div className="group-row" key={group.id}>
                                <span className="group-icon">
                                    <svg viewBox="0 0 24 24" width="15" height="15" fill="none" stroke="currentColor" strokeWidth="2">
                                        <rect x="4" y="4" width="7" height="7" rx="1.5" />
                                        <rect x="13" y="4" width="7" height="7" rx="1.5" />
                                        <rect x="4" y="13" width="7" height="7" rx="1.5" />
                                        <rect x="13" y="13" width="7" height="7" rx="1.5" />
                                    </svg>
                                </span>
                                <div className="group-info">
                                    <span className="group-name">{group.name}</span>
                                    <span className="group-sub">Absent: {group.absentCount} | Late: {group.lateCount}</span>
                                </div>
                                <span className={`group-count ${group.studentsCount < group.capacity ? 'group-count--warn' : 'group-count--full'}`}>
                                    {group.studentsCount}/{group.capacity}
                                </span>
                            </div>
                        ))}
                    </div>
                </div>
            </div>

            <div className="dashboard-card attendance-strip-card">
                <div className="dashboard-card-header">
                    <h3>Students Attendance</h3>
                </div>

                <div className="attendance-strip">
                    {summary.dailyAttendance.map((d) => (
                        <div className="attendance-day" key={d.day} title={`Present: ${d.present} · Absent: ${d.absent}`}>
                            <div className="attendance-bar">
                                <div
                                    className="attendance-bar-fill"
                                    style={{ height: `${Math.min(100, (d.present / 36) * 100)}%` }}
                                />
                            </div>
                            <span className="attendance-day-label">{d.day}</span>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default DashboardPage;
