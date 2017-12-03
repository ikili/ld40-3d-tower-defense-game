using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float startSpeed = 5f;
	[HideInInspector]
	public float speed;
	public float health = 100f;
	public int moneyDrop = 50;
	public GameObject enemyDeathParticle;

	void Start()
	{
		speed = startSpeed;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Die();
		}
	}

	public void Slow(float slowAmount)
	{
		speed = startSpeed * (1f - slowAmount);
	}

	void Die()
	{
		PlayerStats.Money += moneyDrop;

		Instantiate(enemyDeathParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
