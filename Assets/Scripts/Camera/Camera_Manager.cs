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
        cameras = FindObjectsOfType<Camera>();
		
        for (int i = 0; i < cameras.Length; i++)
        {
            cameraCount = Camera.allCameras.Length;
            //cameraCount++;
            
        }
        cameras[0].gameObject.SetActive(false);

    }

    private void LoadCamSwitchTo()
    {
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false);
    }
    public void MainCamSwitchTo()
    {
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
    }
}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}

