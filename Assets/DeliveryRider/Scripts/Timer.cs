using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float totalTime;
    [SerializeField] float finishTime = 30f;
    [SerializeField] float nowSeconsToFinish;
    [SerializeField] TMP_Text timerTxt;
    public bool isUpdated = false;
    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        nowSeconsToFinish = finishTime - totalTime;

        if(nowSeconsToFinish < 0)
        {
            Debug.Log("Game Over");
            
            isUpdated = true;
            GameManager.Instance.CheckEndGame();
            totalTime = 0;
        }
        //timerTxt.text = nowSeconsToFinish.ToString("N2");
        //timerTxt.text = Math.Round(nowSeconsToFinish, 2).ToString();
        timerTxt.text = String.Format("{0:N2}", nowSeconsToFinish);
    }
}
