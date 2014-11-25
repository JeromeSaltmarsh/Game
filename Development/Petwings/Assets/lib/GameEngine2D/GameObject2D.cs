using UnityEngine;
using System.Collections;

public class GameObject2D : MonoBehaviour
{
		public float speed = 1f;
		public float rotation;
		public float rotationAcceleration;
		public float rotationVelocity;
		public float rotationFriction = 1f;
		public Vector2 position;
		public Vector2 acceleration;
		public Vector2 velocity;
		public float friction = 1f;
		public bool inGameObject = true;

		void Start ()
		{
				position = transform.position;
				rotation = transform.eulerAngles.z;
		}

		void FixedUpdate ()
		{
				position = transform.position;
				float relativeTime = RelativeTime;

				velocity += acceleration * relativeTime;
				acceleration = Vector2.zero;
				velocity *= friction;
				position += velocity * relativeTime;

				rotationVelocity += rotationAcceleration * relativeTime;
				rotationVelocity *= rotationFriction;
				rotation += rotationVelocity * relativeTime;

				transform.position = position;
				Vector3 eulerAngles = gameObject.transform.eulerAngles;
				eulerAngles.z = rotation;
				gameObject.transform.eulerAngles = eulerAngles;
		}
	
		public float RelativeTime {
				get {
						if (inGameObject) { 
								return speed * Game.GameSpeed;
						} else {
								return speed * (60f / Time.deltaTime);
						}
				}
		}
}

