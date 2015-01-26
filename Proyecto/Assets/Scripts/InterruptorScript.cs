using UnityEngine;
using System.Collections;

public class InterruptorScript : MonoBehaviour {

	public bool isActive;
	public GameObject[] reciever;
	public bool canActivateAnyone = false;
	// Use this for initialization
	void Start () {
		isActive = false;

	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			isActive=true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			isActive=false;
		}
	}

	public void sendAction(PlayerScript player)
	{
		if (player.currentCharacter == PlayerScript.Character.Smart||canActivateAnyone) {
			foreach (GameObject item in reciever) {
				if(item)
				{
					isActive=false;
					item.GetComponent<RecieverScript>().makeAction();
				}
			}
			GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playSwitch();
		}else{
			GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playWrong();
			//SONIDO DE EQUIVOCACION
		}

	}
}
