using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VacuumShaders.CurvedWorld;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Player currentPlayer;
    public Player CurrentPlayer
    {
        get { return currentPlayer; }
    }
    public List<PlayerLifeData> player = new List<PlayerLifeData>();
    // Start is called before the first frame update
    public CurvedWorld_Controller controller; 
    void Start()
    {
        if (SoundManager.instance) SoundManager.instance.StopBG();
        instance = this;
        goodDeeds = maxGoodDeeds;
        SetNextPlayer();
        startingPos = currentPlayer.transform.position;
        if (isReverse)
        {
            goodDeeds -= 19;
        }
        UpdateFillPage();
        SetCameraTo(isReverse);
        StartCoroutine(HandleCurve());

    }
    IEnumerator HandleCurve()
    {
        while (tapToPlayObj.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.01f);
        }
        while (helpPage.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.01f);
        }
        while (controller.leftRightSize < 4)
        {
            controller.leftRightSize += 0.001f;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(Random.Range(10, 20));
        while (controller.leftRightSize > 0)
        {
            controller.leftRightSize -= 0.001f;
            yield return new WaitForEndOfFrame();
        }
        controller.leftRightSize = 0;
        yield return new WaitForSeconds(Random.Range(10, 20));
        while (controller.leftRightSize > -4)
        {
            controller.leftRightSize -= 0.001f;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(Random.Range(10, 20));
        StartCoroutine(HandleCurve());
    }
    public Vector3 startingPos;
    public void PlayerDie()
    {
        camFollow.stopFollow=true;
    }
    public CameraFollow camFollow;
     
    public GameObject effectObj;
    public bool deathFinish;
    public ResultPage resultPage;
    int moneyCollected;
    public void PutEffectOnDeath()
    {
        if (SoundManager.instance) SoundManager.instance.PlayBg(  eClipType.musicBack  );
        effectObj.transform.position = currentPlayer.transform.position;
        effectObj.transform.localEulerAngles = currentPlayer.transform.localEulerAngles;
        effectObj.SetActive(true);
        resultPage.SetGiftAmount((1-fillBarLife.fillAmount)*50,moneyCollected);
        Invoke("SwitchCameraToDeath",isReverse? 1:3);
    }
    public bool isGameStarted;
    public GameObject tapToPlayObj;
    public GameObject pauseButton,pausePage;
    public void ShowPagePage()
    {
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);
        pausePage.SetActive(true);
        Time.timeScale = 0;
        GameData.Vibrate();
    }
    public void ButtonRetryAtPausePage()
    {
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);
        Time.timeScale = 1;
        pausePage.SetActive(false);
        Application.LoadLevel(Application.loadedLevelName);
        GameData.Vibrate();
    }
    public void ButtonHidePausePage()
    {
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);
        pausePage.SetActive(false);
        Time.timeScale = 1;
        GameData.Vibrate();
    }
    int helpId;
    public GameObject healthAndLifeGrp;
    public void OnTapToPlay()
    {
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);
        pauseButton.SetActive(true);
        if (!GameData.IsHelpPageShowned(1))
        {
            helpId = 1;
            ShowHelpPage();
        }
        healthAndLifeGrp.SetActive(true);
        tapToPlayObj.SetActive(false);
        
        for(int i = 0; i < player.Count; i++)
        {
            if(i>1)
            player[i].player.gameObject.SetActive(false);
        }
        GameData.Vibrate();
        if (!helpPage.activeInHierarchy)
        {
            StartGame();
        }
    }
    void StartGame()
    {
        isGameStarted = true;
        if (SoundManager.instance) SoundManager.instance.PlayBg(isReverse ? eClipType.musicBack : eClipType.musicFront);
    }
    void SwitchCameraToDeath()
    {  
        deathFinish = true;
        Invoke("ShowResult", 2);
    }
    void ShowResult()
    {
        if (SoundManager.instance) SoundManager.instance.PlayBg(eClipType.musicFront);
        resultPage.gameObject.SetActive(true);
    }
    public  bool isReverse;
    public void SetCameraTo(bool isReverse)
    {
        this.isReverse = isReverse;
        if (isReverse)
        {
              lastGeneratedCoin = null;
              reverseValue = currentPlayer.transform.position.z;
            if (!GameData.IsHelpPageShowned(3))
            {
                helpId = 3;
                ShowHelpPage();
            }
        }
        else
        {
            startingPos.z += reverseDistance;
            reverseValue = 0;
            reverseDistance = 0;
        }
        if (currentPlayer)
        {
            currentPlayer.HandleFollowPoint();
            CameraFollow.instance.HandleFollowPoint(isReverse);
        }
        if(!tapToPlayObj.activeInHierarchy && !currentPlayer.die)
        if (SoundManager.instance) SoundManager.instance.PlayBg(isReverse ? eClipType.musicBack : eClipType.musicFront);
    }
    public void OnCollectedItem(GameObject obj)
    {
        GameData.Vibrate();
        moneyCollected++;
        //   GameData.SetCoins(GameData.GetCoins() + 1);
        if (!isReverse)
        {
            for (int i = 0; i < collectableData.Count; i++)
            {
                if (collectableData[i].collectablePrefab.name == obj.name)
                {
                    if (!collectableData[i].isPositive)
                    {
                        if (!GameData.IsHelpPageShowned(2))
                        {
                            helpId = 2;
                            ShowHelpPage();
                        }
                        goodDeeds -= 5;
                    }
                    else
                    {
                        goodDeeds++;
                    }
                }

            }
        }
        UpdateFillPage();
        //SetCameraTo(!isReverse);
    }
    void UpdateFillPage()
    {
        if (goodDeeds >= maxGoodDeeds) goodDeeds = maxGoodDeeds;

        if (goodDeeds <= 0)
        {
            isReverse = true;
            SetCameraTo(isReverse);
        }
        if (isReverse)
        {
            if (goodDeeds >= maxGoodDeeds)
            {
                isReverse = false;
                SetCameraTo(false);
            }
        }
        fillBarDeed.fillAmount = (float)goodDeeds / (float)maxGoodDeeds;
    }
    public Image fillBarLife;
    public Image fillBarDeed;

    public PlayerLifeData currentLifeData;
   
    public int goodDeeds;
    public int maxGoodDeeds;
    int counterPlayerLifeData=-1;
    public float maxDistanceToCover;
    public float currentDistance;
    public float reverseValue;
    float reverseDistance;
    float nextDistance;
    float lastReverseTime;
    public void HandleLife()
    {
       
        if (currentLifeData == null) return;
        currentDistance = Vector3.Distance(startingPos, currentPlayer.transform.position);
       
        if (isReverse)
        {
            lastReverseTime = Time.time;
            reverseDistance = Vector3.Distance( new Vector3(startingPos.x, startingPos.y, reverseValue), currentPlayer.transform.position);
           // return;
        }
        fillBarLife.fillAmount =1- (float)((currentDistance - reverseDistance)) / maxDistanceToCover;
        if (fillBarLife.fillAmount <= 0.3f)
        {
            fillBarLife.color = Color.red;
        }
       
        //if(Time.time>=currentLifeData.lifeTime+lastPlayerSetTime)
        if(currentDistance >= nextDistance && Time.time> lastReverseTime+5)
        {
                SetNextPlayer();   
        }
    }
    void SetNextPlayer()
    {
        counterPlayerLifeData++;
        if (counterPlayerLifeData < player.Count)
        {
            currentLifeData = player[counterPlayerLifeData];
            Vector3 lastPosition = player[counterPlayerLifeData].player.transform.position;

            if (currentPlayer != null)
            {
                if (currentPlayer != player[counterPlayerLifeData].player)
                {
                    currentPlayer.gameObject.SetActive(false);
                }
                lastPosition = currentPlayer.transform.position;   
            }
            currentPlayer = player[counterPlayerLifeData].player;
            currentPlayer.transform.position = lastPosition;
            if (counterPlayerLifeData >= 1)
            {
                currentPlayer.SetIsChild(false);
            }
            currentPlayer.gameObject.SetActive(true);
            camFollow.target = currentPlayer.followPoint;
            camFollow.lookPoint = currentPlayer.lookAtPoint;
            nextDistance += player[counterPlayerLifeData].distance;
            SetCameraTo(isReverse);


        }
        else
        {
            currentPlayer.die = true;
        }
    }
    // Update is called once per frame
    public GameObject coinsPrefab;
    public float maxAgeDistance=30;
    void Update()
    {
        if (!isGameStarted) return;
        if (isReverse)
        {
            fillBarDeed.fillAmount += Time.deltaTime / 10;
            if (fillBarDeed.fillAmount >= 1)
            {
                isReverse = false;
                BossEffect[] effect = FindObjectsOfType<BossEffect>();
                for(int i = 0; i < effect.Length; i++)
                {
                    effect[i].gameObject.SetActive(false);
                }
                goodDeeds = maxGoodDeeds;
                UpdateFillPage();
                SetCameraTo(isReverse);
            }
        }
        GenerateCoins(); HandleLife();
    }
    float lastCoinsGeneratedAtTime;
    public float delyInCoinsGeneration;
    public float maxPlayerDistanceToGenerateCoins;
    GameObject lastGeneratedCoin;
    [SerializeField]
    public List<CollectableData> collectableData = new List<CollectableData>();
    public GameObject GetCollectablePrefab()
    {
        if (currentLifeData!=null && currentPlayer)
        {
            eCollectableType rC = currentLifeData.listOfCollectableType[Random.Range(0, currentLifeData.listOfCollectableType.Count)];
            for(int i = 0; i < collectableData.Count; i++)
            {
                if (rC == collectableData[i].collectableType)
                {
                    return collectableData[i].collectablePrefab;
                }
            }
        }
        return collectableData[0].collectablePrefab;
    }
   
    void GenerateCoins()
    {
        if (isReverse) return;
        if(Time.time>=lastCoinsGeneratedAtTime+ delyInCoinsGeneration)
        {
            float dis = 0;
            if (lastGeneratedCoin != null)
            {
                  dis = Vector3.Distance(lastGeneratedCoin.transform.position, currentPlayer.transform.position);
            }
         
            if (dis<=maxPlayerDistanceToGenerateCoins)
            {
                lastCoinsGeneratedAtTime = Time.time;
                Vector3 nextPos = Vector3.zero;
                if (!lastGeneratedCoin)
                {
                    nextPos.z = currentPlayer.transform.position.z;
                }
                else
                {
                    nextPos.z = lastGeneratedCoin.transform.position.z;
                    
                }
                nextPos += new Vector3(Random.Range(-1.5f,1.5f), 0, 5);
                GameObject expected = GetCollectablePrefab();
               
                lastGeneratedCoin = Instantiate(expected);
                lastGeneratedCoin.name = expected.name;
                lastGeneratedCoin.transform.position = nextPos;
                lastGeneratedCoin.SetActive(true);
            }
        }
    }
    public Set currentSet;
    public void OutOfScreen(Set set)
    {
        currentSet.endPoint.transform.SetParent(null);
        set.transform.position = currentSet.endPoint.transform.position;
        currentSet.endPoint.transform.SetParent(currentSet.transform);
        currentSet = set;
    }
    [Header("Help")]
    public GameObject helpPage;
    public void CloseHelpPage()
    {
        if (SoundManager.instance) SoundManager.instance.PlayClip(eClipType.click);
        if (helpId == 1)
        {
            if (SoundManager.instance) SoundManager.instance.PlayBg(isReverse ? eClipType.musicBack : eClipType.musicFront);
            isGameStarted = true;
        }
        else
        {
            Time.timeScale = 1;
        }
        GameData.SetIsHelpPageShowned(helpId,"true");
        helpPage.SetActive(false);
    }
    public List<GameObject> listHelpContent;
    public void ShowHelpPage()
    {
        for(int i=0;i< listHelpContent.Count; i++)
        {
            listHelpContent[i].SetActive(false);
        }
        if (helpId < listHelpContent.Count)
        {
            listHelpContent[helpId].SetActive(true);
        }
        if(helpId!=1)
        {
            Time.timeScale = 0;
        }
        helpPage.SetActive(true);
    }
}
public enum eCollectableType
{
    milkBottle=1,toy=2,baseballBat=3,ball=4,wine=5,money=6, chocolate=7,ballonRed=8,ballonBlue=9
}
[System.Serializable]
public class CollectableData
{
    public eCollectableType collectableType;
    public GameObject collectablePrefab;
    public bool isPositive = true;
}
[System.Serializable]
public class PlayerLifeData
{
    public Player player;
  //  public float lifeTime;
    public float distance;
    public List<eCollectableType> listOfCollectableType = new List<eCollectableType>();
}