import './SeasonDecoration.css';

const PARTICLE_COUNT = 16;

function Blossom() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 7 + (i % 6);
                return (
                    <span
                        key={i}
                        className="blossom-particle"
                        style={{
                            left: `${(i * 43) % 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${8 + (i % 6)}s`,
                            animationDelay: `${i % 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Blossom;
