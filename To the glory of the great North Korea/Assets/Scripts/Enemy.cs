using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public float startSpeed = 5f;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public int startNodeID;
	public float startHealth = 100f;
	public int moneyDrop = 50;
	public GameObject enemyDeathParticle;

	[Header("HealthUI")]
	public RectTransform healthBar;
	private float health;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.offsetMax = new Vector2(-(100f - (health / startHealth * 100f)), 0f);
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
		WaveSpawner.EnemiesAlive--;

		Instantiate(enemyDeathParticle, new Vector3(transform.position.x, 2f, transform.position.z), Quaternion.identity);
		Destroy(gameObject);
	}
}
