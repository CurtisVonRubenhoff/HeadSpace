using UnityEngine;

public class Flute: PickUpItem {

  protected override void StartTalking() {
    base.StartTalking();
    GameManager.instance.hasFlute = true;
  }

  protected override void StopTalking() {
    base.StopTalking();
    if (GameManager.instance.hasFlute) {
      Destroy(this);
      TextMaster.ClearText();
      TextMaster.IndicatorOff();
    } 
  }
}
