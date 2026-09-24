import { PlaceholderPage } from '../../shared/ui';

function EnrollPage() {
    return (
        <PlaceholderPage
            title="Enroll"
            description="Enroll a new or existing student into a course/group."
            apiTodo="POST /api/enrollments"
        />
    );
}

export default EnrollPage;
