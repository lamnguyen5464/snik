function generateID() {
  return Math.floor(100000 + Math.random() * 900000).toString();
}
class Room {
  constructor(ownerName) {
    this.ownerName = ownerName;
    this.id = generateID();
    this.userData = []; // { id, clientId, nickName, position: {x, y}
  }

  addClient(clientId, nickName, position = {}) {
    this.userData.push({
      clientId,
      nickName,
      position,
      id: this.userData.length,
    });
  }

  getClientIds() {
    return this.userData?.map((item) => item.clientId);
  }

  containClient(clientId) {
    return this.getClientIds().includes(clientId);
  }
}

class RoomManager {
  static instance = null;
  static getInstance() {
    if (RoomManager.instance == null) {
      RoomManager.instance = new RoomManager();
    }
    return RoomManager.instance;
  }

  rooms = [];
  userIdToRoomId = {};
  defaultRoom = new Room("user_01");

  addRoom(room) {
    this.rooms.push(room);
  }

  createRoom(ownerName) {
    const room = new Room(ownerName);
    this.rooms.push(room);
    return room;
  }

  addClientToRoom(clientId, nickName, roomId) {
    this.userIdToRoomId[clientId] = roomId;
    const room = this.findRoom(roomId);

    room.addClient(clientId, nickName);
  }

  findRoom(roomId) {
    const index = this.rooms?.findIndex((room) => room.id === roomId);
    return index !== -1 ? this.rooms[index] : this.defaultRoom;
  }
}

const roomManager = RoomManager.getInstance();

module.exports = { roomManager, Room };
