using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooting : MonoBehaviour
{
    public int damage = 34;
    public float timerToFire = 1;
    public float speed = 5f;
    public float maxAmmo = 30f;
    public float ammo = 30f;
    public float maxRateOfFireTime = 1f;
    public float range = 7f;
    public float lineDelay = .05f;
    public float shootTimer = 0;
    public bool canShoot = true;
    public Transform shootPoint;
    private LineRenderer lineRenderer;
    private Vector3 hitPoint;
    //private Vector3 shotRay;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void Start()
    {

        ammo = maxAmmo;
    }

    public void Reload()
    {
        ammo = maxAmmo;
    }

    public void PrimaryFire()
    {
        if (canShoot)
        {
            //creates a Ray, that starts at the position of the muzzle(A.K.A shootpoint), and directs it on the BLUE axis where the muzzle is facing in the world
            Ray shotRay = new Ray(shootPoint.position, shootPoint.forward);


            Vector3 start = shootPoint.position;
            Vector3 end = shootPoint.position + shootPoint.forward * range;

            //Creates a "hit", which retrieves information where the ray hits
            RaycastHit hit;
            Physics.Raycast(shotRay, out hit, range);
            Debug.Log(hit.point.ToString());
            Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.red);
            if (Physics.Raycast(shotRay, out hit, range))
            {
                hitPoint = hit.point;
                end = hitPoint;
                hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }

            //takes 1 bullet away from the current ammo pool
            ammo--;
            //resets the fire rate timer to 0
            timerToFire = 0;
            //and you cannot fire (clamp on the maximum rate of fire)
            canShoot = false;
            // Enable line (show line as Coroutine)
            StartCoroutine(ShowLine(start, end, lineDelay));
        }

        IEnumerator ShowLine(Vector3 start, Vector3 end, float lineDelay)
        {
            // Update and Show the line
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.enabled = true;

            // Wait a few seconds
            yield return new WaitForSeconds(lineDelay);

            // Hide the line
            lineRenderer.enabled = false;
        }

        void FixedUpdate()
        {
            // Rate of Fire
            timerToFire += Time.deltaTime;

            if (timerToFire >= maxRateOfFireTime)
            {
                canShoot = true;
            }
            // Out of ammo
            if (ammo <= 0)
            {
                // Can't shoot
                ammo = 0;
                canShoot = false;
            }
            Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.red);

            Vector3 start = shootPoint.position;
            Vector3 end = shootPoint.position + shootPoint.forward * range;

            //Creates a "hit", which retrieves information where the ray hits
            //RaycastHit hit;
            //Physics.Raycast(shotRay, out hit, range);
            //Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);
            //if (Physics.Raycast(shotRay, out hit, range))
            //{
            //    // Note to self (Add this back once enemy has a health script)
            //    //hitPoint = hit.point;
            //    //end = hitPoint;
            //    //if (hit.collider.GetComponent<Health>())
            //    //{
            //    //    hit.collider.GetComponent<Health>().TakeDamage(damage);
            //    //}
            //    //Fire();
            //
            //
            //
            //}


            //takes 1 bullet away from the current ammo pool
            ammo--;
            //resets the fire rate timer to 0
            shootTimer = 0;
            StartCoroutine(ShowLine(start, end, lineDelay));

            // If R is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reload weapon
                Reload();
            }


        }


    }
}
