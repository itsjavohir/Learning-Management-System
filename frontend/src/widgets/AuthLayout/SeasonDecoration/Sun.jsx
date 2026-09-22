import './SeasonDecoration.css';

const SPARKLE_COUNT = 10;

function Sun() {
    const sparkles = Array.from({ length: SPARKLE_COUNT });

    return (
        <div className="season-decoration">
            <div className="sun-glow" />
            {sparkles.map((_, i) => {
                const size = 3 + Math.random() * 3;
                return (
                    <span
                        key={i}
                        className="sun-sparkle"
                        style={{
                            top: `${Math.random() * 60}%`,
                            right: `${Math.random() * 45}%`,
                            width: size,
                            height: size,
                            animationDuration: `${2.5 + Math.random() * 2}s`,
                            animationDelay: `${Math.random() * 3}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Sun;
