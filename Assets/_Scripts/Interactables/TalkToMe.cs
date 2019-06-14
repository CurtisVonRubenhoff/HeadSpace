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

  [SerializeField]
  protected MusicController maestro;

	// Update is called once per frame
	protected virtual void Update () {
    if (GameManager.instance.currentState == GameManager.GameState.RUNNING) {
      if (Input.GetButtonDown("Interact")) {
        if (canDo) {
          if (myState == InteractableState.STANDBY) {
            canDo = false;
            StartTalking();
            if (maestro != null) maestro.PlayMySong(CharName);
          }         
        } else if (myState == InteractableState.USING) {
          if (!isDone) {
            if (IsLastMessage()) {
              canDo = true;
              if (myTMP == null) TextMaster.ClearText();
              if (maestro != null) maestro.StopCharacterSongs();
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
    if(col.gameObject.tag == "Player") {
      StopTalking();
      base.OnTriggerExit(col);
      TextMaster.IndicatorOff();
    }
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
