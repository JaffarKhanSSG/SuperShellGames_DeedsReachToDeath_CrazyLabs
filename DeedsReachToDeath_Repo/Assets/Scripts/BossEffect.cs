using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public float randomX;
    float maxDistance=10;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        randomX = Random.Range(-1.5f, 1.5f);
    }
    public float forwardSpeed=3;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        transform.position = Vector3.Lerp(transform.position, new Vector3(randomX, transform.position.y, transform.position.z), Time.deltaTime * 2);
        float dist = Vector3.Distance(transform.position, startPos);
        if (dist >= 15)
        {
            gameObject.SetActive(false);
        }
    }

}
