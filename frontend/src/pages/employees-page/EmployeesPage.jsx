import { PlaceholderPage } from '../../shared/ui';

function EmployeesPage() {
    return (
        <PlaceholderPage
            title="Employees"
            description="Staff / mentor directory, separate from the students list."
            apiTodo="GET /api/employees"
        />
    );
}

export default EmployeesPage;
