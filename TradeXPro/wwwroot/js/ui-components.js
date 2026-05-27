// TradeX Pro - UI Components
// Handles: Save button spinner, Toast notifications, Delete confirmation modal

// ==========================================
// 1. SAVE BUTTON SPINNER + DOUBLE-CLICK PREVENTION
// ==========================================
function initSaveButtons() {
    document.querySelectorAll('[data-save-btn]').forEach(btn => {
        btn.addEventListener('click', async function (e) {
            e.preventDefault();
            if (this.disabled) return;

            const originalText = this.innerHTML;
            const form = this.closest('form');

            // Disable and show spinner
            this.disabled = true;
            this.classList.add('btn-saving');
            this.innerHTML = '<span class="spinner"></span> Saving...';

            try {
                // If inside a form, submit it
                if (form && form.getAttribute('data-api-url')) {
                    const url = form.getAttribute('data-api-url');
                    const method = form.getAttribute('data-api-method') || 'POST';
                    const formData = new FormData(form);
                    const data = Object.fromEntries(formData.entries());

                    const response = await fetch(url, {
                        method: method,
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(data)
                    });

                    const result = await response.json();

                    if (result.success) {
                        showToast('Changes saved successfully!', 'success');
                    } else {
                        showToast(result.message || 'Save failed', 'error');
                    }
                } else {
                    // Simulate save for mockup
                    await new Promise(resolve => setTimeout(resolve, 1000));
                    showToast('Changes saved successfully!', 'success');
                }
            } catch (error) {
                showToast('An error occurred. Please try again.', 'error');
            } finally {
                // Re-enable button
                this.disabled = false;
                this.classList.remove('btn-saving');
                this.innerHTML = originalText;
            }
        });
    });
}

// ==========================================
// 2. TOAST NOTIFICATIONS (Bottom-left, 5s auto-close)
// ==========================================
let toastContainer = null;

function getToastContainer() {
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.className = 'toast-container';
        document.body.appendChild(toastContainer);
    }
    return toastContainer;
}

function showToast(message, type = 'success', duration = 5000) {
    const container = getToastContainer();

    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;

    const icons = {
        success: '&#10003;',
        error: '&#10007;',
        warning: '&#9888;',
        info: '&#8505;'
    };

    toast.innerHTML = `
        <div class="toast-icon">${icons[type] || icons.info}</div>
        <div class="toast-message">${message}</div>
        <button class="toast-close" onclick="dismissToast(this)">&times;</button>
    `;

    container.appendChild(toast);

    // Trigger slide-in animation
    requestAnimationFrame(() => {
        toast.classList.add('toast-visible');
    });

    // Auto-dismiss after duration
    const timeout = setTimeout(() => {
        dismissToast(toast.querySelector('.toast-close'));
    }, duration);

    toast.dataset.timeout = timeout;
}

function dismissToast(closeBtn) {
    const toast = closeBtn.closest('.toast');
    if (!toast) return;

    clearTimeout(toast.dataset.timeout);
    toast.classList.remove('toast-visible');
    toast.classList.add('toast-hiding');

    setTimeout(() => {
        toast.remove();
    }, 300);
}

// ==========================================
// 3. DELETE CONFIRMATION MODAL
// ==========================================
let deleteModal = null;
let deleteCallback = null;

function createDeleteModal() {
    if (deleteModal) return;

    deleteModal = document.createElement('div');
    deleteModal.className = 'modal-overlay';
    deleteModal.id = 'deleteModal';
    deleteModal.innerHTML = `
        <div class="modal-content">
            <div class="modal-header">
                <span class="modal-icon">&#9888;</span>
                <h3 class="modal-title">Delete Confirmation</h3>
            </div>
            <div class="modal-body">
                <p id="deleteModalMessage">Are you sure you want to delete this item?</p>
                <p class="modal-warning">This action cannot be undone.</p>
            </div>
            <div class="modal-footer">
                <button class="btn-secondary" onclick="closeDeleteModal()">Cancel</button>
                <button class="btn-danger" id="confirmDeleteBtn" onclick="confirmDelete()">Delete</button>
            </div>
        </div>
    `;

    document.body.appendChild(deleteModal);

    // Close on backdrop click
    deleteModal.addEventListener('click', function (e) {
        if (e.target === deleteModal) closeDeleteModal();
    });

    // Close on ESC key
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && deleteModal.classList.contains('modal-visible')) {
            closeDeleteModal();
        }
    });
}

function showDeleteConfirmation(message, callback) {
    createDeleteModal();
    deleteCallback = callback;

    document.getElementById('deleteModalMessage').textContent = message || 'Are you sure you want to delete this item?';

    requestAnimationFrame(() => {
        deleteModal.classList.add('modal-visible');
    });
}

function closeDeleteModal() {
    if (deleteModal) {
        deleteModal.classList.remove('modal-visible');
        deleteCallback = null;
    }
}

function confirmDelete() {
    if (deleteCallback) {
        deleteCallback();
    }
    closeDeleteModal();
    showToast('Item deleted successfully!', 'success');
}

// Initialize delete buttons
function initDeleteButtons() {
    document.querySelectorAll('[data-delete-btn]').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            const message = this.getAttribute('data-delete-message') || 'Are you sure you want to delete this item?';
            const url = this.getAttribute('data-delete-url');
            const row = this.closest('tr') || this.closest('[data-delete-item]');

            showDeleteConfirmation(message, async () => {
                if (url) {
                    try {
                        const response = await fetch(url, { method: 'DELETE' });
                        const result = await response.json();
                        if (result.success && row) {
                            row.style.transition = 'opacity 0.3s';
                            row.style.opacity = '0';
                            setTimeout(() => row.remove(), 300);
                        }
                    } catch (error) {
                        showToast('Delete failed. Please try again.', 'error');
                    }
                } else {
                    // Mockup mode: just remove the element
                    if (row) {
                        row.style.transition = 'opacity 0.3s';
                        row.style.opacity = '0';
                        setTimeout(() => row.remove(), 300);
                    }
                }
            });
        });
    });
}

// ==========================================
// INITIALIZE ON DOM READY
// ==========================================
document.addEventListener('DOMContentLoaded', function () {
    initSaveButtons();
    initDeleteButtons();
});
