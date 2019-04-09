using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChattyNpc: TalkToMe {

  [SerializeField]
  protected List<Message> AcceptMessages;
  [SerializeField]
  protected List<Message> DenyMessages;

  [SerializeField]
  protected Animator myAnim;

  protected override void StartTalking() {
    base.StartTalking();
    var message = messages[currentMessage];

    if (message.isPrompt) {
      AddChoiceListeners();
    }

    myAnim.SetBool("talking", true);
  }

  protected override void StopTalking() {
    RemoveChoiceListeners();

    base.StopTalking();
    myAnim.SetBool("talking", false);
  }

  protected void AddChoiceListeners() {
    TextMaster tm = TextMaster.instance;

    tm.AcceptButton.onClick.AddListener(AcceptChoice);
    tm.DenyButton.onClick.AddListener(DenyChoice);
  }

  protected void RemoveChoiceListeners() {
    TextMaster tm = TextMaster.instance;

    tm.AcceptButton.onClick.RemoveListener(AcceptChoice);
    tm.DenyButton.onClick.RemoveListener(DenyChoice);
  }


  public virtual void AcceptChoice() {
    // Implement in child class
  }

  public virtual void DenyChoice() {
    // Implement in child class
  }

}
