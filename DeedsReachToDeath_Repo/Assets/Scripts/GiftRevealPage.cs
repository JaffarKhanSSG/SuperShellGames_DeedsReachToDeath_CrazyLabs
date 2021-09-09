using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftRevealPage : MonoBehaviour
{
    public static GiftRevealPage instance;
    public Text textCoins;
    // Start is called before the first frame update
    int moneyGiftValue;
    void Start()
    {
        instance = this;
        moneyGiftValue = GameData.GetLevel()*500;
        textMoneyGifted.text = moneyGiftValue + "";
        UpdateCurrencyText();

    }
    public void UpdateCurrencyTextAs(string val)
    {
        textCoins.text = val + "";
    }
    public void FinishedCurrencyAnimationByDelay()
    {
        Invoke("FinishedCurrencyAnimation", 1);
    }
    public void FinishedCurrencyAnimation()
    {
        
            gameObject.SetActive(false);
            PlayerPrefs.SetFloat("GiftValue", 0);
            GameData.SetLevel(GameData.GetLevel() + 1);
            Application.LoadLevel(Application.loadedLevelName);
       
    }
    public void UpdateCurrencyText()
    {
        textCoins.text = GameData.GetCoins() + "";
    }
    public Text textMoneyGifted;
    public GameObject unRevealObj, revealedObj;
    public CurrencyAnimation currencyAnimation;
    public GameObject startFrom;
    public GameObject buttonReveal;
    public void ButtonReveal()
    {
        unRevealObj.SetActive(false);
        revealedObj.SetActive(true);
        buttonReveal.SetActive(false);
        Invoke("StartAnimationSequence", 1);
    }
    void StartAnimationSequence()
    {
        revealedObj.SetActive(false);
        currencyAnimation.StartAnimation(moneyGiftValue, startFrom, true,1.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
