using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    public GameObject gameOverPanel;

    public static UIManager Instance;

    private void Awake() => Instance = this;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        text.text = "Score:" + PlayerManager.Instance.score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
