import './Input.css';

function Input({ className = '', ...rest }) {
    return (
        <span className="seasonal-input">
            <input className={className} {...rest} />
        </span>
    );
}

export default Input;