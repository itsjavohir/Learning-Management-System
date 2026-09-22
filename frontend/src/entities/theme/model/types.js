// Season values must match the backend response exactly
// GET /api/theme/season -> { season: "Winter" | "Spring" | "Summer" | "Autumn" }

export const SEASON = {
    WINTER: 'Winter',
    SPRING: 'Spring',
    SUMMER: 'Summer',
    AUTUMN: 'Autumn',
};

export const SEASONS = Object.values(SEASON);

export const isValidSeason = (value) => SEASONS.includes(value);
