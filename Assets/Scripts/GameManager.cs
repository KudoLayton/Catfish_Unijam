using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public struct FishType
    {
        string color;
        int score;
        float probability;
        public FishType(string color, int score, float probability)
        {
            this.color = color;
            this.score = score;
            this.probability = probability;
        }

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
    public struct GameSetting
    {
        public int time;
        public FishType[] fishtypes;

        public GameSetting(int time, FishType[] fishtypes)
        {
            this.time = time;
            this.fishtypes = fishtypes;
        }
    }
    
    [SerializeField] GameSetting currentGameSet;
    [SerializeField] int fishSlotNum;
    [SerializeField] int catSlotNum;
    [SerializeField] int fishInitNum;
    [SerializeField] int catInitNum;
    [SerializeField] float fishCoolTime;
    [SerializeField] float catCoolTime;
    GameObject[] fishSlot;
    GameObject[] catSlot;

    int score;

    long fishTick, catTick;
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
        fishTick = fishMaxTick + 1;
        catTick = catMaxTick + 1;
    }
    
    
    //게임 오버 시 호출할 메소드입니다.
    public void GameOver()
    {
        // 엔딩 연출
        // Hunt Count 전송 (score도 대조용으로 전송)
    }

    
    // 물고기 방생 시 호출할 메소드입니다.
    public GameObject LeaveFishSlot(int n)
    {
        GameObject leaveFish = fishSlot[n];
        fishSlot[n] = null;
        return leaveFish;
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
        Object.Destroy(fish);
        fish = null;
        Object.Destroy(cat);
        cat = null;
    }

    
    // n번 Fish Slot에 물고기를 새로 채웁니다. 확률에 기반합니다. 생성에 실패한 경우 -1을 return합니다.
    private int AddFishSlot(int n)
    {
        return 0;
    }

    
    // n번 Fish Slot에 물고기가 있는지 확인합니다. 물고기가 있다면 true를 return합니다.
    private bool GetFishSlot(int n)
    {
        return fishSlot[n] ? true : false;
    }

    
    // n번 Cat Slot에 고양이를 새로 채웁니다. 생성에 실패한 경우 -1을 return합니다.
    private int AddCatSlot(int n)
    {
        return 0;
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
    }
}
