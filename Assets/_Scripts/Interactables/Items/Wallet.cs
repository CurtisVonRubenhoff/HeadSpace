using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : PickUpItem {
  protected override void StartTalking() {
    base.StartTalking();
    GameManager.instance.hasWallet = true;
  }

  protected override void StopTalking() {
    base.StopTalking();
    if (GameManager.instance.hasWallet) {
      Destroy(this);
      TextMaster.ClearText();
      TextMaster.IndicatorOff();
    } 
  }
}
