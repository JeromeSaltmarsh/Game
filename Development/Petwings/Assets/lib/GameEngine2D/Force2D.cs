using UnityEngine;
using System.Collections;

public class Force2D : MonoBehaviour
{
		public GameObject2D target;
		public Vector2 value;

		void Start ()
		{
				if (target == null) {
						target = gameObject.GetOrCreateComponent<GameObject2D> ();
				}
		}

		void FixedUpdate ()
		{
				if (!Game.Instance.Paused) {
					target.acceleration += value;
				}
		}
}

