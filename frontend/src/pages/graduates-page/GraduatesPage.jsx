import { PlaceholderPage } from '../../shared/ui';

function GraduatesPage() {
    return (
        <PlaceholderPage
            title="Graduates"
            description="List of students who finished all their courses."
            apiTodo="GET /api/students?status=graduated"
        />
    );
}

export default GraduatesPage;
