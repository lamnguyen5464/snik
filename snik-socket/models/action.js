class ActionType {
  static JOIN_ROOM = "JOIN_ROOM";
  static FIND_ROOM = "FIND_ROOM";
  static CREATE_ROOM = "CREATE_ROOM";
  static NEW_COORDINATE = "NEW_COORDINATE";
  static ON_MOVE = "ON_MOVE";
  static START_GAME = "START_GAME";
  static READY = "READY";
  static RESET_GAME_ROUND = "RESET_GAME_ROUND";
}
class Action {
  constructor(payload, action) {
    this.action = action;
    this.payload = payload;
  }
  init(action, payload) {
    this.action = action;
    this.payload = JSON.stringify(payload);
    return JSON.stringify(this);
  }
}
const action = new Action();
module.exports = { ActionType, action };
