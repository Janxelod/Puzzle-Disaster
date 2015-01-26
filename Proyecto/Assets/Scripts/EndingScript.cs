using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!AutoFade.Fading) {
			if(Input.GetKeyDown(KeyCode.Return))
			{
				AutoFade.LoadLevel(0 ,1,1,Color.white);
			}
			
		}
	}
}
