using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy" || other.tag == "InactiveEnemy")
		{
			MusicController.Instance.PlaySoundEffects("underattack");
		}
	}
}
