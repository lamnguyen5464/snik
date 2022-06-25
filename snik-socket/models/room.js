function generateID() {
  return Math.floor(100000 + Math.random() * 900000).toString();
}
class Room {
  constructor(ownerName) {
    this.ownerName = ownerName;
    this.id = generateID();
    this.userName = null;
    this.clientIds = [];
    this.A = {};
    this.B = {};
  }
  joinRoom(userName) {
    this.userName = userName;
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
  defaultRoom = new Room();

  addRoom(room) {
    this.rooms.push(room);
  }

  findRoom(roomId) {
    return this.rooms?.find((room) => room.id === roomId) ?? this.defaultRoom;
  }
}

const roomManager = RoomManager.getInstance();

module.exports = { roomManager, Room };
