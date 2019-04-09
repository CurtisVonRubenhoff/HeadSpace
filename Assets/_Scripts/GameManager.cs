using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameManager: MonoBehaviour {

  public enum GameState {
    PAUSED,
    RUNNING,
  }
  public bool hasFlute;
  public bool hasWallet;
  public bool hasPhone;
  public bool hasMap;

  public GameState currentState;

  [SerializeField]
  private GameObject PauseMenu;

  [SerializeField]
  private Image fade;



  public static GameManager instance;


  private void Awake() {
    Cursor.visible = false;
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(gameObject);

    StartCoroutine(FadeIn());
  }

  private IEnumerator FadeIn()
  {
    var start = fade.color.a;

    while (start > 0.1f) {
      start -= Time.deltaTime;
      fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, start);
      yield return null;
    }
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Cursor.visible = !PauseMenu.activeSelf;
      PauseMenu.SetActive(!PauseMenu.activeSelf);

      currentState = (PauseMenu.activeSelf) ? GameState.PAUSED : GameState.RUNNING;
    }
  }
}
