using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 70f;
	public GameObject bulletImpactParticle;

	private Transform target;

	void Start()
	{
		
	}

	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget()
	{
		GameObject impactParticle = Instantiate(bulletImpactParticle, transform.position, transform.rotation);
		Destroy(impactParticle, 2f);
		Destroy(gameObject);
		return;
	}

	public void Seek(Transform _target)
	{
		target = _target;
	}
}
