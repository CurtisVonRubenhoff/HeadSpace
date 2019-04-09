using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StrangerState {
    AGONY,
    REMEMBER,
    WALKING,
    MONSTERS,
  }

public class Stranger: ChattyNpc {


  [SerializeField]
  protected List<StrangerMessageLists> serializedLists; 
  protected Dictionary<StrangerState, List<Message>> MessageLists = new Dictionary<StrangerState, List<Message>>();

  [SerializeField]
  protected StrangerState strangeState;

  public bool playerRemembered;

  protected void Awake() {
    foreach (var list in serializedLists) {
      MessageLists.Add(list.state, list.messages);
    }
  }

  protected override void Update() {
    base.Update();

    if (strangeState == StrangerState.AGONY && GameManager.instance.hasFlute) {
      strangeState = StrangerState.REMEMBER;
      myAnim.SetBool("fluteFound", true);
    }

    messages = MessageLists[strangeState];
  }

  private void Remember(bool didHe) {
    RemoveChoiceListeners();
    TextMaster.DisableChoices();
    var message = (didHe) ? AcceptMessages[0] : DenyMessages[0];

    ProcessMessage(message);

    currentMessage = -1;
    strangeState = StrangerState.MONSTERS;
    playerRemembered = didHe;
  }

  public override void AcceptChoice() {
    StopAllCoroutines();

    switch (strangeState) {
      case StrangerState.REMEMBER:
        Remember(true);
        break;
      default:
        break;
    }
  }

  public override void DenyChoice() {
    StopAllCoroutines();

    switch (strangeState) {
      case StrangerState.REMEMBER:
        Remember(false);
        break;
      default:
        break;
    }
  }
}
