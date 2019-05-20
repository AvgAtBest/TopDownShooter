using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 10f;
    public float diveSpeed = 20f;
    private CharacterController charC;
    private Vector3 motion;
    private Camera cam;
    float counter;
    bool endReached;
    float gravity = 15f;
    void Start()
    {
        charC = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
        counter = 0;
    }
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        
        
        Move(inputH, gravity, inputZ);
        charC.Move(motion * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

        }


    }
    void Move(float inputH,float gravity, float inputZ)
    {
        Vector3 direction = new Vector3(inputH, gravity, inputZ);
        motion.x = direction.x * speed;
        //shit gravity
        //motion.y -= direction.y - gravity * Time.deltaTime;
        motion.z = direction.z * speed;


    }
    void Dive()
    {

        motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        motion = transform.TransformDirection(motion);
        motion *= diveSpeed;
        motion.z = diveSpeed;
        motion.y -= gravity * Time.deltaTime;
        #region Terrible
        //Vector3 startPos = transform.position;
        //Vector3 pos = new Vector3(startPos.x, startPos.y + Mathf.Sin(Mathf.PI * 2 * counter / 360), startPos.z + Mathf.Sin(Mathf.PI * 2 * counter / 360));
        //transform.position = Vector3.Lerp(transform.position, pos, 1f);
        //counter += speed;
        //if(counter >= 180)
        //{
        //    endReached = true;
        //}
        #endregion

    }
}
