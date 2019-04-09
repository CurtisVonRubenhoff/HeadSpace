using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BillyState {
  INTRO,
  BEG,
  WALKING,
  OTHERBEG,
}

public class Billy : ChattyNpc {

  [SerializeField]
  public List<BilyMessageLists> serializedLists;
  protected Dictionary<BillyState, List<Message>> MessageLists = new Dictionary<BillyState, List<Message>>();

  protected BillyState billState;
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
    }
  }

  public override void DenyChoice() {
    switch(billState) {
      default:
        break;
    }
  }
}
