import axiosInstance from '../axiosInstance';

// GET /api/theme/season -> { season: "Winter" | "Spring" | "Summer" | "Autumn", isManualOverride: boolean }
// Throws on network/HTTP error — caller decides the fallback.
export const getCurrentSeason = async () => {
    const { data } = await axiosInstance.get('/theme/season');
    return data;
};
