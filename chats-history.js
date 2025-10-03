// Extended sample data for chats history
const chatsHistoryData = {
    // Today's date stats
    todayStats: {
        totalChats: 127,
        successChats: 95,
        failedChats: 32,
        averageRating: 4.3
    },
    
    // All chats data (simulating multiple days)
    allChats: [
        // Today's chats
        {
            id: 101,
            dateTime: '2024-01-15 14:32',
            topic: 'Flight Status',
            summary: 'User inquired about flight AA123 delay status',
            rating: 5,
            isFailedChat: false,
            category: 'Flight Status',
            messages: [
                { type: 'user', content: 'Hi, can you check the status of flight AA123?', timestamp: '14:32' },
                { type: 'assistant', content: 'Hello! I\'d be happy to help you check the status of flight AA123. Let me look that up for you.', timestamp: '14:32' },
                { type: 'assistant', content: 'Flight AA123 from New York to Los Angeles is currently delayed by 45 minutes due to weather conditions. The new estimated departure time is 3:45 PM.', timestamp: '14:33' },
                { type: 'user', content: 'Thank you for the update. Will this affect my connecting flight?', timestamp: '14:33' },
                { type: 'assistant', content: 'I can help you check your connecting flight. Could you please provide me with your connecting flight number?', timestamp: '14:34' },
                { type: 'user', content: 'It\'s flight AA456 to Chicago', timestamp: '14:34' },
                { type: 'assistant', content: 'Good news! Flight AA456 to Chicago departs at 6:30 PM, so you should have enough time to make your connection even with the delay.', timestamp: '14:35' }
            ]
        },
        {
            id: 102,
            dateTime: '2024-01-15 13:45',
            topic: 'Flight Booking',
            summary: 'User successfully booked a round-trip flight to Miami',
            rating: 4,
            isFailedChat: false,
            category: 'Flight Booking',
            messages: [
                { type: 'user', content: 'I need to book a flight to Miami for next week', timestamp: '13:45' },
                { type: 'assistant', content: 'I\'d be happy to help you book a flight to Miami! Could you please tell me your departure city and preferred travel dates?', timestamp: '13:45' },
                { type: 'user', content: 'From Boston, departing March 15th and returning March 22nd', timestamp: '13:46' },
                { type: 'assistant', content: 'Perfect! I found several options for Boston to Miami. Here are the best available flights for your dates...', timestamp: '13:46' }
            ]
        },
        {
            id: 103,
            dateTime: '2024-01-15 12:18',
            topic: 'Baggage',
            summary: 'User reported lost baggage, case created',
            rating: 3,
            isFailedChat: false,
            category: 'Baggage',
            messages: [
                { type: 'user', content: 'My baggage didn\'t arrive with my flight', timestamp: '12:18' },
                { type: 'assistant', content: 'I\'m sorry to hear about your missing baggage. I can help you report this and track it down. Can you provide me with your flight details?', timestamp: '12:18' },
                { type: 'user', content: 'Flight BA789 from London to New York yesterday', timestamp: '12:19' },
                { type: 'assistant', content: 'Thank you. I\'ve created a baggage claim case for you. Your reference number is BG123456. We\'ll track your baggage and contact you as soon as we locate it.', timestamp: '12:20' }
            ]
        },
        {
            id: 104,
            dateTime: '2024-01-15 11:22',
            topic: 'Check-in',
            summary: 'Technical issue prevented online check-in completion',
            rating: 2,
            isFailedChat: true,
            category: 'Check-in',
            messages: [
                { type: 'user', content: 'I\'m trying to check in online but getting an error', timestamp: '11:22' },
                { type: 'assistant', content: 'I\'m sorry you\'re experiencing issues with online check-in. Can you tell me what error message you\'re seeing?', timestamp: '11:22' },
                { type: 'user', content: 'It says "System temporarily unavailable"', timestamp: '11:23' },
                { type: 'assistant', content: 'I apologize for the technical difficulties. Unfortunately, I\'m unable to complete your check-in through this system. Please visit the airport check-in counter or try again later.', timestamp: '11:24' }
            ]
        },
        {
            id: 105,
            dateTime: '2024-01-15 10:15',
            topic: 'Miles',
            summary: 'User inquired about miles balance and redemption',
            rating: 5,
            isFailedChat: false,
            category: 'Miles',
            messages: [
                { type: 'user', content: 'How many miles do I have in my account?', timestamp: '10:15' },
                { type: 'assistant', content: 'I can help you check your miles balance. Your current balance is 45,230 miles. Would you like to know about redemption options?', timestamp: '10:15' },
                { type: 'user', content: 'Yes, what can I get with those miles?', timestamp: '10:16' },
                { type: 'assistant', content: 'With 45,230 miles, you can redeem for a domestic round-trip flight (25,000 miles) or upgrade to business class on your next international flight (40,000 miles).', timestamp: '10:16' }
            ]
        },
        {
            id: 106,
            dateTime: '2024-01-15 09:30',
            topic: 'At the Airport',
            summary: 'User asked for directions to gate and amenities',
            rating: 4,
            isFailedChat: false,
            category: 'At the Airport',
            messages: [
                { type: 'user', content: 'I\'m at JFK airport, how do I get to gate B12?', timestamp: '09:30' },
                { type: 'assistant', content: 'From your current location, take the AirTrain to Terminal 5, then follow signs to gates B10-B15. It should take about 10 minutes.', timestamp: '09:30' },
                { type: 'user', content: 'Are there any restaurants near that gate?', timestamp: '09:31' },
                { type: 'assistant', content: 'Yes! Near gate B12 you\'ll find Shake Shack, Starbucks, and a Hudson newsstand with snacks.', timestamp: '09:31' }
            ]
        },
        {
            id: 107,
            dateTime: '2024-01-15 08:45',
            topic: 'Vacation Planner',
            summary: 'User planned a vacation package to Hawaii',
            rating: 5,
            isFailedChat: false,
            category: 'Vacation Planner',
            messages: [
                { type: 'user', content: 'I want to plan a vacation to Hawaii', timestamp: '08:45' },
                { type: 'assistant', content: 'Hawaii sounds wonderful! What time of year are you thinking of traveling, and how many people will be going?', timestamp: '08:45' },
                { type: 'user', content: 'Two people, sometime in April', timestamp: '08:46' },
                { type: 'assistant', content: 'April is a great time to visit Hawaii! I can help you find flight and hotel packages. Would you prefer Oahu, Maui, or the Big Island?', timestamp: '08:46' }
            ]
        },
        // Yesterday's chats
        {
            id: 108,
            dateTime: '2024-01-14 16:20',
            topic: 'Flight Status',
            summary: 'User checked flight status for international flight',
            rating: 4,
            isFailedChat: false,
            category: 'Flight Status',
            messages: [
                { type: 'user', content: 'What\'s the status of flight LH401?', timestamp: '16:20' },
                { type: 'assistant', content: 'Flight LH401 from Frankfurt to New York is on time, departing at 11:20 AM local time.', timestamp: '16:20' }
            ]
        },
        {
            id: 109,
            dateTime: '2024-01-14 15:10',
            topic: 'FAQ',
            summary: 'User asked about baggage allowance policies',
            rating: 3,
            isFailedChat: false,
            category: 'FAQ',
            messages: [
                { type: 'user', content: 'What\'s the baggage allowance for economy class?', timestamp: '15:10' },
                { type: 'assistant', content: 'For economy class, you can bring one carry-on bag (22" x 14" x 9") and one personal item. Checked baggage fees apply for most domestic flights.', timestamp: '15:10' }
            ]
        },
        {
            id: 110,
            dateTime: '2024-01-14 14:30',
            topic: 'Booking',
            summary: 'User modified existing booking dates',
            rating: 4,
            isFailedChat: false,
            category: 'Booking',
            messages: [
                { type: 'user', content: 'I need to change my flight dates', timestamp: '14:30' },
                { type: 'assistant', content: 'I can help you modify your booking. Can you provide your confirmation number?', timestamp: '14:30' },
                { type: 'user', content: 'It\'s ABC123', timestamp: '14:31' },
                { type: 'assistant', content: 'I found your booking. What new dates would you like to travel?', timestamp: '14:31' }
            ]
        }
    ]
};

// Pagination settings
let currentPage = 1;
const itemsPerPage = 10;
let filteredChats = [];
let selectedDate = '';

// Initialize page when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    initializePage();
});

// Initialize the page
function initializePage() {
    setTodayDate();
    updateTodayStats();
    filterChatsByDate(getCurrentDateString());
}

// Get current date string in YYYY-MM-DD format
function getCurrentDateString() {
    return new Date().toISOString().split('T')[0];
}

// Set today's date in the date input
function setTodayDate() {
    const today = getCurrentDateString();
    document.getElementById('selectedDate').value = today;
    selectedDate = today;
    applyDateFilter();
}

// Set yesterday's date in the date input
function setYesterdayDate() {
    const yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    const yesterdayString = yesterday.toISOString().split('T')[0];
    document.getElementById('selectedDate').value = yesterdayString;
    selectedDate = yesterdayString;
    applyDateFilter();
}

// Update today's stats
function updateTodayStats() {
    document.getElementById('totalChatsToday').textContent = chatsHistoryData.todayStats.totalChats;
    document.getElementById('successChatsToday').textContent = chatsHistoryData.todayStats.successChats;
    document.getElementById('failedChatsToday').textContent = chatsHistoryData.todayStats.failedChats;
    document.getElementById('averageRatingToday').textContent = chatsHistoryData.todayStats.averageRating;
}

// Apply date filter
function applyDateFilter() {
    const dateInput = document.getElementById('selectedDate');
    selectedDate = dateInput.value;
    
    if (!selectedDate) {
        showNotification('Please select a date', 'warning');
        return;
    }
    
    // Add loading state
    const filterBtn = document.querySelector('.filter-btn');
    const originalText = filterBtn.innerHTML;
    filterBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Filtering...';
    filterBtn.disabled = true;
    
    setTimeout(() => {
        filterChatsByDate(selectedDate);
        filterBtn.innerHTML = originalText;
        filterBtn.disabled = false;
        showNotification(`Showing chats for ${formatDate(selectedDate)}`, 'success');
    }, 800);
}

// Filter chats by selected date
function filterChatsByDate(date) {
    filteredChats = chatsHistoryData.allChats.filter(chat => {
        const chatDate = chat.dateTime.split(' ')[0];
        return chatDate === date;
    });
    
    // Update stats for selected date
    updateStatsForDate(filteredChats);
    
    // Reset pagination
    currentPage = 1;
    populateChatsTable();
    updatePagination();
}

// Update stats based on filtered chats
function updateStatsForDate(chats) {
    const totalChats = chats.length;
    const successChats = chats.filter(chat => !chat.isFailedChat).length;
    const failedChats = chats.filter(chat => chat.isFailedChat).length;
    const averageRating = chats.length > 0 ? 
        (chats.reduce((sum, chat) => sum + chat.rating, 0) / chats.length).toFixed(1) : 0;
    
    document.getElementById('totalChatsToday').textContent = totalChats;
    document.getElementById('successChatsToday').textContent = successChats;
    document.getElementById('failedChatsToday').textContent = failedChats;
    document.getElementById('averageRatingToday').textContent = averageRating;
}

// Populate chats table with pagination
function populateChatsTable() {
    const tbody = document.getElementById('chatsHistoryTableBody');
    tbody.innerHTML = '';
    
    // Calculate pagination
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    const pageChats = filteredChats.slice(startIndex, endIndex);
    
    if (pageChats.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align: center; padding: 40px; color: var(--neutral-500);">
                    <i class="fas fa-calendar-times" style="font-size: 24px; margin-bottom: 12px; display: block;"></i>
                    No chats found for the selected date
                </td>
            </tr>
        `;
        return;
    }
    
    pageChats.forEach(chat => {
        const row = document.createElement('tr');
        
        // Generate star rating
        const stars = '★'.repeat(chat.rating) + '☆'.repeat(5 - chat.rating);
        
        row.innerHTML = `
            <td>${formatDateTime(chat.dateTime)}</td>
            <td><strong>${chat.topic}</strong></td>
            <td>${chat.summary}</td>
            <td><span class="rating-stars">${stars}</span></td>
            <td>
                <span class="status-badge ${chat.isFailedChat ? 'status-failed' : 'status-success'}">
                    ${chat.isFailedChat ? 'Failed' : 'Success'}
                </span>
            </td>
            <td>
                <button class="view-btn" onclick="openChatModal(${chat.id})">
                    <i class="fas fa-eye"></i> View
                </button>
            </td>
        `;
        
        tbody.appendChild(row);
    });
}

// Update pagination info and buttons
function updatePagination() {
    const totalPages = Math.ceil(filteredChats.length / itemsPerPage);
    
    document.getElementById('pageInfo').textContent = `Page ${currentPage} of ${totalPages}`;
    document.getElementById('prevBtn').disabled = currentPage === 1;
    document.getElementById('nextBtn').disabled = currentPage === totalPages || totalPages === 0;
}

// Change page
function changePage(direction) {
    const totalPages = Math.ceil(filteredChats.length / itemsPerPage);
    const newPage = currentPage + direction;
    
    if (newPage >= 1 && newPage <= totalPages) {
        currentPage = newPage;
        populateChatsTable();
        updatePagination();
    }
}

// Search chats
function searchChats() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const categoryFilter = document.getElementById('categoryFilter').value;
    
    // Start with date filtered chats
    let dateFilteredChats = chatsHistoryData.allChats.filter(chat => {
        const chatDate = chat.dateTime.split(' ')[0];
        return chatDate === selectedDate;
    });
    
    // Apply search and category filters
    filteredChats = dateFilteredChats.filter(chat => {
        const matchesSearch = searchTerm === '' || 
            chat.topic.toLowerCase().includes(searchTerm) ||
            chat.summary.toLowerCase().includes(searchTerm);
        
        const matchesCategory = categoryFilter === '' || chat.category === categoryFilter;
        
        return matchesSearch && matchesCategory;
    });
    
    // Reset pagination and update table
    currentPage = 1;
    populateChatsTable();
    updatePagination();
}

// Filter by category
function filterByCategory() {
    searchChats(); // Reuse the search function as it handles both search and category filtering
}

// Format date for display
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });
}

// Format datetime for table display
function formatDateTime(dateTimeString) {
    const date = new Date(dateTimeString.replace(' ', 'T'));
    return date.toLocaleString('en-US', {
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

// Open chat modal
function openChatModal(chatId) {
    const chat = chatsHistoryData.allChats.find(c => c.id === chatId);
    if (!chat) return;
    
    // Update modal header info
    document.getElementById('chatDate').textContent = formatDateTime(chat.dateTime);
    document.getElementById('chatTopic').textContent = chat.topic;
    document.getElementById('chatRating').textContent = `Rating: ${'★'.repeat(chat.rating)}${'☆'.repeat(5 - chat.rating)}`;
    
    // Populate chat messages
    const messagesContainer = document.getElementById('chatMessages');
    messagesContainer.innerHTML = '';
    
    chat.messages.forEach(message => {
        const messageDiv = document.createElement('div');
        messageDiv.className = `message ${message.type}`;
        
        messageDiv.innerHTML = `
            <div class="message-avatar">
                <i class="fas ${message.type === 'user' ? 'fa-user' : 'fa-robot'}"></i>
            </div>
            <div class="message-content">
                ${message.content}
                <div class="message-time">${message.timestamp}</div>
            </div>
        `;
        
        messagesContainer.appendChild(messageDiv);
    });
    
    // Show modal
    document.getElementById('chatModal').style.display = 'block';
    document.body.style.overflow = 'hidden';
}

// Close chat modal
function closeChatModal() {
    document.getElementById('chatModal').style.display = 'none';
    document.body.style.overflow = 'auto';
}

// Show notification
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: ${type === 'success' ? '#10b981' : type === 'warning' ? '#f59e0b' : '#4f46e5'};
        color: white;
        padding: 16px 24px;
        border-radius: 8px;
        box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1);
        z-index: 1001;
        animation: slideInRight 0.3s ease;
        font-weight: 500;
        max-width: 300px;
    `;
    
    notification.innerHTML = `
        <i class="fas ${type === 'success' ? 'fa-check-circle' : type === 'warning' ? 'fa-exclamation-triangle' : 'fa-info-circle'}"></i>
        ${message}
    `;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.animation = 'slideOutRight 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Close modal when clicking outside
window.onclick = function(event) {
    const modal = document.getElementById('chatModal');
    if (event.target === modal) {
        closeChatModal();
    }
}

// Handle keyboard shortcuts
document.addEventListener('keydown', function(event) {
    if (event.key === 'Escape') {
        closeChatModal();
    }
});

// Add animation keyframes for notifications (same as dashboard)
const style = document.createElement('style');
style.textContent = `
    @keyframes slideInRight {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOutRight {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);