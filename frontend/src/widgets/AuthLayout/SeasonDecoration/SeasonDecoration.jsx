import { lazy, Suspense } from 'react';
import { useThemeStore } from '../../../entities/theme/model/themeStore';

// Each effect is its own chunk — only the one matching the current
// season is ever downloaded.
const EFFECT_COMPONENTS = {
    snow: lazy(() => import('./Snow')),
    snowman: lazy(() => import('./effects/Snowman')),
    'christmas-lights': lazy(() => import('./effects/ChristmasLights')),
    blossom: lazy(() => import('./Blossom')),
    sun: lazy(() => import('./Sun')),
    leaves: lazy(() => import('./Leaves')),
};

function SeasonDecoration() {
    const effects = useThemeStore((state) => state.effects);

    if (!effects.length) return null;

    return (
        <Suspense fallback={null}>
            {effects.map((effect) => {
                const EffectComponent = EFFECT_COMPONENTS[effect];
                return EffectComponent ? <EffectComponent key={effect} /> : null;
            })}
        </Suspense>
    );
}

export default SeasonDecoration;
