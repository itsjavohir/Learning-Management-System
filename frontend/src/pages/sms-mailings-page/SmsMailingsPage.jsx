import { PlaceholderPage } from '../../shared/ui';

function SmsMailingsPage() {
    return (
        <PlaceholderPage
            title="SMS mailings"
            description="Bulk SMS campaigns to students/parents."
            apiTodo="GET /api/sms-mailings, POST /api/sms-mailings"
        />
    );
}

export default SmsMailingsPage;
