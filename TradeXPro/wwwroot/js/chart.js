// TradeX Pro - Price Chart Rendering
(function () {
    const canvas = document.getElementById('priceChart');
    const container = document.getElementById('chartCanvas');

    if (!canvas || !container) return;

    function resizeCanvas() {
        canvas.width = container.offsetWidth;
        canvas.height = container.offsetHeight;
    }

    resizeCanvas();
    const ctx = canvas.getContext('2d');

    function drawChart() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        const w = canvas.width;
        const h = canvas.height;
        const padding = { top: 20, right: 60, bottom: 30, left: 10 };
        const chartW = w - padding.left - padding.right;
        const chartH = h - padding.top - padding.bottom;

        // Generate price data (simulated uptrend)
        const dataPoints = 80;
        let prices = [];
        let price = 182;
        // Use a seeded approach for consistent chart
        let seed = 42;
        function seededRandom() {
            seed = (seed * 16807) % 2147483647;
            return (seed - 1) / 2147483646;
        }

        for (let i = 0; i < dataPoints; i++) {
            price += (seededRandom() - 0.42) * 2.2;
            prices.push(price);
        }

        const minPrice = Math.min(...prices) - 2;
        const maxPrice = Math.max(...prices) + 2;
        const priceRange = maxPrice - minPrice;

        const getX = (i) => padding.left + (i / (dataPoints - 1)) * chartW;
        const getY = (p) => padding.top + (1 - (p - minPrice) / priceRange) * chartH;

        // Grid lines
        ctx.strokeStyle = '#21262d';
        ctx.lineWidth = 1;
        for (let i = 0; i <= 5; i++) {
            const y = padding.top + (i / 5) * chartH;
            ctx.beginPath();
            ctx.moveTo(padding.left, y);
            ctx.lineTo(w - padding.right, y);
            ctx.stroke();

            // Price labels
            const priceLabel = (maxPrice - (i / 5) * priceRange).toFixed(2);
            ctx.fillStyle = '#8b949e';
            ctx.font = '11px -apple-system, sans-serif';
            ctx.textAlign = 'left';
            ctx.fillText('$' + priceLabel, w - padding.right + 8, y + 4);
        }

        // Area fill gradient
        const gradient = ctx.createLinearGradient(0, padding.top, 0, h - padding.bottom);
        gradient.addColorStop(0, 'rgba(63, 185, 80, 0.15)');
        gradient.addColorStop(1, 'rgba(63, 185, 80, 0.0)');

        ctx.beginPath();
        ctx.moveTo(getX(0), h - padding.bottom);
        for (let i = 0; i < dataPoints; i++) {
            ctx.lineTo(getX(i), getY(prices[i]));
        }
        ctx.lineTo(getX(dataPoints - 1), h - padding.bottom);
        ctx.closePath();
        ctx.fillStyle = gradient;
        ctx.fill();

        // Price line
        ctx.beginPath();
        ctx.moveTo(getX(0), getY(prices[0]));
        for (let i = 1; i < dataPoints; i++) {
            ctx.lineTo(getX(i), getY(prices[i]));
        }
        ctx.strokeStyle = '#3fb950';
        ctx.lineWidth = 2;
        ctx.stroke();

        // Current price indicator
        const lastPrice = prices[prices.length - 1];
        const lastX = getX(dataPoints - 1);
        const lastY = getY(lastPrice);

        // Dashed line at current price
        ctx.setLineDash([4, 4]);
        ctx.strokeStyle = 'rgba(63, 185, 80, 0.5)';
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(padding.left, lastY);
        ctx.lineTo(w - padding.right, lastY);
        ctx.stroke();
        ctx.setLineDash([]);

        // Price dot
        ctx.beginPath();
        ctx.arc(lastX, lastY, 5, 0, Math.PI * 2);
        ctx.fillStyle = '#3fb950';
        ctx.fill();
        ctx.beginPath();
        ctx.arc(lastX, lastY, 8, 0, Math.PI * 2);
        ctx.strokeStyle = 'rgba(63, 185, 80, 0.3)';
        ctx.lineWidth = 2;
        ctx.stroke();

        // Volume bars at bottom
        seed = 123; // Reset seed for volume
        for (let i = 0; i < dataPoints; i++) {
            const vol = seededRandom() * 30 + 5;
            const barH = vol;
            const barW = chartW / dataPoints * 0.6;
            const barX = getX(i) - barW / 2;
            const isGreen = i === 0 ? true : prices[i] >= prices[i - 1];
            ctx.fillStyle = isGreen ? 'rgba(63, 185, 80, 0.25)' : 'rgba(248, 81, 73, 0.25)';
            ctx.fillRect(barX, h - padding.bottom - barH, barW, barH);
        }

        // Time labels
        const times = ['9:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00'];
        ctx.fillStyle = '#8b949e';
        ctx.font = '11px -apple-system, sans-serif';
        ctx.textAlign = 'center';
        times.forEach((t, i) => {
            const x = padding.left + (i / (times.length - 1)) * chartW;
            ctx.fillText(t, x, h - 8);
        });
    }

    drawChart();

    window.addEventListener('resize', function () {
        resizeCanvas();
        drawChart();
    });
})();
