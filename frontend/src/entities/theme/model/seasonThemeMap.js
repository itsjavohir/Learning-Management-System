import { SEASON } from './types';

// Display name + decorative effect used per season.
// The "effect" name maps 1:1 to the lazy-loaded component in
// widgets/AuthLayout/SeasonDecoration (Snow, Blossom, Sun, Leaves).
export const seasonThemeMap = {
    [SEASON.WINTER]: {
        displayName: 'Winter',
        effect: 'snow',
    },
    [SEASON.SPRING]: {
        displayName: 'Spring',
        effect: 'blossom',
    },
    [SEASON.SUMMER]: {
        displayName: 'Summer',
        effect: 'sun',
    },
    [SEASON.AUTUMN]: {
        displayName: 'Autumn',
        effect: 'leaves',
    },
};
