using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
  [SerializeField]
  List<Message> fluteMessages;
    [SerializeField]
  List<Message> walletMessages;

  [SerializeField]
  private TextMeshProUGUI myTextBox;

  [SerializeField]
  private Billy bill;

  [SerializeField]
  private GameObject PauseMenu;
  [SerializeField]
  private GameManager GM;
  

  public void InspectPhone() {}

  public void InspectMap() {}

  public void InspectWallet() {
    Debug.Log("attempting");
    if (!GM.hasMoney) {
      TextMaster.ShowText(walletMessages[0], myTextBox);
    } else {
      switch (bill.billState) {
        case BillyState.BEG:
          TextMaster.ShowText(walletMessages[1], myTextBox);
          break;
        case BillyState.PREINTRO:
          TextMaster.ShowText(walletMessages[0], myTextBox);
          break;
        case BillyState.RICH:
          TextMaster.ShowText(walletMessages[2], myTextBox);
          break;
        default:
          TextMaster.ShowText(walletMessages[0], myTextBox);
          break;
      }
    }
  }

  public void InspectFlute() {}

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Cursor.visible = !PauseMenu.activeSelf;
      PauseMenu.SetActive(!PauseMenu.activeSelf);
      if (!PauseMenu.activeSelf) TextMaster.ClearText(myTextBox);
    }
  }
}
