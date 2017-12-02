using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[Header("Attributes")]
	public float range = 15f;
	public float turnSpeed = 10f;
	public float fireRate = 1f;
	[Header("Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public Transform[] firePoint;
	public GameObject bulletPrefab;

	private Transform target;
	private float fireCountdown = 0f;
	private int firePointNum = 0;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
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

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (target == null)
		{
			return;
		}
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
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
