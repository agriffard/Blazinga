export function register(dotNetHelper, container) {
    function handleClick(event) {
        if (!container.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('CloseDropdown');
        }
    }

    document.addEventListener('click', handleClick);
    return {
        dispose: () => {
            document.removeEventListener('click', handleClick);
        }
    };
}
