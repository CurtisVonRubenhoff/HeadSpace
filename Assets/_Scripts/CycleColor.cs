using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColor : MonoBehaviour {

  [SerializeField]
  Camera thisCamera;

  [SerializeField]
  bool isCamera;
  [SerializeField]
  Light light;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    float h, s, v;

    var curr = (isCamera) ? thisCamera.backgroundColor: light.color;
    Color.RGBToHSV(curr, out h, out s, out v);
    h += .001f;
    if (h > 1.0f) h = 0f;

    var newColor = Color.HSVToRGB(h, s, v);

    if (isCamera) thisCamera.backgroundColor =newColor;
    else light.color = newColor;
	}
}
