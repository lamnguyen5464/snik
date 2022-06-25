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
    console.info("[send] : ", data);
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
        const newRoom = roomManager.createRoom(roomOwnerName);
        roomManager.addClientToRoom(client.id, roomOwnerName, {}, newRoom.id);
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
        // if (founded.ownerName === userName || founded.userName !== null) {
        //   client.send(action.init(ActionType.FIND_ROOM, "Cannot join room"));
        // }
        // foundedRoom.clientIds.push(client.id);
        // foundedRoom.joinRoom(userName);
        roomManager.addClientToRoom(client.id, userName, {}, newRoom.id);
        this.sendTo(
          foundedRoom.clientIds,
          action.init(ActionType.USER_JOINED, userName + "has joined!")
        );
        break;
      }

      case ActionType.NEW_COORDINATE: {
        const { roomId, nickName = "", x, y } = data || {};
        const foundedRoom = roomManager.findRoom(roomId);

        foundedRoom.userData?.forEach((item, index) => {
          if (item.clientId === client.id) {
            console.warn(`[modify] of ${client.id}`, { x, y });
            foundedRoom.userData[index].position = { x, y };
          }
        });

        this.sendTo(
          foundedRoom.getClientIds(),
          action.init(ActionType.ON_MOVE, { items: foundedRoom.userData })
        );

        break;
      }

      case ActionType.START_GAME: {
        const { roomId, nickName = "" } = data;
        const foundedRoom = roomManager.findRoom(roomId);

        if (!foundedRoom.containClient(client.id)) {
          roomManager.addClientToRoom(client.id, nickName, foundedRoom.id);
        }

        client.send(
          JSON.stringify({
            action: ActionType.START_GAME,
            payload: JSON.stringify({
              clientId: client.id,
              clientIds: foundedRoom.userData?.map((item) => item.clientId),
            }),
          })
        );

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
