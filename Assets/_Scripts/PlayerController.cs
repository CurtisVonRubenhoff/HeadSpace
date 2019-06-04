using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    Animator PlayerAnim;
    [SerializeField]
    List<AudioSource> FootstepSounds = new List<AudioSource>();

    [SerializeField]
    MusicController maestro;


    public int turnSpeed;
    [SerializeField]
    private bool PS_canMove = true;
    private bool PS_CanUse = false;
    private bool PS_OnBridge = false;

    // Update is called once per frame
    void Update() {
      var inputx = Input.GetAxis("Horizontal");
      var inputy = Input.GetAxis("Vertical");
      var mouseX = Input.GetAxis("Mouse X");
      var mousey = Input.GetAxis("Mouse Y");

      Debug.Log(inputx);
      Debug.Log(inputy);

      if (GameManager.instance.currentState == GameManager.GameState.PAUSED) {
        inputx = inputy = mouseX = mousey = 0f;
      }
      
      if (PS_canMove) {
        var shift = (PS_OnBridge) ? false : Input.GetButton("Run");

        if (Mathf.Abs(inputx) > 0) {
          transform.Rotate(Vector3.up * Time.deltaTime * inputx * turnSpeed);
        }
        if (inputy > 0) {
          PlayerAnim.SetInteger("State", (shift)? 2 : 1);
        }
        if (inputy == 0) {
          PlayerAnim.SetInteger("State", 0);
        }

        maestro.UpdateWalkingMusic(inputy, (!PS_OnBridge && Input.GetButton("Run")));
      } else {
        maestro.UpdateWalkingMusic(0.0f, false);
      }
    }
    public void Step() {
      FootstepSounds[Random.Range(0, FootstepSounds.Count-1)].Play();
    }

    private void OnTriggerStay(Collider col) {
      if (col.gameObject.tag == "Stairs") {
        PS_OnBridge = true;
      }
    }

    private void OnTriggerExit(Collider col) {
      if (col.gameObject.tag == "Stairs") {
        PS_OnBridge = false;
      }
    }

    private void OnDisable() {
      maestro.UpdateWalkingMusic(0.0f, false);
    }
    
}
