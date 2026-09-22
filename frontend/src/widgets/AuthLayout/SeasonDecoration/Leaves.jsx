import './SeasonDecoration.css';

const PARTICLE_COUNT = 18;

function Leaves() {
    const particles = Array.from({ length: PARTICLE_COUNT });

    return (
        <div className="season-decoration">
            {particles.map((_, i) => {
                const size = 8 + Math.random() * 6;
                return (
                    <span
                        key={i}
                        className="leaf-particle"
                        style={{
                            left: `${Math.random() * 100}%`,
                            width: size,
                            height: size,
                            animationDuration: `${8 + Math.random() * 7}s`,
                            animationDelay: `${Math.random() * 8}s`,
                        }}
                    />
                );
            })}
        </div>
    );
}

export default Leaves;
