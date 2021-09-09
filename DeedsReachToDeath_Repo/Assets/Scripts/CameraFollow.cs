using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool stopFollow;

    // Update is called once per frame
    public Vector3 followPointLeft, followPointRight, followPointMiddle;
    public GameObject target;
    public Vector3 currentPos;
    public float speed;
    public Vector2 targetX;
    public GameObject lookPoint;
    public bool reverse;
    public void HandleFollowPoint(bool reverse)
    {
        this.reverse = reverse;
    }
    [Range(0,10)]
    public float followSpeed=3;
    void Update()
    {
       
        if (GameManager.instance.deathFinish)
        {
            currentPos = Boss.instance.followPoint.transform.position;
            transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * speed*2);
            Vector3 targetDirection = Boss.instance.lookPoint.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed* followSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
           // Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
           
            return;
        }
        if (stopFollow) return;
        if (target.transform.position.x > targetX.y)
        {
            currentPos = new Vector3(followPointRight.x, transform.position.y, target.transform.position.z);
        }
        else if (target.transform.position.x < targetX.x)
        {
            currentPos = new Vector3(followPointLeft.x, transform.position.y, target.transform.position.z);
        }
        else
        {
            currentPos = new Vector3(followPointMiddle.x, transform.position.y, target.transform.position.z);
        }
        transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * speed);

        if (reverse)
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 180, 0), Time.deltaTime * 2);
        }
        else
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, 0), Time.deltaTime * 2);
        }
        //transform.LookAt(lookPoint.transform);
    }
}
