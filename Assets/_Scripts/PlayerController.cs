using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    Animator PlayerAnim;
    [SerializeField]
    Rigidbody PlayerRigid;
    [SerializeField]
    List<AudioSource> FootstepSounds = new List<AudioSource>();

    public int turnSpeed;
    [SerializeField]
    private bool PS_canMove = true;
    private bool PS_CanUse = false;

    // Update is called once per frame
    void Update() {
      var inputx = Input.GetAxis("Horizontal");
      var inputy = Input.GetAxis("Vertical");
      var mouseX = Input.GetAxis("Mouse X");
      var mousey = Input.GetAxis("Mouse Y");

      if (GameManager.instance.currentState == GameManager.GameState.PAUSED) {
        inputx = inputy = mouseX = mousey = 0f;
      }
      
      if (PS_canMove) {
        var shift = Input.GetKey(KeyCode.LeftShift);
        if (Mathf.Abs(inputx) > 0) {
          transform.Rotate(Vector3.up * Time.deltaTime * inputx * turnSpeed);
        }
        if (inputy > 0) {
          PlayerAnim.SetInteger("State", (shift)? 2 : 1);
        }
        if (inputy == 0) {
          PlayerAnim.SetInteger("State", 0);
        }
      }
    }
    public void Step() {
      FootstepSounds[Random.Range(0, FootstepSounds.Count-1)].Play();
    }
}
