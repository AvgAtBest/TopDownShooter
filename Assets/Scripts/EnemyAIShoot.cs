using System.Collections;
using System.Collections.Generic;
using TopDownShooter;
using UnityEngine;

public class EnemyAIShoot : MonoBehaviour
{
	public Transform muzzle;
	public int damage = 10;
	public LineRenderer laser;
	public float delayBetweenShots = 0.2f;
	public float shotDuration = 0.25f;
	public float weaponRange = 75f;

	[Header("Rotation")]
	public float minAngle = -45f;
	public float maxAngle = 45f;
	//public AudioSource soundSource;
	//public AudioClip[] soundclips;
	//public LayerMask targetMask;


	// Use this for initialization
	void Start()
	{
		laser = GetComponentInChildren<LineRenderer>();
		//soundSource = GetComponentInChildren<AudioSource>(true);

	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public float ToNegative(float angle)
	{
		return (angle > 180) ? angle - 360 : angle;
	}
	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = GetComponentInParent<EnemyAIStates>().targetPos;

		/*\
|============|
|Thanks Manny|
|============|
		\*/
		Vector3 forward = targetPosition - transform.position;
		transform.rotation = Quaternion.LookRotation(forward.normalized);

		Vector3 localEuler = transform.localEulerAngles;
		// Convert to negative if positive
		localEuler.x = ToNegative(localEuler.x);
		localEuler.y = ToNegative(localEuler.y);
		// Filter with min and max
		localEuler.x = ClampAngle(localEuler.x, minAngle, maxAngle);
		localEuler.y = ClampAngle(localEuler.y, minAngle, maxAngle);
		transform.localEulerAngles = localEuler;



		Vector3 origin = muzzle.transform.position;
		RaycastHit hit;
		Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange);
		if (hit.collider)
		{
			if (hit.collider.CompareTag("Player") && laser.enabled)
			{
				laser.SetPosition(0, muzzle.position);
				laser.SetPosition(1, GameObject.Find("Player").transform.position);
				DealDamage();
			}
			else
			{
				laser.SetPosition(0, Vector3.zero);
				laser.SetPosition(1, Vector3.zero);
			}
		}
	}
	void DealDamage()
	{
		PlayerHealth pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
		StartCoroutine(Shooting());
		if (pHealth != null)
		{
			pHealth.TakeDamage(damage);
		}
		#region old script
		//Vector3 origin = muzzle.transform.position;

		//RaycastHit hit;
		//if (Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask))
		//{
		//    if (hit.collider.tag == "Player")
		//    {
		//        laser.SetPosition(1, hit.point);
		//        Vector3 direction = (hit.point - muzzle.position).normalized;
		//        PlayerHealth pHealth = hit.collider.GetComponent<PlayerHealth>();
		//        if (pHealth != null)
		//        {
		//            pHealth.TakeDamage(damage);
		//        }


		//    }
		//    else
		//    {
		//        //laser.SetPosition(1, origin + (transform.forward * weaponRange));
		//    }
		//}
		#endregion
	}
	private IEnumerator Shooting()
	{
		laser.enabled = true;
		//soundSource.clip = soundclips[0];
		//soundSource.Play();
		yield return new WaitForSeconds(shotDuration);
		laser.enabled = false;
		yield return new WaitForSeconds(delayBetweenShots);
		laser.enabled = true;

	}
}
