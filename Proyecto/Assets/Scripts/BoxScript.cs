using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			PlayerScript playerScript=other.gameObject.GetComponent<PlayerScript>();
			if(playerScript.currentCharacter==PlayerScript.Character.Strong)
			{
				switch (other.GetComponentInParent<PlayerScript> ().currentDirection) {
				case PlayerScript.Direction.Down:
					rigidbody2D.AddForce (new Vector2 (0, -100));
					break;
				case PlayerScript.Direction.Up:
					rigidbody2D.AddForce (new Vector2 (0, 100));
					break;
				case PlayerScript.Direction.Left:
					rigidbody2D.AddForce (new Vector2 (-100, 0));
					break;
				case PlayerScript.Direction.Right:
					rigidbody2D.AddForce (new Vector2 (100, 0));
					break;
				}
			}
		}
	}
}
