import { create } from 'zustand';
import { getSeasonState, SEASONS } from './seasonalCalendar';
import { getThemeConfig } from './seasonThemeMap';

const STORAGE_KEY = 'crm-seasonal-theme-enabled';

const isEnabled = () => typeof window === 'undefined'
    || window.localStorage.getItem(STORAGE_KEY) !== 'false';

export const useThemeStore = create((set) => ({
    season: null,
    event: null,
    transition: 0,
    effects: [],
    uiAccents: [],
    mode: 'auto',
    enabled: isEnabled(),
    isLoading: true,
    error: null,

    applyTheme: (season, event = null, transition = 0, mode = 'auto') => {
        const safeSeason = Object.values(SEASONS).includes(season) ? season : SEASONS.AUTUMN;
        const config = getThemeConfig(safeSeason, event);
        set({ season: safeSeason, event, transition, ...config, mode, error: null });
    },

    syncWithDate: () => {
        const state = getSeasonState();
        set({ ...state, ...getThemeConfig(state.season, state.event), mode: 'auto', isLoading: false, error: null });
    },

    setManualTheme: (season, event = null) => {
        const safeSeason = Object.values(SEASONS).includes(season) ? season : SEASONS.AUTUMN;
        set({ season: safeSeason, event, transition: 0, ...getThemeConfig(safeSeason, event), mode: 'manual', isLoading: false });
    },

    setEnabled: (enabled) => {
        if (typeof window !== 'undefined') window.localStorage.setItem(STORAGE_KEY, String(enabled));
        const { season, event } = useThemeStore.getState();
        set({ enabled, effects: enabled ? getThemeConfig(season, event).effects : [] });
    },

    setLoading: (isLoading) => set({ isLoading }),

    setError: (error) => set({ error }),
}));
