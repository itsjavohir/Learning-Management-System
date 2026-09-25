export const NAV_GROUPS = [
    {
        label: 'Main',
        items: [
            { to: '/', label: 'Dashboard', end: true, icon: 'dashboard' },
            {
                label: 'Students',
                icon: 'students',
                children: [
                    { to: '/users', label: 'All students' },
                    { to: '/students/graduates', label: 'Graduates' },
                    { to: '/students/enroll', label: 'Enroll' },
                    { to: '/students/left-courses', label: 'Left Courses' },
                ],
            },
            { to: '/rewards', label: 'Rewards', icon: 'rewards' },
            { to: '/groups', label: 'Groups', icon: 'groups' },
            { to: '/employees', label: 'Employees', icon: 'employees' },
            { to: '/timetable', label: 'TimeTable', icon: 'timetable' },
            { to: '/courses', label: 'Courses', icon: 'courses' },
            { to: '/administration', label: 'Administration', icon: 'administration' },
            { to: '/branches', label: 'Branches', icon: 'branches' },
            { to: '/sms-mailings', label: 'SMS mailings', icon: 'sms' },
            { to: '/accounting', label: 'Accounting', icon: 'accounting' },
            { to: '/settings', label: 'Settings', icon: 'settings' },
        ],
    },
];
