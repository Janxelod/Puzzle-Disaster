using UnityEngine;
using System.Collections;

public class SpecialWallScript : MonoBehaviour {

	public BoxCollider2D myCollider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D()
	{
		PlayerScript player = GameObject.Find ("Player").GetComponent<PlayerScript>();
		if (player.currentCharacter == PlayerScript.Character.Escapist) {
			//GetComponent<BoxCollider2D>().enabled=false;
		}
	}
}
