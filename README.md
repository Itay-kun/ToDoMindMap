להרצת השרת:
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

הרצת ממשק המשתמש:
לאחר הרצת השרת יש לפתוח את קובץ MindOrgenizerToDo.csproj וללחוץ על play בסביבת ה visual studio ולעקוב אחרי השלבים במדריך למשתמש שבסוף קובץ האיפיון 
