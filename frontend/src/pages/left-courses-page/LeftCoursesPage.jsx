import { PlaceholderPage } from '../../shared/ui';

function LeftCoursesPage() {
    return (
        <PlaceholderPage
            title="Left Courses"
            description="Students who dropped out of a course before finishing."
            apiTodo="GET /api/students?status=left"
        />
    );
}

export default LeftCoursesPage;
