using UnityEngine;

public class PickUpItem: TalkToMe {
  [SerializeField]
  ParticleSystem particles;
  [SerializeField]
  AudioSource sound;
  [SerializeField]
  Light light;
  protected override void StartTalking() {
    base.StartTalking();

    if (particles != null) particles.Stop();
    if (sound != null) sound.Stop();
    if (light != null) light.enabled = false;
  }

  protected override void StopTalking() {
    base.StopTalking();
    TextMaster.IndicatorOff();
  }
}
