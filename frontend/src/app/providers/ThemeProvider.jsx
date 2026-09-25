import { useEffect } from 'react';
import { useLoadSeasonTheme } from '../../entities/theme/lib/useLoadSeasonTheme';
import { getThemeConfig } from '../../entities/theme/model/seasonThemeMap';
import { useThemeStore } from '../../entities/theme/model/themeStore';

// Loads the current season (backend, or a local fallback) and reflects it
// on <html data-theme="..."> so plain CSS can pick it up everywhere via
// [data-theme="..."] selectors — no per-page JS needed.
function ThemeProvider({ children }) {
    useLoadSeasonTheme();

    const season = useThemeStore((state) => state.season);
    const event = useThemeStore((state) => state.event);
    const enabled = useThemeStore((state) => state.enabled);

    useEffect(() => {
        if (!season) return;
        const { palette } = getThemeConfig(season, event);
        document.documentElement.setAttribute('data-theme', season.toLowerCase());
        document.documentElement.dataset.seasonalEffects = enabled ? 'on' : 'off';
        document.documentElement.style.setProperty('--season-bg', palette.bg);
        document.documentElement.style.setProperty('--season-accent', palette.accent);
        document.documentElement.style.setProperty('--season-form-bg', palette.formBg);
        document.documentElement.style.setProperty('--season-text', palette.text);
    }, [season, event, enabled]);

    // Intentionally no loading gate here: the default (:root) CSS values
    // already look correct, so we never block the form behind a spinner —
    // the theme just switches in-place once the season is known.
    return children;
}

export default ThemeProvider;
