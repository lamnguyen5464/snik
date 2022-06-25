const { roomManager, Room } = require("../models/room");
const { action, ActionType } = require("../models/action");

class ClientManager {
  static instance = null;
  static getInstance() {
    if (ClientManager.instance === null) {
      ClientManager.instance = new ClientManager();
    }
    return ClientManager.instance;
  }

  id = 0;
  connections = {};

  addConnections(client) {
    const id = this.generateId();
    client.id = id;
    this.connections[id] = client;

    console.info("[info] add new connection with id: ", id);
    client.on("message", (payload) => this.handleRequest(client, payload));
    // client.on("close", () => this.removeConnection(id));
  }

  removeConnection(id) {
    console.info("[info] remove connection with id: ", id);
    delete this.connections[id];
  }

  sendTo(clientIds = [], data) {
    const message = typeof data === "string" ? data : JSON.stringify(data);
    clientIds.forEach((clientId) => this.connections[clientId].send(message));
  }

  generateId() {
    this.id = (this.id ?? 0) + 1;
    return `${this.id}-${new Date().getTime()}`;
  }

  handleRequest(client, payload) {
    const { payload: rawData, action: _action } = JSON.parse(payload);
    const data = JSON.parse(rawData);
    console.log(`[receive] data from ${client.id}: ${payload}`);

    // setInterval(() => {
    //   const _payload = JSON.stringify({
    //     varA: "varAAA",
    //     varB: 111,
    //   });
    //   const dataToSend = JSON.stringify({
    //     action: "TestModel",
    //     payload: _payload,
    //   });
    //   console.info("start send", dataToSend);
    //   client.send(dataToSend);
    // }, 1000);

    switch (_action) {
      case ActionType.CREATE_ROOM: {
        const { roomOwnerName } = data || {};
        const newRoom = new Room(roomOwnerName);
        newRoom.socketids.push(client.id);
        roomManager.addRoom(newRoom);
        client.send(action.init(ActionType.CREATE_ROOM, newRoom.id));
        break;
      }
      case ActionType.FIND_ROOM: {
        const { roomId } = data || {};
        const foundedRoom = roomManager.findRoom(roomId);
        client.send(action.init(ActionType.FIND_ROOM, foundedRoom));
        break;
      }
      case ActionType.JOIN_ROOM: {
        const { roomId, userName } = data || {};
        const foundedRoom = roomManager.findRoom(roomId);
        if (founded.ownerName === userName || founded.userName !== null) {
          client.send(action.init(ActionType.FIND_ROOM, "Cannot join room"));
        }
        foundedRoom.socketids.push(client.id);
        foundedRoom.joinRoom(userName);
        this.sendTo(
          foundedRoom.socketids,
          action.init(ActionType.USER_JOINED, userName + "has joined!")
        );
        break;
      }

      case ActionType.ON_MOVE: {
        const { roomId, name, x, y } = data;
        const foundedRoom = roomManager.findRoom(roomId);
        if (foundedRoom.ownerName === name) {
          foundedRoom.A["x"] = x;
          foundedRoom.A["y"] = y;
        } else {
          foundedRoom.B["x"] = x;
          foundedRoom.B["y"] = y;
        }
        break;
      }

      case ActionType.START_GAME: {
        const { roomId } = data;
        setInterval(() => {
          const foundedRoom = roomManager.findRoom(roomId);
          this.sendTo(
            foundedRoom.socketids,
            action.init(ActionType.NEW_COORDINATE, foundedRoom)
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
  }
}

module.exports = ClientManager.getInstance();
