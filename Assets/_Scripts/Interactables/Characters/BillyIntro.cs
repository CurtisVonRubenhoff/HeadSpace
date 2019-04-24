using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyIntro : TalkToMe {


  [SerializeField]
  private Animator myAnim;
  public Billy realBill;

  protected override void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Player") {
      playerObj = col.gameObject;
      StartTalking();
      myAnim.SetTrigger("Yell");
    }
  }

  protected override void StopTalking() {
    base.StopTalking();
    TextMaster.IndicatorOff();
    realBill.billState = BillyState.INTRO;
    Destroy(this);
  }
}
