using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager: MonoBehaviour {

  public enum GameState {
    PAUSED,
    RUNNING,
  }
  public bool hasFlute;
  public bool hasWallet = true;
  public bool hasMoney = false;
  public bool hasPhone;
  public bool hasMap;
  public int currentDay = 1;

  public GameState currentState;

  [SerializeField]
  private Image fade;
  [SerializeField]
  private TextMeshProUGUI DayText;

  public static GameManager instance;

  public int monstersPlayerEncountered = 0;

  private static List<GameObject> monstersInRange;


  private void Awake() {
    currentDay = PlayerPrefs.GetInt("CurrentDay", 1);
    Cursor.visible = false;
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(gameObject);

    StartCoroutine(ShowDayAndFadeIn());
  }

  private IEnumerator ShowDayAndFadeIn()
  {
    var start = fade.color.a;

    DayText.gameObject.SetActive(true);
    DayText.text = $"Day {currentDay}";
    yield return new WaitForSeconds(1.5f);
    DayText.gameObject.SetActive(false);

    while (start > 0.1f) {
      start -= Time.deltaTime;
      fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, start);
      yield return null;
    }
  }

  public void GoToSleep() {
    currentDay++;
    PlayerPrefs.SetInt("CurrentDay", currentDay);

    StartCoroutine(FadeOut());
  }

  private IEnumerator FadeOut()
  {
    var start = fade.color.a;
    
    while (start < 1.0f) {
      start += Time.deltaTime;
      fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, start);
      yield return null;
    }

    SceneManager.LoadSceneAsync(0);
  }

  private void Update() {
    if (Input.GetButtonDown("Menu")) {
      currentState = (currentState == GameState.RUNNING) ? GameState.PAUSED : GameState.RUNNING;
    }
  }

  public void PlayerApproachesMonster() {
    monstersPlayerEncountered++;
  }
}
