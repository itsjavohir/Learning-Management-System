import { useCallback, useEffect, useState } from 'react';

const STORAGE_KEY = 'crm-color-mode';

const getInitialMode = () => {
    if (typeof window === 'undefined') return 'light';

    const stored = localStorage.getItem(STORAGE_KEY);
    if (stored === 'light' || stored === 'dark') return stored;

    return window.matchMedia?.('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
};

// Real, fully client-side dark/light toggle for the app shell
// (dashboard, users, courses, etc). Persists to localStorage and
// reflects on <html data-color-mode="..."> — see index.css for the
// dark palette. Independent from the seasonal auth-page theme.
export function useColorMode() {
    const [mode, setMode] = useState(getInitialMode);

    useEffect(() => {
        document.documentElement.setAttribute('data-color-mode', mode);
        localStorage.setItem(STORAGE_KEY, mode);
    }, [mode]);

    const toggle = useCallback(() => {
        setMode((prev) => (prev === 'dark' ? 'light' : 'dark'));
    }, []);

    return { mode, toggle };
}
