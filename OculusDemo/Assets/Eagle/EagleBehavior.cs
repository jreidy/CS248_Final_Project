using UnityEngine;
using System.Collections;

public class EagleBehavior : MonoBehaviour {


	private bool paused = false;
	
	public GameObject eagle;
	public AudioClip squawk1, squawk2, squawk3, squawk4, squawk5, scream;
	public float minSpeed = 3f,
				 maxSpeed = 10f,
				 rollStep = 0.8f,
				 pitchStep = 0.8f;

	private float attackAngle = 20f,
				  chanceOfAttack = 0.20f; // How likely it is that an eagle goes to attack when it sees you

	private float speed, height;
	private float roll, pitch, yaw;
	private float rollGoal = 0f, pitchGoal = 0f;
	private bool attacking, stopping;
	private float initialRoll;

	private Vector3 centerOfRope = new Vector3(0f,0f,0f);

	private int count = 0;
	
	// Use this for initialization
	void Start () {
		speed = minSpeed;
		height = transform.position.y;

		// Initial direction
		yaw = Random.Range (-180f, 180f);
		transform.eulerAngles = new Vector3 (0f, yaw, 0f);

		// Initial roll
		float angle = Random.Range (15f, 35f);
		float sign = Random.Range (0f, 1f);
		roll = (sign > 0.5 ? 1 : -1) * angle;
		rollGoal = roll;
		initialRoll = roll;
		transform.Rotate (0f, 0f, roll);

		pitch = pitchGoal;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Update position and velocity
	void FixedUpdate () {
		if (paused) return;

		if (pitchGoal != pitch) {
			float diff = pitchGoal - pitch;
			Pitch (Mathf.Abs(diff) < pitchStep ? diff : Mathf.Sign(diff) * pitchStep);
		}
		if (rollGoal != roll) {
			float diff = rollGoal - roll;
			Roll (Mathf.Abs(diff) < rollStep ? diff : Mathf.Sign(diff) * rollStep);
		}

		Yaw(-roll * Time.deltaTime);

		speed += Mathf.Asin (pitch*Mathf.PI/180) * 9.8f * Time.deltaTime;
		if (speed < minSpeed) speed = minSpeed;
		else if (speed > maxSpeed) speed = maxSpeed;

		transform.Translate (0f,0f,Time.deltaTime * speed);

		if (stopping) {
			if (transform.position.y > height) {
				Circulate ();
			}
		} else if (attacking) {
			if (transform.position.y < -8f) {
				StopAttack ();
			}
		} else {
			float x = transform.localPosition.x,
			z = transform.localPosition.z;
			float xzLen = Mathf.Sqrt (x*x + z*z);
			if (xzLen > 0f) {
				float angleOfPosition = Mathf.Sign (transform.localPosition.z) * Mathf.Acos (transform.localPosition.x/xzLen);

				if ((Mathf.Abs(angleOfPosition * 180f / Mathf.PI + yaw + 90f + 360f) % 360f) < attackAngle/2) {
					if (Random.Range (0f,1f) < chanceOfAttack) {
						float angleToTarget = Mathf.Asin (transform.localPosition.normalized.y) * 180 / Mathf.PI * 0.95f;
						angleToTarget = Mathf.Min(angleToTarget,40f);
						Attack (angleToTarget);
					} else {
						Squawk();
					}
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
	}

	void PitchTo(float angle) {
		pitchGoal = angle;
	}

	void Pitch(float deltaAngle) {
		pitch += deltaAngle;
		pitch %= 360f;
		transform.Rotate (deltaAngle, 0f, 0f);
	}
	
	void RollTo(float angle) {
		rollGoal = angle;
	}
	
	void Roll(float deltaAngle) {
		roll += deltaAngle;
		roll %= 360f;
		transform.Rotate (-pitch, 0f, 0f);
		transform.Rotate (0f, 0f, deltaAngle);
		transform.Rotate (pitch, 0f, 0f);
	}
	
	void Yaw(float deltaAngle) {
		yaw += deltaAngle;
		yaw %= 360f;
		transform.Rotate (-pitch, 0f, 0f);
		transform.Rotate (0f, 0f, -roll);
		transform.Rotate (0f, deltaAngle, 0f);
		transform.Rotate (0f, 0f, roll);
		transform.Rotate (pitch, 0f, 0f);
	}

	void Attack(float angle) {
		attacking = true;
		if (scream.isReadyToPlay) {
			AudioSource.PlayClipAtPoint(scream, transform.position);
		}
		stopping = false;
		RollTo (0f);
		PitchTo (angle);
	}

	void StopAttack() {
		stopping = true;
		attacking = false;
		PitchTo (-45f);
	}
	
	void Circulate() {
		stopping = false;
		attacking = false;
		PitchTo (0f);
		RollTo (initialRoll);
	}

	void Squawk() {
		int i = Random.Range(1,6);
		AudioClip squawk = squawk1;
		switch (i) {
		case 1: squawk = squawk1;
			break;
		case 2: squawk = squawk2;
			break;
		case 3: squawk = squawk3;
			break;
		case 4: squawk = squawk4;
			break;
		case 5: squawk = squawk5;
			break;
		}
		if (squawk.isReadyToPlay) {
			AudioSource.PlayClipAtPoint(squawk, transform.position);
		}
	}
}
