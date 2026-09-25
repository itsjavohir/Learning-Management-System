import { SEASONS } from './seasonalCalendar';

const seasonalTheme = (background, palette, effects, uiAccents) => ({
    background,
    palette,
    effects,
    uiAccents,
});

export const seasonThemeMap = {
    [SEASONS.WINTER]: seasonalTheme('winter', { bg: '#e8f1fa', accent: '#3b82c4', formBg: '#f5faff', text: '#1e3049' }, ['snow'], ['snow-cap']),
    [SEASONS.SPRING]: seasonalTheme('spring', { bg: '#edf7ec', accent: '#4caf7d', formBg: '#f6fbf4', text: '#26402c' }, ['blossom'], ['petal']),
    [SEASONS.SUMMER]: seasonalTheme('summer', { bg: '#fff3d1', accent: '#f2994a', formBg: '#fffaf0', text: '#4a3410' }, ['sun'], ['shine']),
    [SEASONS.AUTUMN]: seasonalTheme('autumn', { bg: '#f3ece0', accent: '#c2703d', formBg: '#f8f1e6', text: '#4a2f1d' }, ['leaves'], ['leaf']),
};

export const getThemeConfig = (season, event = null) => ({
    ...(seasonThemeMap[season] || seasonThemeMap[SEASONS.AUTUMN]),
    ...(event === 'newYear' ? { effects: ['snow', 'snowman', 'christmas-lights'] } : {}),
});
