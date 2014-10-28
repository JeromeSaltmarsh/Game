using UnityEngine;
using System.Collections;

public class GoToDestination : TargetedInstruction
{
	public GameObject destination;
	public float movementSpeed = 0.01f;

	public override void Run ()
	{
		Target.gameObject.transform.position = Vector3.MoveTowards (Target.transform.position, DestPosition, movementSpeed);
	}

	public Vector3 DestPosition{
		get{
			return destination.transform.position;
		}
	}

	public override void Begin ()
	{

	}

	public override bool IsFinished ()
	{
		float dist = Vector3.Distance (Target.transform.position, destination.transform.position);
		if(dist <= (movementSpeed + 0.1f)){
			print ("hi");
		}
		return dist <= (movementSpeed + 0.0001f);		
	}
}

