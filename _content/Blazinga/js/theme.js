window.setBootstrapTheme = (theme) => {
    window.currentTheme = theme;
    const apply = () => {
        let t = window.currentTheme;
        if (t === 'auto') {
            const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
            t = prefersDark ? 'dark' : 'light';
        }
        document.documentElement.setAttribute('data-bs-theme', t);
    };

    apply();

    if (!window._themeListenerSet) {
        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', apply);
        window._themeListenerSet = true;
    }
};
