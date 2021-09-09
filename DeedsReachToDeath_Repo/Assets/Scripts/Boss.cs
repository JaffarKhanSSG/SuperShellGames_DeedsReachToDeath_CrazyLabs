using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public GameObject followPoint, lookPoint;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Animator anim;
    public float delayBtShoot;
    public GameObject prefabShoot;
    float lastShootTime;
    public GameObject alertObj;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isReverse)
        {
            alertObj.SetActive(true);
        }
        else
        {
            alertObj.SetActive(false);
        }
        if (!GameManager.instance.isReverse || GameManager.instance.CurrentPlayer.die)
        {
           // if (GameManager.instance.isReverse)
            {
                if (GameManager.instance.CurrentPlayer.die && GameManager.instance.isReverse)
                {
                    Vector3 playerPos = GameManager.instance.CurrentPlayer.transform.position;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.x, playerPos.y, playerPos.z-3), Time.deltaTime * 2);
                }
            }
          lastShootTime = Time.time;
            return;
        }
        if (!isAttacking && Time.time >= lastShootTime + delayBtShoot && !GameManager.instance.CurrentPlayer.die)
        {
            isAttacking = true; ;
            lastShootTime = Time.time;
            anim.SetTrigger("Attack");
        }
    }
    bool isAttacking;
    public void LoadBullet()
    {
        lastShootTime = Time.time;
        GameObject st = Instantiate(prefabShoot);
        st.transform.SetParent(prefabShoot.transform.parent);
        st.transform.position = prefabShoot.transform.position + new Vector3(0, -0.9f, 3);
        st.gameObject.SetActive(true);
        isAttacking = false;
        DisableHandFireEffect();
    }
    public GameObject handFireEffect,handFireEffect2;
    void EnableHandFireEffect()
    {
        handFireEffect2.SetActive(true);
        handFireEffect.SetActive(true);
    }
    void DisableHandFireEffect()
    {
        handFireEffect2.SetActive(false);
        handFireEffect.SetActive(false);
    }
}
