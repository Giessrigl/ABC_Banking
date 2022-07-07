const express = require('express');
const bodyparser = require('body-parser');
const path = require('path');
const config= require('./config.json');
const { Console } = require('console');
const router = express.Router();


var app = express();

// middleware ------------------------------------------------------------------>
app.use(express.static('public'));
app.use(bodyparser.json());
app.use(bodyparser.urlencoded({extended: true}));

// usage ----------------------------------------------------------------------->
router.get('/',function(req,res){
      res.sendFile(path.join(__dirname, 'public', 'main.html'));
});

app.use('/', router);

app.listen(config.port, () =>{
      console.log("Running at: http://" + config.hostname + ':' + config.port);
});
                                                                              


