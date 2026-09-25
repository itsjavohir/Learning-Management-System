import './SeasonDecoration.css';

const SPARKLE_COUNT = 10;

function Sun() {
    const sparkles = Array.from({ length: SPARKLE_COUNT });

    return (
        <div className="season-decoration">
            <div className="sun-glow" />
            {sparkles.map((_, i) => {
                const size = 3 + (i % 3);
                return (
                    <span
                        key={i}
                        className="sun-sparkle"
                        style={{
                            top: `${(i * 29) % 60}%`,
                            right: `${(i * 31) % 45}%`,
                            width: size,
                            height: size,
                            animationDuration: `${2.5 + (i % 2)}s`,
                            animationDelay: `${i % 3}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Sun;
