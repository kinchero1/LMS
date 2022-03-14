const express = require('express');
const fs = require('fs');
const cors= require('cors');

const app = express();

app.use(express.json())

app.use(cors())
app.get('/', (req, res) => {
    res.send("Welcome to Number to letter api")
})



app.put('/dbUpdate', (req, res) => {

    steName = req.query.ste;
    
    console.log("updating db " + steName)

    if (steName != "bahij" && steName != "perle") {
        res.json({ errorCode: 404, desc: "not found" })
    } else {

        let json = req.body;

        let filename = __dirname + "//jsonDB/" + steName + ".json";
        fs.writeFile(filename, JSON.stringify(json), (err) => {
            if (err) throw err;
            console.log('The file has been saved!');
        });
        res.end("success");
    }


})

app.get('/dbGet', (req, res) => {

    steName = req.query.ste;
    console.log("getting db " + steName)
    if (steName != "bahij" && steName != "perle") {
        res.json({ errorCode: 404, desc: "not found" })
    } else {
        fs.readFile(__dirname + "//jsonDB/" + steName + ".json", 'utf8', (err, txt) => {
            if(txt.length==0){
                   res.json({ errorCode: 404, desc: "empty file" })
            }else{
               let parsedJson =JSON.parse(txt);
        
            res.json(parsedJson);
            }
         
        })

    }
})

app.get('/dbGetMobile', (req, res) => {

    steName = req.query.ste;
    console.log("getting db " + steName)
    if (steName != "bahij" && steName != "perle") {
        res.json({ errorCode: 404, desc: "not found" })
    } else {
        fs.readFile(__dirname + "//jsonDB/" + steName + ".json", 'utf8', (err, txt) => {
            if(txt.length==0){
                   res.json({ errorCode: 404, desc: "empty file" })
            }else{
               let parsedJson =JSON.parse(txt);
        
            res.json(parsedJson.productList);
            }
         
        })

    }
})


app.get('/appAuth', (req, res) => {

    res.sendFile(__dirname + "//jsonDB/authorisation.json");
})




var listener = app.listen(3000, function () {
    console.log('Your app is listening on port ' + listener.address().port);
});