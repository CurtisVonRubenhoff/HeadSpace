using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkToMe : InteractableBase {

  [SerializeField]
  protected List<Message> messages = new List<Message>();
  [SerializeField]
  protected AudioSource audioSource;
  protected int currentMessage = 0;

  [SerializeField]
  protected TextMeshPro myTMP;

	// Update is called once per frame
	protected virtual void Update () {
    if (GameManager.instance.currentState == GameManager.GameState.RUNNING) {
      if (Input.GetKeyDown(KeyCode.E)) {
        if (canDo) {
          if (myState == InteractableState.STANDBY) {
            canDo = false;
            StartTalking();
          }         
        } else if (myState == InteractableState.USING) {
          if (!isDone) {
            if (IsLastMessage()) {
              canDo = true;
              if (myTMP == null) TextMaster.ClearText();
              StopTalking();
            } else {
              ContinueSpeaking();
            }
          }
        }
      }
    }
	}

  protected virtual void StartTalking() {
    StartDoingThing();
    var message = messages[currentMessage];

    if (IsLastMessage()) {
      TextMaster.IndicatorOn("Back");
    } else {
      TextMaster.IndicatorOn("Continue");
    }

    ProcessMessage(message);
  }

  protected virtual void StopTalking() {
    Debug.Log($"I'm doing something {gameObject.name}");
    StopDoingThing();
    TextMaster.DisableChoices();
    currentMessage = 0;
    StopAllCoroutines();
  }

  protected override void OnTriggerExit(Collider col) {
    StopTalking();
    base.OnTriggerExit(col);
  }

  protected void ContinueSpeaking() {
    currentMessage++;
    StartTalking();
  } 

  protected void ProcessMessage(Message mess) {
    if (myTMP != null) TextMaster.ShowText(mess, myTMP);
    else TextMaster.ShowText(mess);

    if (mess.isPrompt) {
      TextMaster.EnableChoices();
    }

    if (audioSource != null) {
      audioSource.clip = mess.audio;
      audioSource.Play();
    }
  }

  protected bool IsLastMessage() {
    return (currentMessage == (messages.Count - 1));
  }
}
