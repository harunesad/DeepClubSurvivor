using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText, timeText;
    [SerializeField] float time;
    [SerializeField] GameManager gameManager;
    [SerializeField] CanvasGroup interact;
    float filAmount;
    public Image superBar, staminaBar;
    bool superAmount, staminaAmount;
    void Start()
    {
        time = (DataSave.Instance.difficulty + 1) * 100;
        coinText.text = DataSave.Instance.coinCount.ToString();
    }
    void Update()
    {
        if (time > 0)
        {
            timeText.text = (int)(time / 60) + " : " + (int)(time % 60);
            time -= Time.deltaTime;
        }
        else
        {
            Time.timeScale = 0;
            StartCoroutine(SceneLaod());
        }
    }
    IEnumerator SceneLaod()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }
    public void SuperBar()
    {
        if (!superAmount)
        {
            filAmount += Time.deltaTime / 25;
            superBar.fillAmount = filAmount;
        }
        if (superBar.fillAmount == 1)
        {
            interact.alpha = 1;
            interact.GetComponentInChildren<TextMeshProUGUI>().text = "Power";
            superAmount = true;
        }
    }
    public void SuperBarReset()
    {
        superBar.DOFillAmount(0, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            filAmount = 0;
            interact.alpha = 0;
            superAmount = false;
        });
    }
    public bool StaminaUpdate(float value)
    {
        if (staminaBar.fillAmount != 0 && !staminaAmount)
        {
            if (value == 1)
            {
                staminaBar.fillAmount -= Time.deltaTime / 25;
            }
            else
            {
                staminaBar.fillAmount += Time.deltaTime / 10 ;
            }
        }
        else if (staminaBar.fillAmount == 0)
        {
            gameManager.player.GetComponent<Animator>().SetBool("Walk", false);
            staminaAmount = true;
            staminaBar.DOFillAmount(1, 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                staminaAmount = false;
            });
        }
        return !staminaAmount;
    }
}
