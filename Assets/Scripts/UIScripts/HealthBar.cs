using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthbar;
    public TextMeshProUGUI healthText;
    void Start()
    {
        healthbar = GetComponent<Slider>();
    }

    void Update()
    {
        
    }

    public void SetValue(int newValue){
        healthbar.value = newValue;
        healthText.text = newValue +"/"+100;
    }
}
