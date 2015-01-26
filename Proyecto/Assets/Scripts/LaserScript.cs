using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public float speedX;
	public float speedY;

	// Use this for initialization
	void Start () {

	}
	public void setInitialRotation(float rotation){
		Vector3 myRotation= transform.rotation.eulerAngles;
		myRotation.z=rotation;
		transform.rotation=Quaternion.Euler(myRotation);
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 position = transform.position;

		position += new Vector3 ((speedX * Time.deltaTime),(speedY * Time.deltaTime), 0f);
		transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D other){
		Invoke ("KillLaser", 0.125f);
		//Destroy (GetComponent<Collider2D>());
		GetComponent<BoxCollider2D> ().isTrigger = false;
		//Debug.Log ("Ontrigger");
	}

	void KillLaser()
	{
		Destroy (gameObject);
		//Debug.Log ("KillLaser");
	}
}
