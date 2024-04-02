using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.AI;

public class UIManager : MonoBehaviour
{
    public Button play, settings, character;
    public Button leave, home, playGame;
    public TextMeshProUGUI killInfo, deathInfo, killCountText, win, time, coin;
    public GameObject killImage, damagePopup, settingsPanel;
    public Image gameBeforeLoading, gameLoading;
    [SerializeField] GameObject canvas;
    [SerializeField] Image gameBackground, charactersBackground, settingsBackgorund;
    public Image waitBackground;
    [SerializeField] AudioClip click, loadingSound;
    public AudioSource chooseSource, loadingSource, birdSource;
    [SerializeField] Slider effectsound, mainSound;
    public Sprite winSprite, loseSprite;
    void Start()
    {

    }
}
    
    