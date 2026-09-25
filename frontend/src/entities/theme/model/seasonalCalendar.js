export const SEASONS = {
    WINTER: 'winter',
    SPRING: 'spring',
    SUMMER: 'summer',
    AUTUMN: 'autumn',
};

const SEASON_BOUNDARIES = [
    { month: 2, day: 1, season: SEASONS.SPRING },
    { month: 5, day: 1, season: SEASONS.SUMMER },
    { month: 8, day: 1, season: SEASONS.AUTUMN },
    { month: 11, day: 1, season: SEASONS.WINTER },
];

export function getSeasonState(date = new Date()) {
    const month = date.getMonth();
    const season = month === 11 || month <= 1
        ? SEASONS.WINTER
        : month <= 4
            ? SEASONS.SPRING
            : month <= 7
                ? SEASONS.SUMMER
                : SEASONS.AUTUMN;

    const event = month === 11 && date.getDate() >= 20 ? 'newYear' : null;
    const transition = SEASON_BOUNDARIES.some(({ month: boundaryMonth, day }) => (
        month === boundaryMonth && date.getDate() === day
    )) ? 1 : 0;

    return { season, event, transition };
}