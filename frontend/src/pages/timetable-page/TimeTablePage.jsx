import { PlaceholderPage } from '../../shared/ui';

function TimeTablePage() {
    return (
        <PlaceholderPage
            title="TimeTable"
            description="Weekly schedule of group lessons per branch/room."
            apiTodo="GET /api/timetable"
        />
    );
}

export default TimeTablePage;
