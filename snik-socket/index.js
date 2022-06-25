const WebSocket = require("ws");
const Room = require("./models/room");
const { action, ActionType } = require("./models/action");

const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log("Server started");
});

const rooms = [];
let intervalControl;
wss.broadcast = function broadcast(msg, receivers) {
  wss.clients
    .filter((client) => receivers.includes(client.id))
    .forEach(function each(client) {
      client.send(msg);
    });
};
wss.getUniqueID = function () {
  function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  }
  return s4() + s4() + "-" + s4();
};
wss.on("connection", (ws) => {
  ws.id = wss.getUniqueID();
  ws.on("message", (payload) => {
    const { data, type } = JSON.parse(payload);
    console.log(data, type);
    switch (type) {
      case ActionType.CREATE_ROOM: {
        const { ownerName } = data;
        const newRoom = new Room(ownerName);
        rooms.push(newRoom);
        newRoom.socketids.push(ws.id);
        ws.send(action.init(ActionType.CREATE_ROOM, newRoom.id));
        break;
      }
      case ActionType.FIND_ROOM: {
        const { roomId } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        ws.send(action.init(ActionType.FIND_ROOM, foundedRoom));
        break;
      }
      case ActionType.JOIN_ROOM: {
        const { roomId, userName } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        if (founded.ownerName === userName || founded.userName !== null) {
          ws.send(action.init(ActionType.FIND_ROOM, "Cannot join room"));
        }
        foundedRoom.socketids.push(ws.id);
        foundedRoom.joinRoom(userName);
        wss.broadcast(
          action.init(ActionType.USER_JOINED, userName + "has joined!"),
          foundedRoom.socketids
        );
        break;
      }

      case ActionType.ON_MOVE: {
        const { roomId, name, x, y } = data;
        const foundedRoom = rooms.findOne((room) => room.id === roomId);
        if (foundedRoom.ownerName === name) {
          foundedRoom.A["x"] = x;
          foundedRoom.A["y"] = y;
        } else {
          foundedRoom.B["x"] = x;
          foundedRoom.B["y"] = y;
        }
        break;
      }

      case ActionType.START: {
        const { roomId } = data;
        intervalControl = setInterval(() => {
          const foundedRoom = rooms.findOne((room) => room.id === roomId);
          wss.broadcast(
            action.init(ActionType.NEW_COORDINATE, foundedRoom),
            foundedRoom.socketids
          );
        }, 30);
        break;
      }

      case ActionType.STOP: {
        intervalControl.clearInterval();
        break;
      }
      default:
        return;
    }
  });
});

wss.on("listening", (ws) => {
  console.log("Server is listening on port 8080");
});
