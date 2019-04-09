using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : PickUpItem {
  protected override void StartTalking() {
    base.StartTalking();
    GameManager.instance.hasMap = true;
  }

  protected override void StopTalking() {
    base.StopTalking();
    if (GameManager.instance.hasMap) {
      Destroy(this);
      TextMaster.ClearText();
      TextMaster.IndicatorOff();
    } 
  }
}
