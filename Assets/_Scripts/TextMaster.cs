using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextMaster: MonoBehaviour {
  [SerializeField]
  private Text MainTextBox;

  [SerializeField]
  private Text InteractionIndicator;

  [SerializeField]
  private float letterpause;

  [SerializeField]
  private GameObject Choices;

  public Button AcceptButton;
  public Button DenyButton;

  public static TextMaster instance;
  private void Start() {
    if (instance == null) instance = this;
  }

  public static void ShowText(Message message) {
    instance.StopAllCoroutines();
    ClearText();
    instance.StartCoroutine(instance.typeText(message.message));
    if (message.isPrompt) EnableChoices();
  }

  public static void ClearText() {
    instance.StopAllCoroutines();
    instance.MainTextBox.text = "";
  }

  public static void ClearText(TextMeshPro tmp) {
    instance.StopAllCoroutines();
    tmp.text = "";
  }

  public static void ClearText(TextMeshProUGUI tmp) {
    instance.StopAllCoroutines();
    tmp.text = "";
  }

  public static void ShowText(Message message, TextMeshPro tmp) {
    instance.StopAllCoroutines();
    ClearText(tmp);
    instance.StartCoroutine(instance.typeText(message.message, tmp));
  }

  public static void ShowText(Message message, TextMeshProUGUI tmp) {
    instance.StopAllCoroutines();
    ClearText(tmp);
    instance.StartCoroutine(instance.typeText(message.message, tmp));
  }

  public static void IndicatorOn(string name) {
    if (name == "") return;
    instance.InteractionIndicator.text = string.Format("[E] {0}", name);
  }

  public static void IndicatorOff() {
    Debug.Log("turning off indicator");
    instance.InteractionIndicator.text = "";
  }

  public static void DisableChoices() {
    Cursor.visible = false;
    instance.Choices.SetActive(false);
  }

  public static void EnableChoices() {
    Cursor.visible = true;
    instance.Choices.SetActive(true);
  }

  private IEnumerator typeText(string s)
  {
    foreach (char letter in s.ToCharArray())
    {
      MainTextBox.text += letter;
      yield return new WaitForSeconds(letterpause);
    }
  }

  private IEnumerator typeText(string s, TextMeshPro tmp)
  {
    foreach (char letter in s.ToCharArray())
    {
      tmp.text += letter;
      yield return new WaitForSeconds(letterpause);
    }
  }

  private IEnumerator typeText(string s, TextMeshProUGUI tmp)
  {
    foreach (char letter in s.ToCharArray())
    {
      tmp.text += letter;
      yield return new WaitForSeconds(letterpause);
    }
  }
}
