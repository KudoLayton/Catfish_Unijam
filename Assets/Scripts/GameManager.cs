using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    [System.Serializable]
    public struct FishType
    {
        public string color;
        public int score;
        public float probability;
        public GameObject prefab;

        public string GetColor()
        {
            return this.color;
        }

        public int GetScore()
        {
            return this.score;
        }

        public float GetProbability()
        {
            return this.probability;
        }
    }
    [System.Serializable]
    public struct GameSetting
    {
        public int time;
        public FishType[] fishtypes;

        public GameSetting(int time, FishType[] fishtypes)
        {
            this.time = time;
            this.fishtypes = fishtypes;
        }

        public GameObject GetPrefab(string name)
        {
            foreach (FishType ftype in fishtypes)
            {
                if (name == ftype.GetColor())
                {
                    return ftype.prefab;
                }
            }

            return null;
        }
    }
    int fishSlotNum = 6;
    int catSlotNum = 6;
    [SerializeField] int fishInitNum = 3;
    [SerializeField] int catInitNum = 4;
    [SerializeField] float fishCoolTime = 2.0f;
    [SerializeField] float catCoolTime = 3.0f;
    [SerializeField] GameSetting currentGameSet;
    [SerializeField] GameObject[] catPrefabs;

    bool[] fishSlot;
    bool[] catSlot;
    int score;
    
    long gameTick;
    long fishTick, catTick;
    long gameMaxTick;
    long fishMaxTick, catMaxTick;
    int[] fishHuntCount;

    // 0 ~ (size - 1) 사이의 난수열을 반환합니다. 1번씩 등장합니다.
    public int[] randomArray(int size)
    {
        int[] x = new int[size];
        for (int i = 0; i < size; i++) {
            x[i] = i;
        }
        for (int i = 0; i < size * size; i++) {
            int temp = x[i % size];
            int randId = Random.Range(0, size);
            x[i % size] = x[randId];
            x[randId] = temp;
        }
        return x;
    }
    
    
    // 게임이 시작되면 호출할 메소드입니다.
    public void GameStart()
    {
        int[] fishIds = randomArray(fishSlotNum);
        int[] catIds = randomArray(catSlotNum);
        for (int i = 0; i < fishInitNum; i++) {
            AddFishSlot(fishIds[i]);
        }
        for (int i = 0; i < catInitNum; i++) {
            AddCatSlot(catIds[i]);
        }
        score = 0;
        fishMaxTick = (long)(fishCoolTime / (Time.fixedDeltaTime));
        catMaxTick = (long)(catCoolTime / (Time.fixedDeltaTime));
        gameMaxTick = (long)(currentGameSet.time / (Time.fixedDeltaTime));
        fishTick = fishMaxTick + 1;
        catTick = catMaxTick + 1;
        gameTick = 0;
    }
    
    
    //게임 오버 시 호출할 메소드입니다.
    public void GameOver()
    {
        // 엔딩 연출
        // Hunt Count 전송 (score도 대조용으로 전송)
    }

    
    // Fish가 Cat을 잡았을 경우, 외부에 의해 호출됩니다.
    public void CatchCat(GameObject fish, GameObject cat)
    {
        string fishColor = fish.GetComponent<FishBehavior>().color;
        int addscore;
        foreach (FishType fishtype in currentGameSet.fishtypes)
        {
            if (fishColor == fishtype.GetColor())
            {
                addscore = fishtype.GetScore();
                score += addscore;
                break;
            }
        }

        // 고양이, 물고기 붙고
        // 애니메이션 : 날려버리기
        // 빈 자리에 짤방
    }

    public void DeleteFishSlot(int n) {
        fishSlot[n] = false;
    }

    public void DeleteCatSlot(int n) {
        catSlot[n] = false;
    }
    
    // n번 Fish Slot에 물고기를 새로 채웁니다. 확률에 기반합니다.
    private void AddFishSlot(int n)
    {
        Vector3 genPosition = new Vector3(-4.5f + 1.8f * n, 3.75f, 12.0f);
        float sumProb = 0.0f;
        float rand = Random.Range(0.0f, 1.0f);
        string genColor = "";
        foreach (FishType fishdefs in currentGameSet.fishtypes)
        {
            if (sumProb <= rand && rand <= sumProb + fishdefs.GetProbability())
            { //probability hits
                genColor = fishdefs.GetColor();
                break;
            }
            else
            {
                sumProb += fishdefs.GetProbability();
            }
        }
        GameObject genFish = Instantiate(currentGameSet.GetPrefab(genColor), genPosition, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        genFish.GetComponent<FishBehavior>().SetColor(genColor);
        genFish.GetComponent<FishBehavior>().SetSlot(n);
        genFish.GetComponent<FishBehavior>().EnterJellyfish();
        fishSlot[n] = true;
    }

    
    // n번 Fish Slot에 물고기가 있는지 확인합니다. 물고기가 있다면 true를 return합니다.
    private bool GetFishSlot(int n)
    {
        return fishSlot[n] ? true : false;
    }

    
    // n번 Cat Slot에 고양이를 새로 채웁니다.
    private void AddCatSlot(int n)
    {
        Vector3 genPosition = new Vector3(-4.5f + 1.8f * n, 3.75f, 12.0f);
        float sumProb = 0.0f;
        int m = catPrefabs.Length;
        float rand = Random.Range(0.0f, 1.0f);
        int i = 0;
        for (; i < m; i++)
        {
            if (sumProb <= rand && rand <= sumProb + 1.0f / m)
            { //probability hits
                break;
            }
            else
            {
                sumProb += 1.0f / m;
            }
        }
        GameObject genFish = Instantiate(catPrefabs[i], genPosition, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        catSlot[n] = true;
    }

    
    // n번 Cat Slot에 고양이가 있는지 확인합니다. 고양이가 있다면 true를 return합니다.
    private bool GetCatSlot(int n)
    {
        return catSlot[n] ? true : false;
    }

    
    // Fish Slot이 꽉 찼는지 (최대 수량에 도달했는지) 확인합니다.
    private bool IsFullFishSlot()
    {
        int numFill = 0;

        for (int i = 0; i < fishSlotNum; i++) {
            if (GetFishSlot(i)) numFill++;
        }

        return numFill == fishSlotNum ? true : false;
    }

    
    // Cat Slot이 꽉 찼는지 (최대 수량에 도달했는지) 확인합니다.
    private bool IsFullCatSlot()
    {
        int numFill = 0;

        for (int i = 0; i < catSlotNum; i++) {
            if (GetCatSlot(i)) numFill++;
        }

        return numFill == catSlotNum ? true : false;
    }

    
    // 빈 임의의 자리에 물고기를 채웁니다. 생성에 실패한 경우 -1을 return합니다.
    private int fillFish()
    {
        int[] randId = randomArray(fishSlotNum);
        for (int i = 0; i < fishSlotNum; i++)
        {
            if (!GetFishSlot(randId[i])) // slot is empty
            {
                AddFishSlot(randId[i]);
                return 0;
            }
        }
        return -1;
    }
    
    
    // 빈 임의의 자리에 고양이를 채웁니다. 생성에 실패한 경우 -1을 return합니다.
    private int fillCat()
    {
        int[] randId = randomArray(catSlotNum);
        for (int i = 0; i < catSlotNum; i++)
        {
            if (!GetCatSlot(randId[i])) // slot is empty
            {
                AddCatSlot(randId[i]);
                return 0;
            }
        }
        return -1;
    }

    void start() {
        GameStart();
    }

    // Update
    void FixedUpdate()
    {
        if (fishTick <= fishMaxTick)
        {
            if (fishTick == fishMaxTick) {
                fillFish();
                if (!IsFullFishSlot()) {
                    fishTick = -1;
                }
            }
            fishTick++;
        }
        if (catTick <= catMaxTick)
        {
            if (catTick == catMaxTick) {
                fillCat();
                if (!IsFullCatSlot()) {
                    catTick = -1;
                }
            }
            catTick++;
        }
        if (gameTick == gameMaxTick) {
            GameOver();
        } else {
            gameTick++;
        }
    }
}
