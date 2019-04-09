using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LexState {
  INTRO,
}

public class Lex : ChattyNpc {

  [SerializeField]
  public List<LexMessageLists> serializedLists;
  protected Dictionary<LexState, List<Message>> MessageLists = new Dictionary<LexState, List<Message>>();

  protected LexState lexState;
  [SerializeField]

  protected void Awake() {
    foreach (var list in serializedLists) {
      MessageLists.Add(list.state, list.messages);
    }
  }

  protected override void Update() {
    base.Update();

    messages = MessageLists[lexState];
  }

  public override void AcceptChoice() {
    switch(lexState) {
      default:
        break;
    }
  }

  public override void DenyChoice() {
    switch(lexState) {
      default:
        break;
    }
  }
}
