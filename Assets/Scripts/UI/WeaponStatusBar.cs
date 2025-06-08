using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponStatusBar : MonoBehaviour
{
    public RectTransform selection;
    public Image icon;
    public Image fill;
    public TMP_Text amount;

    public void DrawCooldowm(float currentValue, float maxValue)
    {
        fill.fillAmount = currentValue / maxValue;
    }

    public void DrawAmount(int value)
    {
        if (value >= 0)
        {
            amount.text = value.ToString();
        }
        else
        {
            amount.text = "∞";
        }
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
