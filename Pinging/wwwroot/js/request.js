class Request {
    static PostTracert(url, data) {
        return new Promise((resolve, rejects) => {
            fetch(url, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => { resolve(data) })
                .catch(error => rejects(error));
        });
    }

    static Post(url, data) {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => {
                    resolve(data)
                })
                .catch(error => reject(error));
        })
    }

    static test() {
        console.log("test")
    }
}