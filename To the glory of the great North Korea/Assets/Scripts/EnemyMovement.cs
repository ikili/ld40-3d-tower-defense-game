using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private Transform target;
	private int waypointIndex = 0;

	private Waypoints wayPoints;
	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		GameObject wayPointsGO = GameObject.FindGameObjectWithTag("Waypoints" + enemy.startNodeID.ToString());
		wayPoints = wayPointsGO.GetComponent<Waypoints>();
		target = wayPoints.points[0];
		/*if (GameSettings.Difficulty > 6)
		{
			enemy.moneyDrop = (int)(enemy.moneyDrop / 2);
		}*/
	}

	void FixedUpdate()
	{
		Vector3 dir = target.position - this.transform.localPosition;

		float distThisFrame = enemy.speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame)
		{
			GetNextWaypoint();
		}
		else
		{
			transform.Translate(dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(dir);
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * (enemy.speed * 0.75f));
		}

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (waypointIndex >= wayPoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		waypointIndex++;
		target = wayPoints.points[waypointIndex];
	}

	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}
