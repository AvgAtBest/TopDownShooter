using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rigid;
    private Vector3 motion;
    private Camera cam;
    public bool isTopDown;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * speed;
        float inputZ = Input.GetAxis("Vertical") * speed;

        Vector3 moveDir = new Vector3(inputH, 0f, inputZ);
        Vector3 camEuler = Camera.main.transform.eulerAngles;
        moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

        rigid.velocity = force;
        Quaternion playerRot = Quaternion.AngleAxis(camEuler.y, Vector3.up);
        transform.rotation = playerRot;
    }
    //void Move(float inputH, float inputZ)
    //{
    //    Vector3 direction = new Vector3(inputH, 0f, inputZ);

    //    motion.x = direction.x * speed;
    //    motion.z = direction.z * speed;

    //}
}
