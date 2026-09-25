import './SeasonDecoration.css';

const PARTICLE_COUNT = 18;

function Leaves() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 8 + (i % 6);
                return (
                    <span
                        key={i}
                        className="leaf-particle"
                        style={{
                            left: `${(i * 47) % 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${8 + (i % 7)}s`,
                            animationDelay: `${i % 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Leaves;
