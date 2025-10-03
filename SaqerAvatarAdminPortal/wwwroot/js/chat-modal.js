// Shared Chat Modal Module - Used by both Dashboard and Chats History
class ChatModal {
    constructor() {
        this.modal = document.getElementById('chatModal');
        this.modalBody = document.getElementById('modalBody');
        this.init();
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
        // Close modal when clicking outside
        window.addEventListener('click', (event) => {
            if (event.target === this.modal) {
                this.close();
            }
        });

        // Handle keyboard shortcuts
        document.addEventListener('keydown', (event) => {
            if (event.key === 'Escape') {
                this.close();
            }
        });

        // Close button
        const closeBtn = this.modal.querySelector('.close');
        if (closeBtn) {
            closeBtn.addEventListener('click', () => this.close());
        }
    }

    async open(chatId) {
        if (!chatId) {
            console.error('Chat ID is required');
            return;
        }

        try {
            // Show modal with loading state
            this.show();
            this.showLoading();

            // Fetch chat details from server
            const response = await fetch(`/api/chat/${chatId}`, {
                method: 'GET',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const html = await response.text();
            this.modalBody.innerHTML = html;

        } catch (error) {
            console.error('Error loading chat details:', error);
            this.showError('Failed to load chat details. Please try again.');
        }
    }

    show() {
        this.modal.style.display = 'block';
        document.body.style.overflow = 'hidden';
        
        // Add animation class
        this.modal.querySelector('.modal-content').style.animation = 'modalSlideIn 0.3s ease';
    }

    close() {
        const modalContent = this.modal.querySelector('.modal-content');
        modalContent.style.animation = 'modalSlideOut 0.3s ease';
        
        setTimeout(() => {
            this.modal.style.display = 'none';
            document.body.style.overflow = 'auto';
            
            // Reset modal content
            this.modalBody.innerHTML = `
                <div class="loading-spinner">
                    <i class="fas fa-spinner fa-spin"></i>
                    <p>Loading chat details...</p>
                </div>
            `;
        }, 300);
    }

    showLoading() {
        this.modalBody.innerHTML = `
            <div class="loading-spinner">
                <i class="fas fa-spinner fa-spin"></i>
                <p>Loading chat details...</p>
            </div>
        `;
    }

    showError(message) {
        this.modalBody.innerHTML = `
            <div class="error-message">
                <i class="fas fa-exclamation-triangle"></i>
                <p>${message}</p>
                <button onclick="ChatModal.close()" class="btn btn-secondary">Close</button>
            </div>
        `;
    }

    // Static methods for global access
    static instance = null;

    static init() {
        if (!ChatModal.instance) {
            ChatModal.instance = new ChatModal();
        }
        return ChatModal.instance;
    }

    static open(chatId) {
        if (ChatModal.instance) {
            ChatModal.instance.open(chatId);
        }
    }

    static close() {
        if (ChatModal.instance) {
            ChatModal.instance.close();
        }
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    ChatModal.init();
});

// Global functions for backward compatibility
function openChatModal(chatId) {
    ChatModal.open(chatId);
}

function closeChatModal() {
    ChatModal.close();
}

// Form enhancement functions
class FormEnhancer {
    static debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    static enhanceSearchForm() {
        const searchInput = document.querySelector('input[name="SearchQuery"]');
        if (searchInput) {
            // Add debounced auto-search
            const debouncedSearch = this.debounce(() => {
                if (searchInput.value.length >= 3 || searchInput.value.length === 0) {
                    searchInput.closest('form').submit();
                }
            }, 500);

            searchInput.addEventListener('input', debouncedSearch);
        }
    }

    static enhanceCategoryFilter() {
        const categorySelect = document.querySelector('select[name="SelectedCategory"]');
        if (categorySelect) {
            categorySelect.addEventListener('change', () => {
                categorySelect.closest('form').submit();
            });
        }
    }
}

// Initialize form enhancements
document.addEventListener('DOMContentLoaded', function() {
    FormEnhancer.enhanceSearchForm();
    FormEnhancer.enhanceCategoryFilter();
});