using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 모든 배달이 완료되면 다음 스테이지로 넘어간다.
// 모든 배달이 완료되지 않고, Timer의 시간이 0이하가 되면 게임이 종료된다.
// 각 스테이지에 있는 집들 중 스테이지의 수에 맞게 고객을 위치시킨다.
// 피자집에는 고객의 수에 맞는 피자를 만든다.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text stageTxt;

    Timer timer;
    [SerializeField] private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreTxt.text = score.ToString();
        }
    }
    [SerializeField] int stage = 0;
    public int Stage
    {
        get{ return stage; }
        set 
        { 
            stage = value;
            stageTxt.text = $"Stage {stage}";
        }
    }
    [SerializeField] GameObject[] stages;
    [SerializeField] List<Transform> customers = new List<Transform>();
    [SerializeField] GameObject customer;
    [SerializeField] GameObject pizza;
    [SerializeField] Transform pizzaSpawnPos;

    [SerializeField] List<Transform> totalHouses = new List<Transform>();
    [SerializeField] List<Transform> housesToDrive = new List<Transform>();
    [SerializeField] List<Transform> stageItems = new List<Transform>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        Score = Stage = 0;
        ResetGame();
    }

    void ResetGame()
    {
        FindHousesInStage(stage);
        SetRandomHousesToDrive();
        CreateCustomers();
        CreatePizzas();
    }

    private void Update()
    {
        if (timer == null)
            return;
    }

    // 모든 배달이 완료되지 않고, Timer의 시간이 0이하가 되면 게임이 종료된다.
    public void CheckEndGame()
    {
        if (customers == null) return;

        if(customers.Count != Score && timer.isUpdated)
        {
            Debug.Log("End Game");
            Time.timeScale = 0f;
            timer.isUpdated = false;
        }
    }

    // 모든 배달이 완료되면 다음 스테이지로 넘어간다.
    public void CheckNextStage()
    {
        if (customers == null) return;

        Debug.Log($"Customer: {customers.Count}, Score: {Score}");

        if (customers.Count == Score)
        {
            Debug.Log("Error: Next Stage");
            GoNextStage();
        }
    }

    private void GoNextStage()
    {
        stages[Stage].SetActive(false);

        Stage++;

        stages[Stage].SetActive(true);

        ResetGame();
    }

    // 각 스테이지에 있는 집을 찾는다.
    void FindHousesInStage(int stageNumber)
    {
        if (stages[stageNumber] == null)
        {
            Debug.Log("Error: No Stages");
            return;
        }

        if (totalHouses.Count > 0)
            totalHouses.Clear();

        Transform houses = stages[stageNumber].transform.Find("Houses");
        for(int i = 0; i< houses.childCount; i++)
        {
            totalHouses.Add(houses.GetChild(i));
        }
    }

    // 각 스테이지에 있는 집들 중 스테이지의 수에 맞게 고객을 위치시킨다.
    void SetRandomHousesToDrive()
    {
        // stage0 -> 고객 1명
        // stage1 -> 고객 2명
        // stage2 -> 고객 3명 ....
        switch(stage)
        {
            case 0:
                for(int i = 0; i < 1; i++)
                {
                    Util.Shuffle(totalHouses);
                    housesToDrive.Add(totalHouses[i]);
                }
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    Util.Shuffle(totalHouses);
                    housesToDrive.Add(totalHouses[i]);
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    Util.Shuffle(totalHouses);
                    housesToDrive.Add(totalHouses[i]);
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    Util.Shuffle(totalHouses);
                    housesToDrive.Add(totalHouses[i]);
                }
                break;
        }
    }

    private void CreateCustomers()
    {
        foreach(var house in housesToDrive)
        {
            Transform customerOBJ = Instantiate(customer.transform);
            customerOBJ.position = GetRandomPosByCol(house);
            customers.Add(customerOBJ);
        }
    }

    void CreatePizzas()
    {
        foreach(Transform house in housesToDrive)
        {
            GameObject pizzaGO = Instantiate(pizza);

            int randX = Random.Range(0, 2);
            int randY = Random.Range(0, 2);
            pizzaGO.transform.position = pizzaSpawnPos.position + new Vector3(randX, randY, 0);
        }
    }

    private Vector3 GetRandomPosByCol(Transform house)
    {
        if (house == null)
        {
            Debug.Log("Error: No Houses");
            return Vector3.zero;
        }

        float rectWidth = house.transform.GetComponent<BoxCollider2D>().size.x;
        float rectHeight = house.transform.GetComponent<BoxCollider2D>().size.y;

        int quadrant = Random.Range(0, 4);
        float randX = 0;
        float randY = 0;
        switch (quadrant)
        {
            case 0:
                // 1quadrant
                randX = Random.Range(rectWidth * 0.5f, rectWidth);
                randY = Random.Range(rectHeight * 0.5f, rectHeight);
                break;
            case 1:
                // 2quadrant
                randX = Random.Range(-rectWidth, -rectWidth * 0.5f);
                randY = Random.Range(rectHeight * 0.5f, rectHeight);
                break;
            case 2:
                // 3quadrant
                randX = Random.Range(rectWidth * 0.5f, rectWidth);
                randY = Random.Range(-rectHeight, -rectHeight * 0.5f);
                break;
            case 3:
                // 4quadrant
                randX = Random.Range(-rectWidth, -rectWidth * 0.5f);
                randY = Random.Range(-rectHeight, -rectHeight * 0.5f);
                break;
        }

        Vector3 newPos = house.transform.position + new Vector3(randX, randY);
        return newPos;
    }
}
