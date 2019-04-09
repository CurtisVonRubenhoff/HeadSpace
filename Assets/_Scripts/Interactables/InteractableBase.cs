using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Motion;

public enum InteractableState {
  STANDBY,
  USING,
}

public class InteractableBase : MonoBehaviour {

  [SerializeField]
  protected bool canDo;
  [SerializeField]
  protected string CharName;
  [SerializeField]
  protected bool dontHidePlayer;
  protected GameObject playerObj;
  [SerializeField]
  protected SmoothFollow cameraSmooth;
  [SerializeField]
  protected Transform FocusPoint;
  protected InteractableState myState = InteractableState.STANDBY;

  protected virtual void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Player") {
      canDo = true;
      TextMaster.IndicatorOn(CharName);
      playerObj = col.gameObject;
    }
  }

  protected virtual void OnTriggerExit(Collider col) {
    if (col.gameObject.tag == "Player") {
      canDo = false;
      TextMaster.ClearText();
      TextMaster.IndicatorOff();
    }
  }
  protected virtual void StartDoingThing() {
    TextMaster.IndicatorOn("Back");
    myState = InteractableState.USING;

    if (FocusPoint) {
      cameraSmooth.target = FocusPoint;
      if (!dontHidePlayer) playerObj.SetActive(false);
    }
  }

  protected virtual void StopDoingThing() {
    myState = InteractableState.STANDBY;
    TextMaster.IndicatorOn(CharName);

    if (FocusPoint) {
      cameraSmooth.target = cameraSmooth.gameObject.transform.parent;
      playerObj.SetActive(true);
    }
  }
}
