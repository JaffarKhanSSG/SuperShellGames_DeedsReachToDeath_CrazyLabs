using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shineLine;
    Vector3 startPos;
    void Start()
    {
        startPos = shineLine.transform.position;
    }
    public float dist;
    // Update is called once per frame
    bool isLoaded;
    public float shineSpeedInSec=20;
    public RectTransform shineRect;
    void Update()
    {
        if (isLoaded) return;
        dist = Vector3.Distance(startPos, shineLine.transform.position);
        shineLine.transform.Translate(Vector3.right *(720 * (Time.deltaTime / shineSpeedInSec)));
        if (shineRect.anchoredPosition.x > 380)
        {
            isLoaded = true;
            Application.LoadLevel("Game");
        }
    }
}
