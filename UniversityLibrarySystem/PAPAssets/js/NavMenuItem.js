document.addEventListener('DOMContentLoaded', function () {
    const currentPath = window.location.pathname.toLowerCase();
    const menuItems = document.querySelectorAll('.menu-item');

    menuItems.forEach(function (item) {
        const link = item.querySelector('.menu-link');
        if (link && link.href) {
            const linkPath = new URL(link.href, window.location.origin).pathname.toLowerCase();

            if (currentPath.includes(linkPath)) {
                item.classList.add('active');

                let parent = item.parentElement.closest('.menu-item');
                while (parent) {
                    parent.classList.add('open', 'active');
                    parent = parent.parentElement.closest('.menu-item');
                }
            }
        }
    });
});