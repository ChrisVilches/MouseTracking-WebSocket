/*
*
* Codigo extraido desde https://www.npmjs.com/package/websocket
* (modificado)
*
*/

var WebSocketClient = require('websocket').client;

var client = new WebSocketClient();

var port = 9876;

client.on('connectFailed', function(error) {
    console.log('Connect Error: ' + error.toString());
});

client.on('connect', function(connection) {
    console.log('WebSocket Client Connected');
    connection.on('error', function(error) {
        console.log("Connection Error: " + error.toString());
    });
    connection.on('close', function() {
        console.log('Connection Closed');
    });
    connection.on('message', function(message) {
        if (message.type === 'utf8') {
            console.log("Received: '" + message.utf8Data + "'");
        }
    });
});

client.connect('ws://localhost:' + port + '/mousetracking');
