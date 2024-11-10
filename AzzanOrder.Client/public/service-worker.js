self.addEventListener('push', function (event) {
    const data = event.data.json();
    const title = data.title || 'Default Title';
    const message = data.message || 'Default Message';
    const url = data.url || '/';
    const icon = data.icon || '/default-icon.png';

    const options = {
        body: message,
        icon: icon,
        data: {
            url: url
        }
    };

    event.waitUntil(
        self.registration.showNotification(title, options)
    );
});

self.addEventListener('notificationclick', function (event) {
    event.notification.close();
    event.waitUntil(
        clients.openWindow(event.notification.data.url)
    );
});

function sendPushNotification(title, message, icon) {
    const data = {
        title: title,
        message: message,
        icon: icon,
        url: '/'
    };

    self.registration.showNotification(data.title, {
        body: data.message,
        icon: data.icon,
        data: {
            url: data.url
        }
    });
}