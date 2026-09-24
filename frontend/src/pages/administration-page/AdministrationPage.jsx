import { PlaceholderPage } from '../../shared/ui';

function AdministrationPage() {
    return (
        <PlaceholderPage
            title="Administration"
            description="Roles, permissions and admin-level account management."
            apiTodo="GET /api/roles, GET /api/permissions"
        />
    );
}

export default AdministrationPage;
