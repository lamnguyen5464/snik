function generateID() {
    return (Math.floor(100000 + Math.random() * 900000)).toString();
}

module.exports = class Room {
    constructor(ownerName) {
        this.ownerName = ownerName;
        this.id = generateID();
        this.userName = null;
        this.socketids = [];
        this.A = {};
        this.B = {};
    }
    joinRoom(userName) {
        this.userName = userName;
    }
}
