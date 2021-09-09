using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultPage : MonoBehaviour
{
    public static ResultPage instance;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateCurrencyText()
    {
        textCoins.text = GameData.GetCoins() + "";
    }
    public void UpdateCurrencyTextAs(string val)
    {
        textCoins.text = val + "";
    }
    // Start is called before the first frame update
    float lastFillAmount;
    
    void Start()
    {
        lastFillAmount = PlayerPrefs.GetFloat("GiftValue", 0);
        textCoins.text = GameData.GetCoins() + "";
         
    }
    int money=100;
    public void SetGiftAmount(float percentageValue,int money)
    {
        this.percentage = percentageValue;
        this.money = money;
        textBonusMoney.text = (2 * money)+"";
        textDefaultMoney.text = money + "";
        if (money < 100)
        {
            textDefaultMoney.text ="0"+ money;
        }
        if (2 * money < 100)
        {
            textBonusMoney.text = "0" + (2 * money);
        }

    }
    public CurrencyAnimation currencyAnimation;
    public GameObject claim2XStartPoint, claim1XStartPoint;
    string claimType;
    public void ButtonClaim(int val)
    {
        if (!string.IsNullOrEmpty(claimType))
        {
            return;
        }
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);



        claimType = val == 1 ? "1X" : "2X";
        currencyAnimation.StartAnimation(money * val,val==1? claim1XStartPoint  : claim2XStartPoint,false,1);

    }
    public GiftRevealPage revealPage;
    public void FinishedCurrencyAnimation()
    {
        if (!string.IsNullOrEmpty(claimType))
        {
            gameObject.SetActive(false);
            claimType = "";
            if (PlayerPrefs.GetFloat("GiftValue") >= 1)
            {
                //Give Now
                revealPage.gameObject.SetActive(true);
            }
            else
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }
    public void FinishedCurrencyAnimationByDelay()
    {
        Invoke("FinishedCurrencyAnimation", 1);   
    }
    public Image giftbar;
    public  float progressiveValue;
    public  float currentVal;
    float percentage = 20f;
    bool isFinishedGiftBar;
    public Text textBarPercentage,textCoins,textDefaultMoney,textBonusMoney;
    // Update is called once per frame
    void Update()
    {
        if (!isFinishedGiftBar)
        {
            if (progressiveValue < percentage)
            {
                progressiveValue += (percentage) * (Time.deltaTime / 2f);
            }
            else
            {
                progressiveValue = percentage;
                isFinishedGiftBar = true;
                PlayerPrefs.SetFloat("GiftValue", lastFillAmount+(progressiveValue/100f));
               
            }

           

            giftbar.fillAmount = lastFillAmount + (progressiveValue / 100f);
            textBarPercentage.text = (int)(giftbar.fillAmount * 100f) + "%";

        }

    }
}
