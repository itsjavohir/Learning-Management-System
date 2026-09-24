import { PlaceholderPage } from '../../shared/ui';

function AccountingPage() {
    return (
        <PlaceholderPage
            title="Accounting"
            description="Payments, invoices and balances per student."
            apiTodo="GET /api/payments, GET /api/invoices"
        />
    );
}

export default AccountingPage;
