class ActionType {
  static USER_JOINED = "USER_JOINED";
  static FIND_ROOM = "FIND_ROOM";
  static CREATE_ROOM = "CREATE_ROOM";
  static NEW_COORDINATE = "NEW_COORDINATE";
}
class Action {
  constructor(msg, type) {
    this.type = type;
    this.msg = msg;
  }
}
module.exports = { ActionType, Action };
