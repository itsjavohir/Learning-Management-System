import { create } from 'zustand';
import { tokenStorage } from '../../../shared/lib/tokenStorage';

export const useAuthStore = create((set) => ({
    user: null,
    isAuthenticated: tokenStorage.isAuthenticated(),

    setSession: (user, accessToken, refreshToken) => {
        tokenStorage.setTokens(accessToken, refreshToken);
        set({ user, isAuthenticated: true });
    },

    clearSession: () => {
        tokenStorage.clearTokens();
        set({ user: null, isAuthenticated: false });
    },
}));