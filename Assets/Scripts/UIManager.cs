using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        text.text = "Score:" + PlayerManager.Instance.score;
    }
}
