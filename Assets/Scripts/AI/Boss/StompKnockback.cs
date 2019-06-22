using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompKnockback : MonoBehaviour
{
    public float knockRadius = 360f; //the knockback Radius
    public float knockPower = 10f;//Knockback power
    public float upForce = 10f;

    //public Vector3 newForce;
    //private void KnockBack()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
    //    foreach (Collider hit in colliders)
    //    {
    //        Rigidbody pRigid = hit.GetComponent<Rigidbody>();
    //        if (hit.tag == "Player")
    //        {

    //            Vector3 knockBackPos = hit.transform.position - transform.position;
    //            Vector3 launchLocation = new Vector3(knockBackPos.x, 120, knockBackPos.z);
    //            pRigid.AddForce(knockBackPos * 10, ForceMode.Impulse);
    //        }
    //    }
    //}
    private void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "Player")
        {
            ContactPoint contact = col.contacts[0];
            Vector3 normal = -contact.normal;
            Rigidbody rigid = col.gameObject.GetComponent<Rigidbody>();
            Vector3 knockBack = new Vector3(normal.x, 0, normal.z) * knockPower;
            Vector3 force = new Vector3(knockBack.x, upForce, knockBack.z);
            rigid.AddForce(force, ForceMode.Impulse);
            //KnockBack();
        }
    }
    //void OnCollisionEnter(Collision other)
    //{

    //if (other.gameObject.tag == "Player")
    //{
    //    Rigidbody pRigid = other.gameObject.GetComponent<Rigidbody>();
    //    if (pRigid != null)
    //    {
    //        pRigid.AddExplosionForce(100f, transform.position , 1000f, .25f, ForceMode.VelocityChange);
    //        //other.transform.Translate(0, 1, -10);

    //    }

    //}
    //}
}
