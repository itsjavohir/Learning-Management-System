import './Button.css';

// variant: primary | secondary | danger | ghost
// size: sm | md
function Button({
    variant = 'primary',
    size = 'md',
    isLoading = false,
    disabled = false,
    fullWidth = false,
    className = '',
    children,
    ...rest
}) {
    const classes = [
        'btn',
        `btn--${variant}`,
        `btn--${size}`,
        fullWidth ? 'btn--full' : '',
        className,
    ].filter(Boolean).join(' ');

    return (
        <button className={classes} disabled={disabled || isLoading} {...rest}>
            {isLoading && <span className="btn-spinner" aria-hidden="true" />}
            <span className={isLoading ? 'btn-label btn-label--loading' : 'btn-label'}>
                {children}
            </span>
        </button>
    );
}

export default Button;
