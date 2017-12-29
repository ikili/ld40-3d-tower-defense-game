using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[Header("General")]
	public float range = 15f;

	[Header("Bullets")]
	public GameObject bulletPrefab;
	public float turnSpeed = 10f;
	public float fireRate = 1f;

	[Header("Laser")]
	public LineRenderer lineRenderer;
	public ParticleSystem impactParticle;
	public Light impactLight;
	public float damageOverTime = 30f;
	public float slowAmount = 0.5f;
	public bool useLaser = false;

	[Header("Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public Transform[] firePoint;

	private Transform target;
	private Enemy targetEnemy;
	private float fireCountdown = 0f;
	private int firePointNum = 0;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		/*
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearestEnemy = null;
		float shortestDistance = Mathf.Infinity;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
 		*/
		Collider[] enemies = Physics.OverlapSphere(transform.position, range);

		GameObject nearestEnemy = null;
		float shortestDistance = Mathf.Infinity;

		foreach (Collider enemy in enemies)
		{
			if (enemy.tag == enemyTag)
			{
				float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
				if (distanceToEnemy < shortestDistance)
				{
					shortestDistance = distanceToEnemy;
					nearestEnemy = enemy.gameObject;
				}
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (target != null && target.tag != enemyTag)
		{
			target = null;
		}
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactParticle.Stop();
					impactLight.enabled = false;
				}
			}
			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();
		}
		else
		{
			if (fireCountdown <= 0)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser()
	{
		if (targetEnemy == null)
		{
			return;
		}
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactLight.enabled = true;
			impactParticle.Play();
		}
		lineRenderer.SetPosition(0, firePoint[0].position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint[0].position - target.position;

		impactParticle.transform.position = target.position + dir.normalized * 0.2f;
		impactParticle.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot()
	{
		if (firePointNum > firePoint.Length - 1)
		{
			firePointNum = 0;
		}
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint[firePointNum].position, firePoint[firePointNum].rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		firePointNum++;
		if (bullet != null)
		{
			bullet.Seek(target);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
