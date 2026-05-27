// TradeX Pro - Site-wide JavaScript

// ==========================================
// THEME MANAGEMENT
// ==========================================
(function () {
    // Apply saved theme immediately to prevent flash
    const savedTheme = localStorage.getItem('tradexpro-theme') || 'dark';
    document.documentElement.setAttribute('data-theme', savedTheme);
})();

document.addEventListener('DOMContentLoaded', function () {
    // Theme toggle
    const themeToggle = document.getElementById('themeToggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            const currentTheme = document.documentElement.getAttribute('data-theme') || 'dark';
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            document.documentElement.setAttribute('data-theme', newTheme);
            localStorage.setItem('tradexpro-theme', newTheme);
        });
    }

    // ==========================================
    // TAB & UI INTERACTIONS
    // ==========================================

    // Order tabs (Buy/Sell)
    const orderTabs = document.querySelectorAll('.order-tabs button');
    const btnOrder = document.querySelector('.btn-order');

    orderTabs.forEach(tab => {
        tab.addEventListener('click', function () {
            orderTabs.forEach(t => {
                t.classList.remove('buy-tab', 'sell-tab');
                t.style.background = 'transparent';
                t.style.color = 'var(--text-secondary)';
            });

            if (this.textContent === 'Buy') {
                this.classList.add('buy-tab');
                this.style.background = 'var(--green)';
                this.style.color = '#fff';
                if (btnOrder) {
                    btnOrder.style.background = 'var(--green)';
                    const ticker = document.querySelector('.ticker-symbol');
                    btnOrder.textContent = 'Buy ' + (ticker ? ticker.textContent : '');
                }
            } else {
                this.classList.add('sell-tab');
                this.style.background = 'var(--red)';
                this.style.color = '#fff';
                if (btnOrder) {
                    btnOrder.style.background = 'var(--red)';
                    const ticker = document.querySelector('.ticker-symbol');
                    btnOrder.textContent = 'Sell ' + (ticker ? ticker.textContent : '');
                }
            }
        });
    });

    // Timeframe buttons
    const timeframeBtns = document.querySelectorAll('.timeframes button');
    timeframeBtns.forEach(btn => {
        btn.addEventListener('click', function () {
            timeframeBtns.forEach(b => b.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // Position/Orders tab switching
    const tabRows = document.querySelectorAll('.tab-row');
    tabRows.forEach(row => {
        const buttons = row.querySelectorAll('button');
        buttons.forEach(btn => {
            btn.addEventListener('click', function () {
                buttons.forEach(b => b.classList.remove('active'));
                this.classList.add('active');
            });
        });
    });

    // Toggle switches
    const toggleSwitches = document.querySelectorAll('.toggle-switch');
    toggleSwitches.forEach(toggle => {
        toggle.addEventListener('click', function () {
            this.classList.toggle('active');
        });
    });

    // User dropdown menu
    const userAvatar = document.getElementById('userAvatar');
    const userDropdown = document.getElementById('userDropdown');
    if (userAvatar && userDropdown) {
        userAvatar.addEventListener('click', function (e) {
            e.stopPropagation();
            userDropdown.classList.toggle('show');
        });

        document.addEventListener('click', function (e) {
            if (!userDropdown.contains(e.target) && e.target !== userAvatar) {
                userDropdown.classList.remove('show');
            }
        });
    }
});
