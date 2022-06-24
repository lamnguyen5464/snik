class ActionType {
  static USER_JOINED = "USER_JOINED";
  static FIND_ROOM = "FIND_ROOM";
  static JOIN_ROOM = "JOIN_ROOM";
  static CREATE_ROOM = "CREATE_ROOM";
  static NEW_COORDINATE = "NEW_COORDINATE";
}
class Action {
  constructor(data, type) {
    this.type = type;
    this.data = data;
  }
  init(type, data) {
    this.type = type;
    this.data = data;
    return JSON.stringify(this);
  }
}
const action = new Action();
module.exports = { ActionType, action };
