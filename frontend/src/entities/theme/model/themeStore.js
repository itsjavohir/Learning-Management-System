import { create } from 'zustand';

export const useThemeStore = create((set) => ({
    season: null,
    isLoading: true,
    isManualOverride: false,
    error: null,

    setSeason: (season, isManualOverride = false) =>
        set({ season, isManualOverride, error: null }),

    setLoading: (isLoading) => set({ isLoading }),

    setError: (error) => set({ error }),
}));
