import { useEffect } from 'react';
import { getCurrentSeason } from '../../../shared/api/theme/getCurrentSeason';
import { useThemeStore } from '../model/themeStore';

export const useLoadSeasonTheme = () => {
    const applyTheme = useThemeStore((state) => state.applyTheme);
    const syncWithDate = useThemeStore((state) => state.syncWithDate);
    const setLoading = useThemeStore((state) => state.setLoading);
    const setError = useThemeStore((state) => state.setError);

    useEffect(() => {
        let cancelled = false;

        const load = async () => {
            setLoading(true);
            try {
                const { season, event = null, transition = 0, isManualOverride } = await getCurrentSeason();
                if (cancelled) return;
                applyTheme(season?.toLowerCase(), event, transition, isManualOverride ? 'manual' : 'auto');
            } catch (err) {
                if (cancelled) return;
                syncWithDate();
                setError(err);
            } finally {
                if (!cancelled) setLoading(false);
            }
        };

        load();

        return () => {
            cancelled = true;
        };
    }, [applyTheme, syncWithDate, setLoading, setError]);
};
