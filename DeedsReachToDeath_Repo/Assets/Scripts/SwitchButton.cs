using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckSwitch();
    }
    public string prefName;
    public GameObject redLine;
    void CheckSwitch()
    {
        if (GameData.IsSwitchOn(prefName))
        {
            redLine.gameObject.SetActive(false);
        }
        else
        {
            redLine.gameObject.SetActive(true);
        }
        if(SoundManager.instance && prefName=="Sound") SoundManager.instance.CheckMusic();

    }
    public void SwitchIt()
    {
        if (GameData.IsSwitchOn(prefName))
        {
            GameData.SetSwitchOnTo(prefName, "OFF");
        }
        else
        {
            GameData.SetSwitchOnTo(prefName, "ON");
        }
        CheckSwitch();
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
