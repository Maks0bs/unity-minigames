using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickerGameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clickUpgradeAmountField;
    [SerializeField] TextMeshProUGUI clickUpgradeCostField;
    [SerializeField] TextMeshProUGUI moneyDisplayField;
    [SerializeField] TextMeshProUGUI uptimeDisplayField;
    [SerializeField] BaseValuesScriptable baseValues;

    //[SerializeField] TextMeshProUGUI textPopupRefab;
    //TextMeshProUGUI textPopup;

    float timer = 0.0f;
    int clickUpgradesAmount = 0;
    int money = 0;
    void Start()
    {
        

        clickUpgradeCostField.text = $"Cost: {GetUpgradeCost()}";

        float time = PlayerPrefs.GetFloat("uptime", -1.0f);
        if (time > 0.0f)
        {
            timer = time;
        }
    }

    int GetUpgradeCost()
    {
        return (int) (
            ((float) baseValues.baseCost) * 
            Mathf.Pow(baseValues.multiplier, clickUpgradesAmount)
        );
    }

    void Update()
    {
        PlayerPrefs.SetFloat("uptime", timer);
        timer += Time.deltaTime;
        uptimeDisplayField.text = $"{(int)timer}s played";
        
        
    }

    public void ClickerClick()
    {
        money += clickUpgradesAmount + 1;
        moneyDisplayField.text = $"{money} ˆ";

        // this does not work now, but progress has been made:

        /*
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 10;
        textPopup = Instantiate(textPopupRefab, pos, Quaternion.identity);
        textPopup.text = $"+{clickUpgradesAmount + 1}";
        var r = StartCoroutine(Animate(textPopup));
        StartCoroutine(StopAction(r, textPopup));
        */
    }

    /*
    IEnumerator Animate(TextMeshProUGUI textPopup)
    {
        textPopup.transform.position += textPopup.transform.up * 0.05f;
        var color = textPopup.color;
        color.a -= 0.01f;
        textPopup.color = color;
        yield return null;
    }

    IEnumerator StopAction(Coroutine r, TextMeshProUGUI textPopup)
    {
        yield return new WaitForSeconds(1f);
        if (r != null)
        {
            StopCoroutine(r);
        }
        Destroy(textPopup);


    }

    */

    public void BuyClickUpgrade()
    {
        int upgradeCost = GetUpgradeCost();
        if (money >= upgradeCost)
        {
            clickUpgradesAmount = Mathf.Min(baseValues.maxUpgrades, clickUpgradesAmount + 1);
            money -= upgradeCost;
            upgradeCost = GetUpgradeCost();
            moneyDisplayField.text = $"{money} ˆ";
            clickUpgradeAmountField.text = $"{clickUpgradesAmount}";
            clickUpgradeCostField.text = $"Cost: {upgradeCost}";
        }

        

    }

    
}
