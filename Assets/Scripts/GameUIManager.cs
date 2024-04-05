using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText, timeText;
    [SerializeField] float time;
    float filAmount;
    public Image superBar;
    bool barAmount;
    void Start()
    {
        time = (DataSave.Instance.difficulty + 1) * 100;
        coinText.text = DataSave.Instance.coinCount.ToString();
    }
    void Update()
    {
        timeText.text = (int)(time / 60) + " : " + (int)(time % 60);
        time -= Time.deltaTime;
    }
    public void SuperBar()
    {
        if (!barAmount)
        {
            filAmount += Time.deltaTime / 25;
            superBar.fillAmount = filAmount;
        }
        if (superBar.fillAmount == 1)
        {
            barAmount = true;
        }
    }
    public void SuperBarReset()
    {
        superBar.DOFillAmount(0, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            filAmount = 0;
            barAmount = false;
        });
    }
}
