using UnityEngine;
using System.Collections;

public class CanionScript : MonoBehaviour {

	public GameObject originLaser;
	public float frequencyShoot=5f;
	public float currentTime=0;
	public float speedxLaser;
	public GameObject laserPreFab;
	public float speedyLaser;
	public int shootNumbers=0;
	public int totalShootsToStop=5;
	public float timeToStartToShoot=1.25f;
	private bool stopShoot=false;
	public int initialShoots=5;
	public enum Direction
	{
		Up=1,
		Down=2,
		Right=3,
		Left=4
	}
	public Direction currentDirection;
	// Use this for initialization
	void Start () {
		checkRotation ();
		speedxLaser = 250f;
		speedyLaser = 250f;
	}
	void checkRotation()
	{
		int rotation = (int)transform.rotation.eulerAngles.z;
		//Debug.Log (transform.rotation.eulerAngles.z);
		switch (rotation) {
			case 0: currentDirection=Direction.Right; break;
			case 360: currentDirection=Direction.Right; break;
			case 90: currentDirection=Direction.Up; break;
			case 180: currentDirection=Direction.Left; break;
			case 270: currentDirection=Direction.Down; break;
			default: currentDirection=Direction.Right; break;
		}
	}
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;
		if (!stopShoot) {
			if (currentTime >= frequencyShoot) {
				currentTime=0;
				shoot();
				shootNumbers++;

				if (shootNumbers == totalShootsToStop) {
					stopShoot=true;
				}
			}
		}else
		{
			if (currentTime >= timeToStartToShoot)
			{
				stopShoot=false;
				currentTime=0;
				shootNumbers=0;
				timeToStartToShoot=Random.Range(0.5f,timeToStartToShoot);
				totalShootsToStop=Random.Range(initialShoots,initialShoots+2);
			}
		}
		checkRotation ();
	}

	void shoot()
	{
		float speedX=0,speedY=0;
		if (currentDirection == Direction.Right || currentDirection == Direction.Left) {
			speedX = (currentDirection == Direction.Right) ? speedxLaser : speedxLaser * -1; 
			//speedY=0;
		}else if (currentDirection == Direction.Up || currentDirection == Direction.Down) {
			speedY = (currentDirection == Direction.Up) ? speedyLaser : speedyLaser * -1; 
			//speedX=0;
		}

		GameObject laser = Instantiate (laserPreFab) as GameObject;
		laser.transform.position = originLaser.transform.position;
		laser.transform.parent = transform.parent;
		laser.GetComponent<LaserScript> ().speedX = speedX;
		laser.GetComponent<LaserScript> ().speedY = speedY;
		laser.GetComponent<LaserScript> ().setInitialRotation (transform.rotation.eulerAngles.z);
		//float speedY=
	}
}
