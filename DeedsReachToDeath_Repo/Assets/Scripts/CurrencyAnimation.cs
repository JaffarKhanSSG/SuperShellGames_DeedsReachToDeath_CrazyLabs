using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> listOfCurrencyImage;
    public GameObject pointToGo;
    public GameObject startFrom;
    void Start()
    {
      //  StartCoroutine(StartAnimation()); 
    }
    int expectedVal;
    float expectedPerValue;
    public bool randomPlace;
    float scaleValue=1;
    public void StartAnimation(int moneyValue,GameObject startFrom,bool randomPlace,float scaleVal)
    {
        gameObject.SetActive(true);
        this.randomPlace = randomPlace;
        this.scaleValue = scaleValue;
        this.startFrom = startFrom;
        expectedVal = moneyValue;
        expectedPerValue = (float)moneyValue / (float)listOfCurrencyImage.Count;
        StartCoroutine(StartAnimation());
    }
    IEnumerator StartAnimation()
    {
        
        for (int i = 0; i < listOfCurrencyImage.Count; i++)
        {
            if (this.randomPlace)
            {
                listOfCurrencyImage[i].transform.position = startFrom.transform.position + new Vector3(Random.Range(-5, 20), Random.Range(-5, 20), 0);
            }
            else
            {
                listOfCurrencyImage[i].transform.position = startFrom.transform.position;

            }
            listOfCurrencyImage[i].transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);
            listOfCurrencyImage[i].gameObject.SetActive(true);
            iTween.MoveTo(listOfCurrencyImage[i].gameObject, iTween.Hash("x", pointToGo.transform.position.x, "y", pointToGo.transform.position.y, "islocal", false, "time", 0.5f, "delay", randomPlace?1:0, "easetype", iTween.EaseType.linear , "onComplete", "AnimateToUP", "oncompletetarget", gameObject));
            if (i == listOfCurrencyImage.Count-1)
            {
                iTween.MoveTo(listOfCurrencyImage[i].gameObject, iTween.Hash("x", pointToGo.transform.position.x, "y", pointToGo.transform.position.y, "islocal", false, "time", 0.5f, "delay", randomPlace ? 1 : 0, "easetype", iTween.EaseType.linear, "onComplete", "FinishedCoinsAnimation", "oncompletetarget", gameObject));

            }
            yield return new WaitForEndOfFrame();
        }
        
    }
    public void FinishedCoinsAnimation()
    {
        GameData.SetCoins(GameData.GetCoins() + expectedVal);
        if (ResultPage.instance && ResultPage.instance.gameObject.activeInHierarchy) {  
        ResultPage.instance.UpdateCurrencyText();
        ResultPage.instance.FinishedCurrencyAnimationByDelay();
        }
        if (GiftRevealPage.instance && GiftRevealPage.instance.gameObject.activeInHierarchy)
        {      
      
            GiftRevealPage.instance.UpdateCurrencyText();
            GiftRevealPage.instance.FinishedCurrencyAnimationByDelay();
        }
        gameObject.SetActive(false);
        for(int i = 0; i < listOfCurrencyImage.Count; i++)
        {
            listOfCurrencyImage[i].gameObject.SetActive(false);
        }
    }
    public void AnimateToUP()
    {
        if(ResultPage.instance)
        ResultPage.instance.UpdateCurrencyTextAs(GameData.GetCoins()+(int)expectedPerValue+"");
        if (GiftRevealPage.instance)
        {
            GiftRevealPage.instance.UpdateCurrencyTextAs(GameData.GetCoins() + (int)expectedPerValue + "");
        }
        if(SoundManager.instance)
        {
            SoundManager.instance.PlayClip(eClipType.coins);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
