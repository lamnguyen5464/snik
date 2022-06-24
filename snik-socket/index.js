const WebSocket = require("ws");
const Room = require("./models/room");
const {Action, ActionType} = require("./models/action");

const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log("Server started");
});

const rooms = [];
let intervalControl;
wss.broadcast = function broadcast(msg, receivers) {
    wss.clients.filter(client => receivers.includes(client.id)).forEach(function each(client) {
        client.send(msg);
    });
};
wss.getUniqueID = function () {
  function s4() {
      return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
  }
  return s4() + s4() + '-' + s4();
};
wss.on("connection", (ws) => {
  ws.id = wss.getUniqueID();
  // console.log(wss.clients);
  ws.on("message", (payload) => {
    console.log(payload);
    const { data, type } = JSON.parse(payload);
    switch (type) {
      case CREATE_ROOM: {
        const { roomOwnerName } = data;
        const newRoom = new Room(roomOwnerName);
        rooms.push(newRoom);
        newRoom.socketids.push(ws.id);
        ws.send(new Action(newRoom.id, ActionType.CREATE_ROOM));
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
        foundedRoom.socketids.push(ws.id)
        foundedRoom.joinRoom(userName);
        wss.broadcast(new Action(userName+ "has joined!" , ActionType.USER_JOINED), foundedRoom.socketids);
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
            wss.broadcast(new Action(foundedRoom, ActionType.NEW_COORDINATE), foundedRoom.socketids);
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
