const WebSocket = require("ws");
const Room = require("./models/room");
const {Action, ActionType} = require("./models/action");

const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log("Server started");
});

const rooms = [];
let intervalControl;
wss.broadcast = function broadcast(msg) {
    wss.clients.forEach(function each(client) {
        client.send(msg);
     });
};
wss.on("connection", (ws) => {
  ws.on("message", (payload) => {
    const { data, type } = JSON.parse(payload);
    switch (type) {
      case CREATE_ROOM: {
        const { roomOwnerName } = data;
        const newRoom = new Room(roomOwnerName);
        rooms.push(newRoom);
        ws.send(new Action("Create room successfully", ActionType.CREATE_ROOM));
        break;
      }
      case FIND_ROOM: {
        const { roomId } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        ws.send(new Action(foundedRoom, ActionType.FIND_ROOM));
        break;
      }
      case JOIN_ROOM: {
        const { roomId, userName } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        if(founded.ownerName === userName || founded.userName !== null) {
            ws.send(new Action("Cannot join room", ActionType.FIND_ROOM));
        }
        foundedRoom.joinRoom(userName);
        wss.broadcast(new Action(userName+ "has joined!" , ActionType.USER_JOINED));
        break;
      }

      case ON_MOVE: {
        const { roomId, name, x, y } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        if(foundedRoom.ownerName === name) {
            foundedRoom.A['x'] = x;
            foundedRoom.A['y'] = y;
        } else {
            foundedRoom.B['x'] = x;
            foundedRoom.B['y'] = y;
        }
        break;
      }
      case START: {
        const { roomId } = data;
        intervalControl = setInterval(() => {
            const foundedRoom = rooms.findOne((room) => room.id === roomId);
            wss.broadcast(new Action(foundedRoom, ActionType.NEW_COORDINATE));
        }, 30);
      }
      case STOP: {
        intervalControl.clearInterval();
      }
    }
  });
});

wss.on("listening", (ws) => {
  console.log("Server is listening on port 8080");
});
