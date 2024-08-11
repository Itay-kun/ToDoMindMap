In order to start the server you must have the following installed:
- Node.js
- npm
- MySQL

To start the server, you must first install the dependencies by running the following command:
```
npm install
```

Then, go to db.js and change the following lines to match your MySQL configuration:
```
const pool = mysql.createPool({
    host: 'localhost
    user: [YOUR USERNAME],
    password: [YOUR PASSWORD]
    database: 'todo'
});
```

Then you can start the server by running the following command:
```
npm start
```

The server will be running on port 5000 by default.
