// Sample data for the dashboard
const dashboardData = {
    stats: {
        totalChats: 2847,
        successChats: 2134,
        failedChats: 713,
        agentChats: 456
    },
    categories: {
        'Flight Status': 687,
        'Flight Booking': 542,
        'At the Airport': 398,
        'Vacation Planner': 321,
        'Baggage': 289,
        'Miles': 234,
        'FAQ': 198,
        'Booking': 156,
        'Check-in': 89,
        'Others': 33
    },
    dailyChats: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        data: [285, 342, 398, 456, 523, 389, 454]
    },
    successFailedDaily: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        success: [215, 289, 321, 378, 423, 312, 376],
        failed: [70, 53, 77, 78, 100, 77, 78]
    },
    ratingsDaily: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        data: [4.2, 4.5, 4.1, 4.3, 4.6, 4.4, 4.5]
    },
    recentChats: [
        {
            id: 1,
            dateTime: '2024-01-15 14:32',
            topic: 'Flight Status',
            summary: 'User inquired about flight AA123 delay status',
            rating: 5,
            isFailedChat: false,
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
            id: 2,
            dateTime: '2024-01-15 13:45',
            topic: 'Flight Booking',
            summary: 'User successfully booked a round-trip flight to Miami',
            rating: 4,
            isFailedChat: false,
            messages: [
                { type: 'user', content: 'I need to book a flight to Miami for next week', timestamp: '13:45' },
                { type: 'assistant', content: 'I\'d be happy to help you book a flight to Miami! Could you please tell me your departure city and preferred travel dates?', timestamp: '13:45' },
                { type: 'user', content: 'From Boston, departing March 15th and returning March 22nd', timestamp: '13:46' },
                { type: 'assistant', content: 'Perfect! I found several options for Boston to Miami. Here are the best available flights for your dates...', timestamp: '13:46' }
            ]
        },
        {
            id: 3,
            dateTime: '2024-01-15 12:18',
            topic: 'Baggage',
            summary: 'User reported lost baggage, case created',
            rating: 3,
            isFailedChat: false,
            messages: [
                { type: 'user', content: 'My baggage didn\'t arrive with my flight', timestamp: '12:18' },
                { type: 'assistant', content: 'I\'m sorry to hear about your missing baggage. I can help you report this and track it down. Can you provide me with your flight details?', timestamp: '12:18' },
                { type: 'user', content: 'Flight BA789 from London to New York yesterday', timestamp: '12:19' },
                { type: 'assistant', content: 'Thank you. I\'ve created a baggage claim case for you. Your reference number is BG123456. We\'ll track your baggage and contact you as soon as we locate it.', timestamp: '12:20' }
            ]
        },
        {
            id: 4,
            dateTime: '2024-01-15 11:22',
            topic: 'Check-in',
            summary: 'Technical issue prevented online check-in completion',
            rating: 2,
            isFailedChat: true,
            messages: [
                { type: 'user', content: 'I\'m trying to check in online but getting an error', timestamp: '11:22' },
                { type: 'assistant', content: 'I\'m sorry you\'re experiencing issues with online check-in. Can you tell me what error message you\'re seeing?', timestamp: '11:22' },
                { type: 'user', content: 'It says "System temporarily unavailable"', timestamp: '11:23' },
                { type: 'assistant', content: 'I apologize for the technical difficulties. Unfortunately, I\'m unable to complete your check-in through this system. Please visit the airport check-in counter or try again later.', timestamp: '11:24' }
            ]
        },
        {
            id: 5,
            dateTime: '2024-01-15 10:15',
            topic: 'Miles',
            summary: 'User inquired about miles balance and redemption',
            rating: 5,
            isFailedChat: false,
            messages: [
                { type: 'user', content: 'How many miles do I have in my account?', timestamp: '10:15' },
                { type: 'assistant', content: 'I can help you check your miles balance. Your current balance is 45,230 miles. Would you like to know about redemption options?', timestamp: '10:15' },
                { type: 'user', content: 'Yes, what can I get with those miles?', timestamp: '10:16' },
                { type: 'assistant', content: 'With 45,230 miles, you can redeem for a domestic round-trip flight (25,000 miles) or upgrade to business class on your next international flight (40,000 miles).', timestamp: '10:16' }
            ]
        }
    ]
};

// Chart instances
let categoryChart, dailyChatsChart, successFailedChart, ratingsChart;

// Initialize dashboard when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    initializeDateFilters();
    updateStats();
    initializeCharts();
    populateChatsTable();
});

// Initialize date filters with default values
function initializeDateFilters() {
    const today = new Date();
    const sevenDaysAgo = new Date(today);
    sevenDaysAgo.setDate(today.getDate() - 7);
    
    document.getElementById('dateFrom').value = sevenDaysAgo.toISOString().split('T')[0];
    document.getElementById('dateTo').value = today.toISOString().split('T')[0];
}

// Update stats cards
function updateStats() {
    document.getElementById('totalChats').textContent = dashboardData.stats.totalChats.toLocaleString();
    document.getElementById('successChats').textContent = dashboardData.stats.successChats.toLocaleString();
    document.getElementById('failedChats').textContent = dashboardData.stats.failedChats.toLocaleString();
    document.getElementById('agentChats').textContent = dashboardData.stats.agentChats.toLocaleString();
}

// Initialize all charts
function initializeCharts() {
    createCategoryPieChart();
    createDailyChatsBarChart();
    createSuccessFailedBarChart();
    createRatingsLineChart();
}

// Create pie chart for chat categories
function createCategoryPieChart() {
    const ctx = document.getElementById('categoryPieChart').getContext('2d');
    
    const colors = [
        '#4f46e5', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6',
        '#06b6d4', '#84cc16', '#f97316', '#ec4899', '#6b7280'
    ];
    
    categoryChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: Object.keys(dashboardData.categories),
            datasets: [{
                data: Object.values(dashboardData.categories),
                backgroundColor: colors,
                borderWidth: 2,
                borderColor: '#ffffff'
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        padding: 20,
                        usePointStyle: true,
                        font: {
                            size: 12
                        }
                    }
                },
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            const label = context.label || '';
                            const value = context.parsed;
                            const total = context.dataset.data.reduce((a, b) => a + b, 0);
                            const percentage = ((value / total) * 100).toFixed(1);
                            return `${label}: ${value} (${percentage}%)`;
                        }
                    }
                }
            }
        }
    });
}

// Create bar chart for daily chat volume
function createDailyChatsBarChart() {
    const ctx = document.getElementById('dailyChatsBarChart').getContext('2d');
    
    dailyChatsChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dashboardData.dailyChats.labels,
            datasets: [{
                label: 'Total Chats',
                data: dashboardData.dailyChats.data,
                backgroundColor: '#4f46e5',
                borderColor: '#3730a3',
                borderWidth: 1,
                borderRadius: 6,
                borderSkipped: false
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: '#e2e8f0'
                    },
                    ticks: {
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                },
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                }
            }
        }
    });
}

// Create bar chart for success vs failed chats
function createSuccessFailedBarChart() {
    const ctx = document.getElementById('successFailedBarChart').getContext('2d');
    
    successFailedChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dashboardData.successFailedDaily.labels,
            datasets: [
                {
                    label: 'Success',
                    data: dashboardData.successFailedDaily.success,
                    backgroundColor: '#10b981',
                    borderColor: '#059669',
                    borderWidth: 1,
                    borderRadius: 6,
                    borderSkipped: false
                },
                {
                    label: 'Failed',
                    data: dashboardData.successFailedDaily.failed,
                    backgroundColor: '#ef4444',
                    borderColor: '#dc2626',
                    borderWidth: 1,
                    borderRadius: 6,
                    borderSkipped: false
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        padding: 20,
                        usePointStyle: true,
                        font: {
                            size: 12
                        }
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    stacked: false,
                    grid: {
                        color: '#e2e8f0'
                    },
                    ticks: {
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                },
                x: {
                    stacked: false,
                    grid: {
                        display: false
                    },
                    ticks: {
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                }
            }
        }
    });
}

// Create line chart for ratings
function createRatingsLineChart() {
    const ctx = document.getElementById('ratingsLineChart').getContext('2d');
    
    ratingsChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: dashboardData.ratingsDaily.labels,
            datasets: [{
                label: 'Average Rating',
                data: dashboardData.ratingsDaily.data,
                borderColor: '#f59e0b',
                backgroundColor: 'rgba(245, 158, 11, 0.1)',
                borderWidth: 3,
                fill: true,
                tension: 0.4,
                pointBackgroundColor: '#f59e0b',
                pointBorderColor: '#ffffff',
                pointBorderWidth: 2,
                pointRadius: 6,
                pointHoverRadius: 8
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: false,
                    min: 1,
                    max: 5,
                    grid: {
                        color: '#e2e8f0'
                    },
                    ticks: {
                        stepSize: 0.5,
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                },
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        font: {
                            size: 12
                        },
                        color: '#64748b'
                    }
                }
            }
        }
    });
}

// Populate chats table
function populateChatsTable() {
    const tbody = document.getElementById('chatsTableBody');
    tbody.innerHTML = '';
    
    dashboardData.recentChats.forEach(chat => {
        const row = document.createElement('tr');
        
        // Generate star rating
        const stars = '★'.repeat(chat.rating) + '☆'.repeat(5 - chat.rating);
        
        row.innerHTML = `
            <td>${chat.dateTime}</td>
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

// Open chat modal
function openChatModal(chatId) {
    const chat = dashboardData.recentChats.find(c => c.id === chatId);
    if (!chat) return;
    
    // Update modal header info
    document.getElementById('chatDate').textContent = chat.dateTime;
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

// Apply date filter
function applyDateFilter() {
    const fromDate = document.getElementById('dateFrom').value;
    const toDate = document.getElementById('dateTo').value;
    
    if (!fromDate || !toDate) {
        alert('Please select both from and to dates');
        return;
    }
    
    if (new Date(fromDate) > new Date(toDate)) {
        alert('From date cannot be later than to date');
        return;
    }
    
    // Add loading state
    const filterBtn = document.querySelector('.filter-btn');
    const originalText = filterBtn.innerHTML;
    filterBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Filtering...';
    filterBtn.disabled = true;
    
    // Simulate API call delay
    setTimeout(() => {
        // In a real application, you would fetch filtered data from your API
        console.log(`Filtering data from ${fromDate} to ${toDate}`);
        
        // Reset button
        filterBtn.innerHTML = originalText;
        filterBtn.disabled = false;
        
        // Show success message
        showNotification('Data filtered successfully!', 'success');
    }, 1500);
}

// Show notification
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: ${type === 'success' ? '#10b981' : '#4f46e5'};
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
        <i class="fas ${type === 'success' ? 'fa-check-circle' : 'fa-info-circle'}"></i>
        ${message}
    `;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.animation = 'slideOutRight 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Add animation keyframes for notifications
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

// Resize charts when window is resized
window.addEventListener('resize', function() {
    if (categoryChart) categoryChart.resize();
    if (dailyChatsChart) dailyChatsChart.resize();
    if (successFailedChart) successFailedChart.resize();
    if (ratingsChart) ratingsChart.resize();
});

// Auto-refresh data every 5 minutes (in a real application)
setInterval(() => {
    console.log('Auto-refreshing dashboard data...');
    // In a real application, you would fetch fresh data from your API
}, 300000);