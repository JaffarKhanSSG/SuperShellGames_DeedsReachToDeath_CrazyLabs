using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startPoint;
    public GameObject endPoint;
    public float maxDistanceForPlayer;
    float dist;
    public void Update()
    {
        //dist = Vector3.Distance(GameManager.instance.CurrentPlayer.transform.position, transform.position);
        if (transform.position.z<=GameManager.instance.CurrentPlayer.transform.position.z-maxDistanceForPlayer)
        {
            GameManager.instance.OutOfScreen(this);
        }
    }
}
