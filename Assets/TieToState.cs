using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieToState : MonoBehaviour {

  [SerializeField]
  private Items myItem;
  [SerializeField]
  private GameObject myButton;

	void Update () {
		var setTrue = false;

    switch (myItem) {
      case Items.FLUTE:
        setTrue = GameManager.instance.hasFlute;
        break;
      case Items.WALLET:
        setTrue = GameManager.instance.hasWallet;
        break;
      case Items.PHONE:
        setTrue = GameManager.instance.hasPhone;
        break;
      case Items.MAP:
        setTrue = GameManager.instance.hasMap;
        break;
    }

    myButton.SetActive(setTrue);
	}
}
