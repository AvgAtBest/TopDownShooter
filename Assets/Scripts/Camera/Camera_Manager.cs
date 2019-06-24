using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Manager : MonoBehaviour
{
	//public Camera camera;
	public Camera[] cameras;
	public int cameraCount;
	//public List<Camera> cameras = new List<Camera>();
	// Start is called before the first frame update

    void Start()
    {
		//finds all of the cameras in the scene
        cameras = FindObjectsOfType<Camera>();
		//loops through them
        for (int i = 0; i < cameras.Length; i++)
        {
			//adds to array of cameras
            cameraCount = Camera.allCameras.Length;
            //cameraCount++;
            
        }
				//sets the first camera off by default (this will be the side view camera)
        cameras[0].gameObject.SetActive(false);

    }
	//switches to the camera in the load buffer zone (side scroller camera)
    private void LoadCamSwitchTo()
    {
		//side camera on
        cameras[0].gameObject.SetActive(true);
		//main cam off
        cameras[1].gameObject.SetActive(false);
    }
	//switches to the main tracking camera
    public void MainCamSwitchTo()
    {
		//side camera off
        cameras[0].gameObject.SetActive(false);
		//main cam on
        cameras[1].gameObject.SetActive(true);
    }
}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}

