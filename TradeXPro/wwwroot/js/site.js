// TradeX Pro - Site-wide JavaScript

// Tab switching functionality
document.addEventListener('DOMContentLoaded', function () {
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
                    btnOrder.textContent = 'Buy ' + document.querySelector('.ticker-symbol').textContent;
                }
            } else {
                this.classList.add('sell-tab');
                this.style.background = 'var(--red)';
                this.style.color = '#fff';
                if (btnOrder) {
                    btnOrder.style.background = 'var(--red)';
                    btnOrder.textContent = 'Sell ' + document.querySelector('.ticker-symbol').textContent;
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
});
