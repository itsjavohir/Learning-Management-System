import { useMemo } from 'react';

/*
 * TODO backend: GET /api/dashboard/summary?branchId={branchId}&date={date}
 *
 * Expected response shape:
 * {
 *   studentsCount: number,
 *   mentorsCount: number,
 *   usersCount: number,
 *   attendance: { present: number, absent: number, late: number },
 *   groups: [
 *     { id, name, studentsCount, capacity, absentCount, lateCount }
 *   ],
 *   attendanceLog: [
 *     { id, fullName, reason, roleName, phone, groupName, commentTime }
 *   ],
 *   dailyAttendance: [
 *     { day: number, present: number, absent: number }
 *   ]
 * }
 *
 * Once the endpoint exists, replace the body of useDashboardSummary with:
 *   return useQuery({
 *     queryKey: ['dashboard-summary', branchId, date],
 *     queryFn: () => dashboardApi.getSummary({ branchId, date }),
 *   });
 * — the component using this hook does not need to change.
 */

const MOCK_SUMMARY = {
    studentsCount: 299,
    mentorsCount: 31,
    usersCount: 1158,
    attendance: { present: 282, absent: 19, late: 13 },
    groups: [
        { id: 1, name: '.Net February 2026', studentsCount: 10, capacity: 10, absentCount: 0, lateCount: 0 },
        { id: 2, name: '.Net March 2026', studentsCount: 5, capacity: 5, absentCount: 0, lateCount: 0 },
        { id: 3, name: '.NET April 2026', studentsCount: 8, capacity: 10, absentCount: 0, lateCount: 0 },
        { id: 4, name: 'Figma April 2026', studentsCount: 9, capacity: 12, absentCount: 1, lateCount: 2 },
    ],
    attendanceLog: [
        { id: 1, fullName: 'Qurbonzoda Afrosiyob', reason: 'Бо хамрохии падараш дар паспортный стол падараш ба ман зангзад', roleName: 'Mentor', groupName: 'Figma April 2026', phone: '177602211, 905500501', commentTime: '14/05/2026, 16:08' },
        { id: 2, fullName: 'Suhrob Usmonalizoda', reason: 'without reason', roleName: 'Admin', groupName: 'Golang April 2026 (18:00)', phone: '071055225, 904250306', commentTime: '14/05/2026, 18:13' },
        { id: 3, fullName: 'Adiba Ziyoyeva', reason: 'without reason', roleName: 'Admin', groupName: 'Golang April 2026 (18:00)', phone: '888297878', commentTime: null },
        { id: 4, fullName: 'Tozajonzoda Muhammad', reason: 'Харду ракамаш ҷавоб надод', roleName: 'Admin', groupName: 'Django MVT May 14:00', phone: '552552270, 987552270', commentTime: null },
    ],
    dailyAttendance: Array.from({ length: 30 }, (_, i) => ({
        day: i + 1,
        present: 24 + Math.round(Math.sin(i / 2) * 6),
        absent: Math.max(0, Math.round(3 + Math.cos(i / 3) * 2)),
    })),
};

export function useDashboardSummary(branchId, date) {
    // TODO backend: pass branchId/date into the real query once the endpoint exists
    void branchId;
    void date;

    return useMemo(
        () => ({ data: MOCK_SUMMARY, isLoading: false, isError: false }),
        []
    );
}
