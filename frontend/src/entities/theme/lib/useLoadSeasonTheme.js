import { useEffect } from 'react';
import { getCurrentSeason } from '../../../shared/api/theme/getCurrentSeason';
import { useThemeStore } from '../model/themeStore';
import { SEASON, isValidSeason } from '../model/types';

// Local fallback if the backend is unreachable — keeps the theme from
// staying empty. month is 0-11 (Date#getMonth()).
const getSeasonByMonth = (month) => {
    if (month === 11 || month === 0 || month === 1) return SEASON.WINTER;
    if (month >= 2 && month <= 4) return SEASON.SPRING;
    if (month >= 5 && month <= 7) return SEASON.SUMMER;
    return SEASON.AUTUMN; // 8, 9, 10
};

// ?previewSeason=Winter — lets QA/designers force a theme without touching the backend.
const getPreviewSeasonFromUrl = () => {
    const params = new URLSearchParams(window.location.search);
    const preview = params.get('previewSeason');
    return isValidSeason(preview) ? preview : null;
};

export const useLoadSeasonTheme = () => {
    const setSeason = useThemeStore((state) => state.setSeason);
    const setLoading = useThemeStore((state) => state.setLoading);
    const setError = useThemeStore((state) => state.setError);

    useEffect(() => {
        let cancelled = false;

        const load = async () => {
            const previewSeason = getPreviewSeasonFromUrl();
            if (previewSeason) {
                setSeason(previewSeason, true);
                setLoading(false);
                return;
            }

            setLoading(true);
            try {
                const { season, isManualOverride } = await getCurrentSeason();
                if (cancelled) return;

                setSeason(
                    isValidSeason(season) ? season : getSeasonByMonth(new Date().getMonth()),
                    Boolean(isManualOverride)
                );
            } catch (err) {
                if (cancelled) return;

                // Backend is down/unreachable — fall back to a locally computed
                // season so the theme is never left empty.
                setSeason(getSeasonByMonth(new Date().getMonth()), false);
                setError(err);
            } finally {
                if (!cancelled) setLoading(false);
            }
        };

        load();

        return () => {
            cancelled = true;
        };
    }, [setSeason, setLoading, setError]);
};
