using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 5f;
	public int health = 100;
	public int moneyDrop = 50;
	public GameObject enemyDeathParticle;

	private Transform target;
	private int wavepointIndex = 0;

	void Start()
	{
		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWaypoint();
		}
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		PlayerStats.Money += moneyDrop;

		Instantiate(enemyDeathParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void EndPath()
	{
		PlayerStats.Lives--;
		Destroy(gameObject);
	}
}
