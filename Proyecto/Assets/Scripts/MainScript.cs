using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!AutoFade.Fading) {
			if(Input.GetKeyDown(KeyCode.Return))
			{
				AutoFade.LoadLevel(1 ,1,1,Color.white);
			}

		}
	}
}
