using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour
{
    public string balance, energy;
    public Text tBalance, tEnergy;
    public Text tFailure1, tFailure2, tFailure3, tFailure4;
    public Text tAge;
    public string failure1, failure2, failure3, failure4;
    public int chance;
    public int age;
    public System.Random rnd;
    public PlayerControls PlayerControls;
    public bool compensate;
    public double eFactor, eAmmount, fAmmount;
    public GameObject finalScreen;
    public bool triggerTablet;


    void Start()
    {
        failure1 = "0"; failure2 = "5"; failure3 = "10"; failure4 = "15";
        balance = "1000"; energy = "100";
        rnd = new System.Random(); chance = 0; age = 20; compensate = false;
        eFactor = 1; eAmmount = 1; fAmmount = 0; triggerTablet = false;
    }

    public void CheckChance()
    {
        if (int.Parse(energy) == 0) chance = 60;
        else if (int.Parse(energy) < 25) chance = 20;
        else if (int.Parse(energy) < 50) chance = 10;
        else chance = 0;
    }

    public void CalculateFailureChance()
    {
        CheckChance();
        failure1 = (0 + chance).ToString();
        failure2 = (5 + chance).ToString();
        failure3 = (10 + chance).ToString();
        failure4 = (15 + chance).ToString();
    }

    public void CalculateEnergy(int bound1, int bound2)
    {
        var fatigue = rnd.Next(bound1, bound2 + 1);
        if (eAmmount > 0)
        {
            fatigue = (int)(fatigue * eFactor);
            eAmmount--;
        }
        energy = (int.Parse(energy) - fatigue).ToString();
        if (int.Parse(energy) < 0) energy = "0";
    }

    public void CalculateMoney(int bound1, int bound2, string failure)
    {
        var salary = rnd.Next(bound1, bound2 + 1) * 10;
        var increase = 0;
        switch (fAmmount)
        {
            case 0:
                increase = 0;
                break;

            case 1:
                increase = 25;
                fAmmount--;
                break;
        }
        var check = rnd.Next(1, 101) < (int.Parse(failure) + increase);
        if (!check) balance = (salary + int.Parse(balance)).ToString();
        if (int.Parse(balance) > 1000000) finalScreen.SetActive(true);
    }

    public void DoWorkPhase1(int number)
    {
        switch(number)
        {
            case 1:
                CalculateEnergy(10, 15);
                CalculateFailureChance();
                CalculateMoney(10, 30, failure1);
                triggerTablet = true;
                break;

            case 2:
                CalculateEnergy(15, 20);
                CalculateFailureChance();
                CalculateMoney(25, 50, failure2);
                triggerTablet = true;
                break;

            case 3:
                CalculateEnergy(20, 25);
                CalculateFailureChance();
                CalculateMoney(50, 90, failure3);
                triggerTablet = true;
                break;

            case 4:
                CalculateEnergy(20, 30);
                CalculateFailureChance();
                CalculateMoney(75, 125, failure4);
                triggerTablet = true;
                break;
        }
        int circles = PlayerControls.circles;
        age = 20 + circles;

    }

    public void GetEnergy(Text text)
    {
        if (!compensate)
        {
            string refill = text.text.Replace("+", "");
            energy = (int.Parse(energy) + int.Parse(refill)).ToString();
            if (int.Parse(energy) > 100) energy = "100";
            CalculateFailureChance();
        }
        else compensate = false;
    }

    public void SpendMoney(Text text)
    {
        string value = text.text;
        if (int.Parse(balance) >= int.Parse(value))
            balance = (int.Parse(balance) - int.Parse(value)).ToString();
        else compensate = true;
    }

    public void CheckDebuff(GameObject gameObject)
    {
        if (eAmmount < 1) gameObject.SetActive(false);
    }

    void Update()
    {
        tBalance.text = balance;
        tEnergy.text = energy;
        tFailure1.text = failure1;
        tFailure2.text = failure2;
        tFailure3.text = failure3;
        tFailure4.text = failure4;
        tAge.text = age.ToString();
    }
}