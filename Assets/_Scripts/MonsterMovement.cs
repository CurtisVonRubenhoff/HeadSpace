using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

  private float DEFAULT_SCENE_LIGHT_LUX = 4.0f;

  public Transform target;
  public UnityEngine.AI.NavMeshAgent agent;

  [SerializeField]
  AudioSource FootStepSound;

  public Transform[] points;
  private int destPoint = 0;

  [SerializeField]
  Light SceneLight;

  private bool playerNearby = false;
  private Transform playerTrans;

  [SerializeField]
  private SphereCollider myCollider;
    

  // Use this for initialization
  void Start () {
    GotoNextPoint();
  }
	
	// Update is called once per frame
	void Update () {
    if (GameManager.instance.currentState == GameManager.GameState.RUNNING) {
      if (agent.remainingDistance < 0.5f)
        GotoNextPoint();

      if (playerNearby)
        CalculateLights();
    }
  }


  void GotoNextPoint() {
    // Returns if no points have been set up
    if (points.Length == 0)
        return;

    // Set the agent to go to the currently selected destination.
    agent.destination = points[destPoint].position;

    // Choose the next point in the array as the destination,
    // cycling to the start if necessary.
    destPoint = (destPoint + 1) % points.Length;
  }

  public void Step() {
    FootStepSound.Play();
  }

  private void CalculateLights() {
    var myPos = transform.position;
    var player = playerTrans.position;

    var distance = Vector3.Distance(myPos, player);

    // distance tends toward 0 as the player gets closer but we need the value
    // here to go toward 1 as the player gets closer. to accomplish this
    // i multiplied the value between 0 and 1 by -1 which means the greater the
    // distance the player is from this monster, the lower the value generated is.
    // then i offset the value so that it ranges from 0 to 1.  
    var normalizedValue = (distance / (myCollider.radius * 2)) * -1f + 1f;

    // i check that the value is greater than .001 because `1000 * .001 = 1`
    // and I don't actually want the lights lower than the default value.
    SceneLight.intensity = normalizedValue > 0.001f ?
      DEFAULT_SCENE_LIGHT_LUX * (1000f * normalizedValue) :
      DEFAULT_SCENE_LIGHT_LUX;
  }

  private void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Player") {
      playerNearby = true;
      playerTrans = col.gameObject.transform;
    }
  }

    private void OnTriggerExit(Collider col) {
    if (col.gameObject.tag == "Player") {
      playerNearby = false;
      SceneLight.intensity = DEFAULT_SCENE_LIGHT_LUX;
    }
  }
}
