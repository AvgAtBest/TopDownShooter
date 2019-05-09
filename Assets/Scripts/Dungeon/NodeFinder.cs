using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFinder : MonoBehaviour
{
    public List<GameObject> nodes;
    void Start()
    {
        FindNodes();
    }

    // Update is called once per frame
    public void FindNodes()
    {
        foreach(Transform child in transform)
        {
            nodes.Add(child.gameObject);

        }
    }
}
