import './SeasonDecoration.css';

const PARTICLE_COUNT = 16;

function Blossom() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 7 + Math.random() * 6;
                return (
                    <span
                        key={i}
                        className="blossom-particle"
                        style={{
                            left: `${Math.random() * 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${8 + Math.random() * 6}s`,
                            animationDelay: `${Math.random() * 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Blossom;
