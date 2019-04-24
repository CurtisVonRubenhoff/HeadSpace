using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BillyState {
  PREINTRO,
  INTRO,
  BEG,
  WALKING,
  OTHERBEG,
  RICH,
}

public class Billy : ChattyNpc {

  [SerializeField]
  public List<BilyMessageLists> serializedLists;
  protected Dictionary<BillyState, List<Message>> MessageLists = new Dictionary<BillyState, List<Message>>();

  public BillyState billState;
  [SerializeField]

  protected void Awake() {
    foreach (var list in serializedLists) {
      MessageLists.Add(list.state, list.messages);
    }
  }

  protected override void Update() {
    base.Update();

    if (billState == BillyState.INTRO && GameManager.instance.hasWallet) {
      billState = BillyState.BEG;
    }

    messages = MessageLists[billState];
  }

  public override void AcceptChoice() {
    switch(billState) {
      default:
        break;
      case BillyState.BEG:
        Donate(true);
        break;
    }
  }

  public override void DenyChoice() {
    switch(billState) {
      default:
        break;
      case BillyState.BEG:
        Donate(false);
        break;
    }
  }

  protected override void StartTalking() {
    if (billState == BillyState.RICH) {
      canDo = false;
      TextMaster.ClearText(myTMP);
      TextMaster.IndicatorOff();
      StopTalking();
    } else {
      base.StartTalking();
    }
  }

  private void Donate(bool didHe) {
    RemoveChoiceListeners();
    TextMaster.DisableChoices();
    var message = (didHe) ? AcceptMessages[0] : DenyMessages[0];

    ProcessMessage(message);

    currentMessage = -1;
    if (didHe) {
      billState = BillyState.RICH;
      isDone = true;
    } else {
      billState = BillyState.BEG;
    }
  }

}
