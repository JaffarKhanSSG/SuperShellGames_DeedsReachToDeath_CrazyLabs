using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerFeatureScriptable playerFeature;
    void Start()
    {
        
    }
    public GameObject followPoint,lookAtPoint;
    public Animator anim;
    public Vector2 boundaryX;
    Vector3 currentPos;
 
   
    // Update is called once per frame
    public bool die;
    public void SetIsChild(bool val)
    {
        anim.SetBool("IsChild", val);
    } 
    void Update()
    {
        if (!GameManager.instance.isGameStarted)
        {

            return;
        }
        if (die)
        {
            GameManager.instance.PlayerDie();
            transform.localEulerAngles = new Vector3(0,180,0);
            if (!anim.GetBool("Die"))
            {
                anim.SetBool("Die",true);

            }
            return;
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = transform.position + Vector3.right * ((Input.GetAxis("Horizontal")) + Input.GetAxisRaw("Mouse X")) * playerFeature.touchSensitivity*(GameManager.instance.isReverse?-1:1);

            if (currentPos.x >= boundaryX.y)
            {
                currentPos.x = boundaryX.y;
            }
            else if (currentPos.x <= boundaryX.x)
            {
                currentPos.x = boundaryX.x;
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentPos.x, transform.position.y, transform.position.z), Time.deltaTime * playerFeature.leftRightSpeed);
        }
        transform.Translate(Vector3.forward * Time.deltaTime *  (playerFeature.forwardSpeed*(GameManager.instance.isReverse ?2:1)));
        
        anim.SetFloat("MoveSpeed", GameManager.instance.isReverse?1:0.3333333f);
        
    }
    public void PutEffectOnDeath()
    {
        GameManager.instance.PutEffectOnDeath();
    }
    [Header("Camera Follow Offset")]
    [Range(-10, 0)]
    public float offsetCameraForward;
    [Range(0, 10)]
    public float offsetCameraBackward;
    
    public void HandleFollowPoint()
    {

        iTween.MoveTo(followPoint.gameObject, iTween.Hash("z", GameManager.instance.isReverse ? offsetCameraBackward : offsetCameraForward, "islocal", true, "time", 0.2, "delay", 0, "easetype",iTween.EaseType.linear));// "onComplete", "AnimateToUP", "oncompletetarget", gameObject));
         

    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Collectable")
        {
            other.gameObject.SetActive(false);
            GameManager.instance.OnCollectedItem(other.gameObject);
        }
        if (other.tag == "Bullet" && !die)
        {
            other.gameObject.SetActive(false);
            die = true;

        }
    }
}
