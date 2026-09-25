import './SeasonDecoration.css';

const PARTICLE_COUNT = 22;

function Snow() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 3 + (i % 4);
                return (
                    <span
                        key={i}
                        className="snow-particle"
                        style={{
                            left: `${(i * 37) % 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${7 + (i % 8)}s`,
                            animationDelay: `${i % 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Snow;
