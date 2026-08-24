document.addEventListener('DOMContentLoaded', () => {
    const currentPath = window.location.pathname.toLowerCase();
    const menuItems = document.querySelectorAll('.menu-item');

    menuItems.forEach((item) => {
        const link = item.querySelector('.menu-link');
        if (link) {
            const linkPath = new URL(link.href, window.location.origin).pathname.toLowerCase();
            if (currentPath.includes(linkPath)) {
                document.getElementById("head-title").textContent = item.textContent;
            }
        }
    });
});
