(() => {
    const logs = [];
    const $ = (id) => document.getElementById(id);
    const escapeHtml = (value) => value.replace(/[&<>\"']/g, (char) => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '\"': '&quot;', "'": '&#039;' })[char]);
    const addLog = (kind, message) => {
        const now = new Date();
        const time = now.toLocaleString('tr-TR', { dateStyle: 'short', timeStyle: 'medium' });
        logs.unshift({ time, kind, message });
        const list = $('log-list');
        list.innerHTML = logs.map(log => `<div class="log-entry"><span class="log-time">${escapeHtml(log.time)}</span><span class="log-kind">[${escapeHtml(log.kind)}]</span><span>${escapeHtml(log.message)}</span></div>`).join('');
        $('log-count').textContent = logs.length;
    };

    document.querySelectorAll('.tool-tab').forEach(tab => tab.addEventListener('click', () => {
        document.querySelectorAll('.tool-tab').forEach(item => { item.classList.remove('active'); item.setAttribute('aria-selected', 'false'); });
        document.querySelectorAll('.tool-panel').forEach(panel => { panel.classList.remove('active'); panel.hidden = true; });
        tab.classList.add('active'); tab.setAttribute('aria-selected', 'true');
        $(tab.dataset.target).hidden = false;
    }));

    const bufferToHex = (buffer) => [...new Uint8Array(buffer)].map(byte => byte.toString(16).padStart(2, '0')).join('');
    $('hash-button').addEventListener('click', async () => {
        const input = $('hash-input').value;
        if (!input.trim()) { $('hash-input').focus(); return; }
        const data = new TextEncoder().encode(input);
        const hash = bufferToHex(await crypto.subtle.digest('SHA-256', data));
        $('hash-result').innerHTML = `<div class="hash-output"><code>${hash}</code><button class="copy-button" type="button" title="Kopyala">⧉</button></div>`;
        $('hash-result').querySelector('.copy-button').addEventListener('click', async (event) => { await navigator.clipboard.writeText(hash); event.currentTarget.textContent = '✓'; });
        addLog('ŞİFRELEME', `SHA-256 hash üretildi (${input.length} karakter)`);
    });
    $('hash-input').addEventListener('keydown', (event) => { if (event.key === 'Enter') $('hash-button').click(); });

    $('validate-button').addEventListener('click', () => {
        const value = $('validation-input').value.trim();
        const type = $('validation-type').value;
        const pattern = type === 'email' ? /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i : /^(?:\+?\d{1,3}[\s.-]?)?(?:\(?\d{3}\)?[\s.-]?)?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}$/;
        const valid = pattern.test(value);
        const result = $('validation-result');
        result.className = `validation-result ${valid ? 'valid' : 'invalid'}`;
        result.innerHTML = `<span class="status-dot"></span><span>${valid ? 'Geçerli format — değer kontrolü başarılı.' : 'Geçersiz format — lütfen değeri kontrol edin.'}</span>`;
        addLog('DOĞRULAMA', `${type === 'email' ? 'E-posta' : 'Telefon'} ${valid ? 'geçerli' : 'geçersiz'} olarak işaretlendi`);
    });

    $('download-logs').addEventListener('click', () => {
        const content = logs.length ? logs.map(log => `${log.time}  [${log.kind}] ${log.message}`).join('\n') : 'Henüz kaydedilmiş bir işlem yok.';
        const link = document.createElement('a'); link.href = URL.createObjectURL(new Blob([content], { type: 'text/plain;charset=utf-8' })); link.download = `sistem-gunlugu-${new Date().toISOString().slice(0, 10)}.txt`; link.click(); URL.revokeObjectURL(link.href);
        addLog('SİSTEM', 'Günlük dosyası indirildi');
    });
    $('clear-logs').addEventListener('click', () => { logs.length = 0; $('log-list').innerHTML = '<div class="empty-log">Henüz kaydedilmiş bir işlem yok.</div>'; $('log-count').textContent = '0'; });
})();
