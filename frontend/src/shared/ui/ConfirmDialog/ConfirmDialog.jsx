import Modal from '../Modal/Modal';
import Button from '../Button/Button';
import './ConfirmDialog.css';

// Reusable "are you sure?" dialog — replaces native confirm() everywhere
// (delete user, delete course, delete group, etc.)
function ConfirmDialog({
    isOpen,
    onClose,
    onConfirm,
    title = 'Are you sure?',
    description,
    confirmLabel = 'Delete',
    cancelLabel = 'Cancel',
    isLoading = false,
    error,
}) {
    return (
        <Modal
            isOpen={isOpen}
            onClose={onClose}
            title={title}
            size="sm"
            footer={
                <>
                    <Button variant="secondary" onClick={onClose} disabled={isLoading}>
                        {cancelLabel}
                    </Button>
                    <Button variant="danger" onClick={onConfirm} isLoading={isLoading}>
                        {confirmLabel}
                    </Button>
                </>
            }
        >
            <div className="confirm-dialog-wrap">
                <div className="confirm-dialog-icon" aria-hidden="true">!</div>
                {error && <div className="confirm-dialog-error">{error}</div>}
                {description && <p className="confirm-dialog-text">{description}</p>}
            </div>
        </Modal>
    );
}

export default ConfirmDialog;
