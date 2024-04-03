using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button play, settings, characterChoose;
    [SerializeField] Button mainHome;
    [SerializeField] CanvasGroup characterWindow, settingsWindow, entryWindow;
    [SerializeField] Sprite select, unselect;
    [SerializeField] List<Button> characters;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TMP_Dropdown difficulty;
    Image choosen;
    void Start()
    {
        coinText.text = DataSave.Instance.coinCount.ToString();
        mainHome.onClick.AddListener(MainHome);
        characterChoose.onClick.AddListener(CharacterWindow);
        settings.onClick.AddListener(SettingsOpen);
        play.onClick.AddListener(PlayGame);
        for (int i = 0; i < characters.Count; i++)
        {
            int j = i;
            characters[i].onClick.AddListener(delegate { CharacterSelect(characters[j].transform.parent.GetComponent<Image>(), j); });
        }
    }
    void MainHome()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    void CharacterWindow()
    {
        UITransition(entryWindow, characterWindow);
    }
    void SettingsOpen()
    {
        UITransition(entryWindow, settingsWindow);
        entryWindow.blocksRaycasts = false;
    }
    void CharacterSelect(Image choosenImage, int index)
    {
        if (choosen != null && choosen != choosenImage)
        {
            choosen.sprite = unselect;
            choosen = choosenImage;
            choosen.sprite = select;
        }
        else if (choosen == null)
        {
            choosen = choosenImage;
            choosen.sprite = select;
        }
        DataSave.Instance.CharacterSelect(index);
    }
    void PlayGame()
    {
        characterWindow.blocksRaycasts = false;
        characterWindow.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            DataSave.Instance.CoinUpdate(int.Parse(coinText.text));
            DataSave.Instance.DifficultySelect(difficulty.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
    public void MainSoundUpdate(Slider slider)
    {
        DataSave.Instance.SoundUpdate(-1, slider.value);
    }
    public void EffectSoundUpdate(Slider slider)
    {
        DataSave.Instance.SoundUpdate(slider.value, -1);
    }
    public void HomeOpen(CanvasGroup canvasGroup)
    {
        UITransition(canvasGroup, entryWindow);
    }
    void UITransition(CanvasGroup close, CanvasGroup open)
    {
        close.blocksRaycasts = false;
        close.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            open.DOFade(1, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                open.blocksRaycasts = true;
            });
        });
    }
}