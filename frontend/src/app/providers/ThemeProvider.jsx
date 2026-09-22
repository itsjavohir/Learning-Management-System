import { useEffect } from 'react';
import { useLoadSeasonTheme } from '../../entities/theme/lib/useLoadSeasonTheme';
import { useThemeStore } from '../../entities/theme/model/themeStore';

// Loads the current season (backend, or a local fallback) and reflects it
// on <html data-theme="..."> so plain CSS can pick it up everywhere via
// [data-theme="..."] selectors — no per-page JS needed.
function ThemeProvider({ children }) {
    useLoadSeasonTheme();

    const season = useThemeStore((state) => state.season);

    useEffect(() => {
        if (!season) return;
        document.documentElement.setAttribute('data-theme', season.toLowerCase());
    }, [season]);

    // Intentionally no loading gate here: the default (:root) CSS values
    // already look correct, so we never block the form behind a spinner —
    // the theme just switches in-place once the season is known.
    return children;
}

export default ThemeProvider;
