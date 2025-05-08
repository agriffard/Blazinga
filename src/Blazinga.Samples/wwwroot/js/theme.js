window.setBsTheme = function (theme) {
    let effectiveTheme = theme;

    if (theme === "auto") {
        const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;
        effectiveTheme = prefersDark ? "dark" : "light";

        // Optional: auto-switch when system theme changes
        window.matchMedia("(prefers-color-scheme: dark)").onchange = (e) => {
            const newTheme = e.matches ? "dark" : "light";
            if (localStorage.getItem("bs-theme") === "auto") {
                document.documentElement.setAttribute("data-bs-theme", newTheme);
            }
        };
    }

    document.documentElement.setAttribute("data-bs-theme", effectiveTheme);
    localStorage.setItem("bs-theme", theme);
};

window.getBsTheme = function () {
    return localStorage.getItem("bs-theme") || "auto";
};
