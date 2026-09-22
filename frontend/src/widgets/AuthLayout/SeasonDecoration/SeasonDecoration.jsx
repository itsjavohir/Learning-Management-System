import { lazy, Suspense } from 'react';
import { useThemeStore } from '../../../entities/theme/model/themeStore';
import { seasonThemeMap } from '../../../entities/theme/model/seasonThemeMap';

// Each effect is its own chunk — only the one matching the current
// season is ever downloaded.
const EFFECT_COMPONENTS = {
    snow: lazy(() => import('./Snow')),
    blossom: lazy(() => import('./Blossom')),
    sun: lazy(() => import('./Sun')),
    leaves: lazy(() => import('./Leaves')),
};

function SeasonDecoration() {
    const season = useThemeStore((state) => state.season);

    if (!season) return null;

    const effect = seasonThemeMap[season]?.effect;
    const EffectComponent = EFFECT_COMPONENTS[effect];

    if (!EffectComponent) return null;

    return (
        <Suspense fallback={null}>
            <EffectComponent />
        </Suspense>
    );
}

export default SeasonDecoration;
