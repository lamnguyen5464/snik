const WebSocket = require("ws");
const clientManager = require("./client");

class SocketManager {
  static instance = null;
  static getInstance() {
    if (SocketManager.instance === null) {
      SocketManager.instance = new SocketManager();
    }
    return SocketManager.instance;
  }

  id = 0;

  start() {
    const webSocket = new WebSocket.Server({ port: 8080 }, () => {
      console.log("Socket server started");
    });

    webSocket.on("connection", this.onConnection);
  }

  onConnection(client) {
    clientManager.addConnections(client);
  }
}

module.exports = SocketManager.getInstance();
