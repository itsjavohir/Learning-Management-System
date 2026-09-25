import test from 'node:test';
import assert from 'node:assert/strict';

import { NAV_GROUPS } from '../src/widgets/sidebar/sidebarConfig.js';

test('sidebar includes a dedicated Settings link for the settings dashboard', () => {
  const settingsItems = NAV_GROUPS.flatMap((group) => group.items);
  const settingsItem = settingsItems.find((item) => item.to === '/settings');

  assert.ok(settingsItem, 'Settings navigation item should exist');
  assert.equal(settingsItem.label, 'Settings');
});
