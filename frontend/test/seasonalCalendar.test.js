import test from 'node:test';
import assert from 'node:assert/strict';
import { getSeasonState, SEASONS } from '../src/entities/theme/model/seasonalCalendar.js';

test('maps season boundaries', () => {
    assert.equal(getSeasonState(new Date(2026, 1, 28)).season, SEASONS.WINTER);
    assert.equal(getSeasonState(new Date(2026, 2, 1)).season, SEASONS.SPRING);
    assert.equal(getSeasonState(new Date(2026, 4, 30)).season, SEASONS.SPRING);
    assert.equal(getSeasonState(new Date(2026, 5, 1)).season, SEASONS.SUMMER);
    assert.equal(getSeasonState(new Date(2026, 8, 1)).season, SEASONS.AUTUMN);
    assert.equal(getSeasonState(new Date(2026, 11, 1)).season, SEASONS.WINTER);
});

test('marks the new year event and transition dates', () => {
    assert.deepEqual(getSeasonState(new Date(2026, 11, 24)), {
        season: SEASONS.WINTER,
        event: 'newYear',
        transition: 0,
    });
    assert.equal(getSeasonState(new Date(2026, 2, 1)).transition, 1);
});