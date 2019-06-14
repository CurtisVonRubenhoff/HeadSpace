using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
  private bool warningOn;
  private bool optionsOn;
  
  [SerializeField]
  private AudioSource StartSound;
  [SerializeField]
  private AudioSource SelectSound;
  [SerializeField]
  private AudioSource HumNoise;
  [SerializeField]
  private Image FadeImage;
  float FadeDuration = 10f;
  bool fadingIn = false;
  bool fadingOut = false;

  [SerializeField]
  GameObject Warning;
  [SerializeField]
  GameObject OptionsMenu;

  [SerializeField]
  Button StartGameButton;
  [SerializeField]
  Toggle DisclaimerButton;
  [SerializeField]
  Button QuitButton;

  [SerializeField]
  GameObject GameClearedNotification;
	
	// Update is called once per frame
	void Update () {
    if (fadingIn) {
      var cur = FadeImage.color;
      FadeImage.color = Color.Lerp(cur, new Color(cur.r, cur.g, cur.b, 1.3f), Time.deltaTime);
    }
    if (fadingOut) {
        var cur = FadeImage.color;
        FadeImage.color = Color.Lerp(cur, new Color(cur.r, cur.g, cur.b, 0), Time.deltaTime);
    }
  }

  public void StartGame() {
    StartGameButton.interactable = false;
    DisclaimerButton.interactable = false;
    QuitButton.interactable = false;
    StartSound.time = 1.5f;
    StartSound.Play();
    HumNoise.Stop();
    StartCoroutine(Fade(1f));
  }

  public void QuitGame(){
    Application.Quit();
  }
  private IEnumerator Fade(float finalValue) {
    fadingIn = true;
    yield return new WaitForSeconds(5);
    fadingIn = false;
    SceneManager.LoadSceneAsync(1);
  }

  public void ToggleCW() {
    Warning.SetActive(!Warning.activeSelf);
  }

  public void ResetGameData() {
    PlayerPrefs.SetInt("CurrentDay", 1);
    if (!GameClearedNotification.activeSelf) {
      SelectSound.Play();
      GameClearedNotification.SetActive(true);
      StartCoroutine(waitAndDisableNotification());
    }
  }

  public IEnumerator waitAndDisableNotification()
  {
    yield return new WaitForSeconds(1f);
    GameClearedNotification.SetActive(false);
  }
}
