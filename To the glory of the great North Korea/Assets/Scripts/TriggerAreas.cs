using UnityEngine;

public class TriggerAreas : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			other.tag = "InactiveEnemy";
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "InactiveEnemy")
		{
			other.tag = "Enemy";
		}
	}
}
