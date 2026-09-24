import { PlaceholderPage } from '../../shared/ui';

function BranchesPage() {
    return (
        <PlaceholderPage
            title="Branches"
            description="Physical branches/campuses — feeds the branch selector on Dashboard."
            apiTodo="GET /api/branches, POST /api/branches"
        />
    );
}

export default BranchesPage;
