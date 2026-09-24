import './PlaceholderPage.css';

function BuildIcon() {
    return (
        <svg viewBox="0 0 24 24" width="30" height="30" fill="none" stroke="currentColor" strokeWidth="1.6">
            <path d="M14.5 3.5 3.5 14.5l3 3L17.5 6.5l-3-3Z" />
            <path d="M11 7l6 6" />
            <path d="M4 20l2.2-.6L5 17.2 4 20Z" />
        </svg>
    );
}

// Real, empty-but-routable pages for sections the backend doesn't
// have yet. Keeps every sidebar link alive (no dead <a href="#">)
// while the API contract for the section is still TODO.
function PlaceholderPage({ title, description, apiTodo }) {
    return (
        <div className="placeholder-page">
            <div className="placeholder-card">
                <div className="placeholder-icon">
                    <BuildIcon />
                </div>
                <h1>{title}</h1>
                <p>{description || 'This section is wired up and routable — content will land here once the backend endpoint is ready.'}</p>
                {apiTodo && <code className="placeholder-todo">TODO: {apiTodo}</code>}
            </div>
        </div>
    );
}

export default PlaceholderPage;
