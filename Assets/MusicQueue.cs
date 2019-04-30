using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicQueue : MonoBehaviour
{
  [SerializeField]
  private MusicController maestro;

  private void OnTriggerEnter(Collider col)
  {
    if (col.tag == "MainCamera") {
      maestro.GetIntense();
    }
  }

  private void OnTriggerExit(Collider col)
  {
    if (col.tag == "MainCamera") {
      maestro.EaseUp();
    }
  }
}
