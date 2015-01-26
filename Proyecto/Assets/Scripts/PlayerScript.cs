using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private bool isCollided=false;
	public GameObject[] invisibleObjects;
	public enum Direction{
		Up=1,
		Down=2,
		Left=3,
		Right=4,
		Stop=5
	} public Direction currentDirection;

	public enum State{
		Alive=1,
		Death=2,
		Transforming=3,
		Waiting=4,
		Ending=5
	} public State currentState;
	public bool isAlive=true;
	public enum Character{
		Smart=1,
		Strong=2,
		Escapist=3,
		Psychic=4,
	} public Character currentCharacter;

	public int health=100;

	private int currentPlayer;
	private bool isMoving = false;
	private Animator animator;
	private KeyCode keyAction;
	private KeyCode keyChangeCharacter;
	private GameObject currentInterruptor;
	public float speed=200f;
	public int totalFriends=2;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		setMovingAnimation ("PlayerDown");
		keyAction = KeyCode.Z;
		keyChangeCharacter = KeyCode.X;
		currentPlayer = 1;
		currentCharacter = Character.Smart;

		foreach (GameObject item in invisibleObjects) {
			item.SetActive(false);
		}
		currentState = State.Alive;
	}


	private void setDirection(Direction newDirection)
	{
		currentDirection = newDirection;
		isMoving = true;
	}
	private void setMovingAnimation(string nameAnimation)
	{
		if (isMoving) {
			animator.Play(Animator.StringToHash(nameAnimation+"Moving"));
		} else {
			animator.Play(Animator.StringToHash(nameAnimation+"Stand"));
		}
	}

	private void checkAnim()
	{
		Vector3 scale=gameObject.transform.localScale;
		switch(currentDirection)
		{
			case Direction.Up:
				setMovingAnimation("PlayerUp");
				break;
			case Direction.Down:
				setMovingAnimation("PlayerDown");
				break;
			case Direction.Left:
				setMovingAnimation("PlayerSide");
				scale.x=1;
				transform.localScale=scale;
				break;
			case Direction.Right:
				setMovingAnimation("PlayerSide");
				scale.x=-1;
				transform.localScale=scale;
				break;
		}

	}


	private void changeCharacter()
	{
		currentPlayer++;
		//currentCharacter.ge
		if (currentPlayer > totalFriends) {
			currentPlayer=1;
		}
		switch (currentPlayer) {
			case 1: currentCharacter=Character.Smart; break;
			case 2: currentCharacter=Character.Strong; break;
			case 3: currentCharacter=Character.Escapist; break;
			case 4: currentCharacter=Character.Psychic; break;
		}
		if (currentCharacter == Character.Psychic) {
			foreach (GameObject item in invisibleObjects) {
				item.SetActive(true);
			}
		}else
		{
			foreach (GameObject item in invisibleObjects) {
				item.SetActive(false);
			}
		}
		RuntimeAnimatorController animator = Resources.Load<RuntimeAnimatorController> ("Anim/Player"+currentPlayer+"/Player"+currentPlayer);
		GetComponent<Animator> ().runtimeAnimatorController = animator;	
	}

	private void checkKey()
	{
		if (Input.GetKeyDown (keyAction)) {
			if(currentInterruptor!=null)
			{
				if(currentInterruptor.GetComponent<InterruptorScript>().isActive)
				{
					currentInterruptor.GetComponent<InterruptorScript>().sendAction(this);
				}
			}
		} else if (Input.GetKeyDown (keyChangeCharacter)) {
			changeCharacter();
		}
	}
	// Update is called once per frame
	void Update () {
		switch (currentState) {
			case State.Alive:
				checkKey ();
				checkAnim ();
			break;
			case State.Transforming:

			break;
			case State.Waiting:
				
			break;
			case State.Death:
				if(isAlive)
				{
					animator.Play(Animator.StringToHash("PlayerDownStand"));
					Vector3 myRotation= transform.rotation.eulerAngles;
					myRotation.z=270;
					transform.rotation=Quaternion.Euler(myRotation);
					GetComponent<SpriteRenderer>().color=new Color(1f,0f,0f,1f);
					Invoke("resetGame",1);
					isAlive=false;
				}
			break;
			case State.Ending:
				if(!finishGame)
				{
					finishGame=true;
					AutoFade.LoadLevel(3,1,1,Color.black);
				}
			break;
		}
	}
	private bool finishGame=false;

	void resetGame()
	{
		AutoFade.LoadLevel(0 ,1,1,Color.red);
	}
	void FixedUpdate()
	{
		if(currentState!=State.Death&&currentState!=State.Waiting&&currentState!=State.Ending)
			move ();
	}

	private bool madeTeleport=false;
	private GameObject currentTeleport;
	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log("Something has entered this zone."); 
		if(currentState!=State.Death&&currentState!=State.Ending)
			onTriggerEnter (other);
	}
	public void startTeleport()
	{
		Vector3 position=currentTeleport.GetComponent<TeleporterScript>().target.transform.position;
		transform.position=position;
		if(currentTeleport.GetComponent<TeleporterScript>().target.GetComponent<TeleporterScript>())
		{
			madeTeleport=true;//Quiere decir que el teleport lo hizo hacia otro teleport
		}else
		{
			madeTeleport=false;//Quiere decir que el teleport lo hizo hacia un target vacio.
		}
		currentState = State.Alive;

	}
	void onTriggerEnter(Collider2D other)
	{
		if (other.gameObject.name == "Teleporter") {
			if(!madeTeleport)
			{
				currentTeleport=other.gameObject;
				currentState=State.Waiting;

				Invoke("startTeleport",1.1f);
				GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playTeleport();
				
			}
		}else if (other.gameObject.name == "Laser(Clone)") {

			health-=10;
			checkHealth();
			Destroy (other.gameObject);

		}else if(other.gameObject.name=="InterruptorArea"){
			currentInterruptor=other.gameObject;
		}else if(other.gameObject.name=="CollisionLava"){

			Debug.Log("Collisiono Lava");
			currentState=State.Death;

		}
		else if(other.gameObject.name=="EndingTrigger"){
			Debug.Log("Termino Juego");
			currentState=State.Ending;
		}else {
			
			if(other.gameObject.name=="wall" && currentCharacter==Character.Escapist)
			{
				//Debug.Log("Traspaso escapista");
				//currentState=State.Death;
			}else
			{
				isCollided = true;
				Vector3 newPosition = transform.position;
				switch(currentDirection)
				{
				case Direction.Up:
					newPosition.y-=speed*Time.fixedDeltaTime;
					break;
				case Direction.Down:
					newPosition.y+=speed*Time.fixedDeltaTime;
					break;
				case Direction.Left:
					newPosition.x+=speed*Time.fixedDeltaTime;
					break;
				case Direction.Right:
					newPosition.x-=speed*Time.fixedDeltaTime;
					break;
				}
				transform.position = newPosition;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("Something has entered this zone.");    
		if(currentState!=State.Death&&currentState!=State.Ending)
			onTriggerExit (other);
	}
	private void onTriggerExit(Collider2D other)
	{
		if (other.gameObject.name == "Teleporter") {
			if(other.gameObject!=currentTeleport)
			{
				madeTeleport=false;
			}
		}else if(other.gameObject.name=="InterruptorArea")
		{
			
		}else if(other.gameObject.name=="wall"){
			
		}
		isCollided = false;
	}
	private void move()
	{
		Vector3 newPosition = transform.position;
		if (!isCollided) {

			if (Input.GetKey (KeyCode.UpArrow)) {
				newPosition.y+=speed*Time.fixedDeltaTime;
				setDirection(Direction.Up);
			}else if(Input.GetKey (KeyCode.DownArrow)) {
				newPosition.y-=speed*Time.fixedDeltaTime;
				setDirection(Direction.Down);
			}else if(Input.GetKey (KeyCode.LeftArrow)) {
				newPosition.x-=speed*Time.fixedDeltaTime;
				setDirection(Direction.Left);
			}else if(Input.GetKey (KeyCode.RightArrow)) {
				newPosition.x+=speed*Time.fixedDeltaTime;
				setDirection(Direction.Right);
			}else
			{
				isMoving=false;
			}
		}
		transform.position = newPosition;
	}
	private void checkHealth()
	{
		if (health < 0) {
			currentState=State.Death;
		}else
		{
			//GetComponent<SpriteRenderer>().color=new Color32(
			//Toca sonido
			//Debug.Log(GetComponent<SpriteRenderer>().color.g+","+GetComponent<SpriteRenderer>().color.b);
			if(currentCharacter==(Character.Smart)||(currentCharacter==Character.Escapist))
				GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playHurtWoman();
			else
				GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playHurtMan();

			GetComponent<SpriteRenderer>().color=new Color(1,(float)(GetComponent<SpriteRenderer>().color.g-0.1f),(float)(GetComponent<SpriteRenderer>().color.b-0.1f),1);
		}
	}

}
