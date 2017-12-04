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
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWaypoint();
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
