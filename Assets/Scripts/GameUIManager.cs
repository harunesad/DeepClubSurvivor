using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    void Start()
    {
        coinText.text = DataSave.Instance.coinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
