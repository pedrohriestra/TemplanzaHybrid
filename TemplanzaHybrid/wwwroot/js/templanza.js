// wwwroot/js/templanza.js
window.templanza = (function () {

    // Crea el contenedor de toasts si no existe
    function ensureToastHolder() {
        let el = document.getElementById('toast-holder');
        if (!el) {
            el = document.createElement('div');
            el.id = 'toast-holder';
            el.className = 'toast-container position-fixed top-0 end-0 p-3';
            document.body.appendChild(el);
        }
        return el;
    }

    // Toast Bootstrap (requiere bootstrap.bundle ya cargado)
    function notify(message, title) {
        const holder = ensureToastHolder();
        const id = 't' + Date.now() + Math.random().toString(36).slice(2, 7);

        const html = `
      <div id="${id}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
        ${title ? `
          <div class="toast-header">
            <strong class="me-auto">${title}</strong>
            <button type="button" class="btn-close ms-2 mb-1" data-bs-dismiss="toast" aria-label="Close"></button>
          </div>` : ''}
        <div class="toast-body">${message}</div>
      </div>`;

        holder.insertAdjacentHTML('beforeend', html);

        const toastEl = document.getElementById(id);
        const toast = new bootstrap.Toast(toastEl, { delay: 2500 });
        toast.show();
        toastEl.addEventListener('hidden.bs.toast', () => toastEl.remove());
    }

    // Enfoca y centra el primer error visible (útil post-submit)
    function focusFirstInvalid() {
        const el = document.querySelector('.input-validation-error, .invalid, .validation-message');
        if (el) {
            el.scrollIntoView({ behavior: 'smooth', block: 'center' });
            if (['INPUT', 'SELECT', 'TEXTAREA'].includes(el.tagName)) el.focus();
        }
    }

    // Debounce simple para oninput
    function debounce(fn, wait = 300) {
        let t;
        return (...args) => {
            clearTimeout(t);
            t = setTimeout(() => fn.apply(this, args), wait);
        };
    }

    // localStorage helper
    const storage = {
        set: (k, v) => localStorage.setItem(k, JSON.stringify(v)),
        get: (k, def = null) => {
            try { const v = localStorage.getItem(k); return v ? JSON.parse(v) : def; }
            catch { return def; }
        },
        remove: (k) => localStorage.removeItem(k)
    };

    // Confirm síncrono (tu versión)
    function confirmBox(msg) {
        return confirm(msg ?? '¿Confirmás?');
    }

    console.log('templanza.js loaded');
    return { confirm: confirmBox, notify, focusFirstInvalid, debounce, storage };
})();
