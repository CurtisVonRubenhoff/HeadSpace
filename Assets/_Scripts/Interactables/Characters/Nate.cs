using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NateState {
  INTRO,
  WAITING,
  COMPLETE
}

public class Nate : ChattyNpc {

  [SerializeField]
  public List<NateMessageLists> serializedLists;
  protected Dictionary<NateState, List<Message>> MessageLists = new Dictionary<NateState, List<Message>>();

  protected NateState nateState;
  [SerializeField]

  protected void Awake() {
    foreach (var list in serializedLists) {
      MessageLists.Add(list.state, list.messages);
    }
  }

  protected override void Update() {
    base.Update();

    if (nateState == NateState.WAITING &&
      GameManager.instance.monstersPlayerEncountered > 0) {
      nateState = NateState.COMPLETE;
    }

    messages = MessageLists[nateState];
  }

  private void AcceptQuest(bool didHe) {
    RemoveChoiceListeners();
    TextMaster.DisableChoices();
    var message = (didHe) ? AcceptMessages[0] : DenyMessages[0];

    ProcessMessage(message);

    currentMessage = 0;
    if (didHe) {
      nateState = NateState.WAITING;
    } else {
      nateState = NateState.INTRO;
    }
  }

  public override void AcceptChoice() {
    switch(nateState) {
      case NateState.INTRO:
        AcceptQuest(true);
        break;
      default:
        break;
    }
  }

  public override void DenyChoice() {
    switch(nateState) {
      case NateState.INTRO:
        AcceptQuest(false);
        break;
      default:
        break;
    }
  }

    protected override void StopTalking() {
    if (nateState == NateState.COMPLETE) {
      var gm = GameManager.instance;
      TextMaster.ClearText(myTMP);
      TextMaster.IndicatorOff();
      if (!gm.hasMoney) gm.hasMoney = true;
      isDone = true;
      base.StopTalking();
    } else {
      base.StopTalking();
    }
  }
}
