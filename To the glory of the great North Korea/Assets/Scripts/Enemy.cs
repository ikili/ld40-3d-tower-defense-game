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
	private bool isAlreadyDead = false;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.offsetMax = new Vector2(-(100f - (health / startHealth * 100f)), 0f);
		if (health <= 0 && isAlreadyDead == false)
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
		isAlreadyDead = true;
		PlayerStats.Money += moneyDrop;
		if (BuildManager.HoveredNode != null)
		{
			BuildManager.HoveredNode.OnMouseEnter();
		}
		Instantiate(enemyDeathParticle, new Vector3(transform.position.x, 2f, transform.position.z), Quaternion.identity);
		Destroy(gameObject);
		WaveSpawner.EnemiesAlive--;
		WaveSpawner.EnemiesKilled++;
	}
}
