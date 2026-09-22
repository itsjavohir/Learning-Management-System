import './SeasonDecoration.css';

const PARTICLE_COUNT = 22;

function Snow() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 3 + Math.random() * 4;
                return (
                    <span
                        key={i}
                        className="snow-particle"
                        style={{
                            left: `${Math.random() * 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${7 + Math.random() * 8}s`,
                            animationDelay: `${Math.random() * 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Snow;
