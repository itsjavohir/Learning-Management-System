import { useEffect } from 'react';
import { createPortal } from 'react-dom';
import './Modal.css';

function CloseIcon() {
    return (
        <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2">
            <path d="M6 6l12 12M18 6 6 18" />
        </svg>
    );
}

// Generic modal shell: portal + backdrop + Esc/click-outside close + scroll lock.
// Used directly for forms (title + children + footer) and as the base for ConfirmDialog.
function Modal({ isOpen, onClose, title, children, footer, size = 'md' }) {
    useEffect(() => {
        if (!isOpen) return;

        const handleKeyDown = (e) => {
            if (e.key === 'Escape') onClose();
        };

        document.addEventListener('keydown', handleKeyDown);
        document.body.style.overflow = 'hidden';

        return () => {
            document.removeEventListener('keydown', handleKeyDown);
            document.body.style.overflow = '';
        };
    }, [isOpen, onClose]);

    if (!isOpen) return null;

    return createPortal(
        <div
            className="modal-backdrop"
            onMouseDown={(e) => {
                if (e.target === e.currentTarget) onClose();
            }}
        >
            <div className={`modal-card modal-card--${size}`} role="dialog" aria-modal="true" aria-label={title}>
                <div className="modal-header">
                    <h3>{title}</h3>
                    <button type="button" className="modal-close" onClick={onClose} aria-label="Close">
                        <CloseIcon />
                    </button>
                </div>

                <div className="modal-body">{children}</div>

                {footer && <div className="modal-footer">{footer}</div>}
            </div>
        </div>,
        document.body
    );
}

export default Modal;
