﻿using UnityEngine;
using System.Collections;
using GameUtil2D;

public class Player : MonoBehaviour
{
		public KeyCode accelerateKey = KeyCode.RightArrow;
		public KeyCode breakKey = KeyCode.LeftArrow;
		public float acceleration;
		public static int currentLevel = 0;
		public bool activated = false;
		public int heldDownFor = 0;
		public bool moving = false;
		public static Player instance;

		void Start ()
		{
				Player.instance = this;
		}

		void ActivateGlintGenerator (bool enabled)
		{
				GameObject glintGenerator = GameObject.Find ("glintGenerator");
				glintGenerator.GetComponent<ParticleGenerator> ().enabled = enabled;
		}

		void FixedUpdate ()
		{
				moving = false;
				ReadKeyboardInput ();
				ReadScreenInput ();
				ActivateGlintGenerator (moving);
				if (moving) {
						GameObject.Find ("mouth").GetComponent<Mouth> ().OpenMouth ();	

				} else {
						GameObject.Find ("mouth").gameObject.GetComponent<Mouth> ().Smile ();
				}

				if (gameObject.Left () < GameScreen.Left) {
						gameObject.Left (GameScreen.Left);
				}

				if (transform.position.x > GameScreen.Right) {
						Level.instance.NextLevel ();
				}

				
		}

		void ReadScreenInput ()
		{
				for (var i = 0; i < Input.touchCount; ++i) {
						Touch touch = Input.GetTouch (i);

						if (activated == false) {

								if (touch.phase == TouchPhase.Began) {
										activated = true;
								}
						}						

						if (activated) {
								if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
										if (touch.position.x > (Screen.width / 2)) {
												Accelerate ();
										} else {
												Reverse ();
										}
								}
						} 
				}
		}

		void ReadKeyboardInput ()
		{
				if (Input.GetKeyDown (accelerateKey) || Input.GetKeyDown (breakKey)) {
						activated = true;
				}
			
				if (Input.GetKey (accelerateKey)) {
						heldDownFor++;
						if (heldDownFor > 15) {
								activated = true;
						}
				}

				if (activated) {

						if (Input.GetKey (accelerateKey) || Input.GetKey (KeyCode.D)) {
								Accelerate ();
						}		
						if (Input.GetKey (breakKey) || Input.GetKey (KeyCode.A)) {
								Reverse ();
						}
				} 
		}

		void Accelerate ()
		{
				GetComponent<Velocity> ().AddAcceleration (acceleration);
				
				Vector3 scale = gameObject.transform.localScale;
				scale.x = 0.25f;
				transform.localScale = scale;
				moving = true;
		}

		void Reverse ()
		{
				GetComponent<Velocity> ().AddAcceleration (-acceleration);

				Vector3 scale = gameObject.transform.localScale;
				scale.x = -0.25f;
				transform.localScale = scale;
				moving = true;
		}
}
