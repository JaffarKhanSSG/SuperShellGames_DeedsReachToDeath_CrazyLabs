using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyEffect : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance && GameManager.instance.CurrentPlayer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.instance.CurrentPlayer.transform.position.z);
        }
    }
}
