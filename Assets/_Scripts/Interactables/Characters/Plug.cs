using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlugState {
  INTRO,
}

public class Plug : ChattyNpc {

  [SerializeField]
  public List<PlugMessageLists> serializedLists;
  protected Dictionary<PlugState, List<Message>> MessageLists = new Dictionary<PlugState, List<Message>>();

  protected PlugState plugState;
  [SerializeField]

  protected void Awake() {
    foreach (var list in serializedLists) {
      MessageLists.Add(list.state, list.messages);
    }
  }

  protected override void Update() {
    base.Update();

    messages = MessageLists[plugState];
  }

  public override void AcceptChoice() {
    switch(plugState) {
      default:
        break;
    }
  }

  public override void DenyChoice() {
    switch(plugState) {
      default:
        break;
    }
  }

  protected override void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Player") {
      if (plugState == PlugState.INTRO) {
        playerObj = col.gameObject;
        StartTalking();
      }
    }
  }
}
