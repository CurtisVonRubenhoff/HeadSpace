using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : PickUpItem {
  protected override void StartTalking() {
    base.StartTalking();
    GameManager.instance.hasPhone = true;
  }

  protected override void StopTalking() {
    base.StopTalking();
    if (GameManager.instance.hasPhone) {
      Destroy(this);
      TextMaster.ClearText();
      TextMaster.IndicatorOff();
    } 
  }
}
