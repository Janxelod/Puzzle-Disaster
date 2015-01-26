using UnityEngine;
using System.Collections;

public class InterruptorDoorScript : RecieverScript {

	public string direction;
	public float totalDistance=48;
	private bool isMoving=false;
	private float distance=0;
	private float speed=48;
	public float timeToActivate=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			float shift=(speed*Time.deltaTime);
			distance+=shift;
			Vector3 position=transform.position;
			switch(direction)
			{
				case "up": position.y+=shift; break;
				case "down": position.y-=shift; break;
				case "left": position.x-=shift; break;
				case "right": position.x+=shift; break;
			}
			transform.position=position;
			if(Mathf.Abs(distance)>=totalDistance) isMoving=false;
		}
	}
	public override void makeAction()
	{

		Invoke ("startOpenTheDoor", timeToActivate);
	}
	public void startOpenTheDoor()
	{
		GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playWallSwitch();
		Debug.Log("Make action");
		isMoving = true;
	}
}
