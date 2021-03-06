using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsScript : MonoBehaviour
{
    public PlayerControls PlayerControls;
    public StatusScript StatusScript;
    public GameObject blue1, blue2, blue3;
    public GameObject red1, red2, red3;
    public GameObject green1, green2, green3;
    public GameObject yellow1, yellow2, yellow3;
    public GameObject warning, trigger, tablet;
    int yNum;
    System.Random rnd;
    public Animator anim;
    public bool animStarted;
    public GameObject closeButton, openButton;

    void Start()
    {
        yNum = 1; animStarted = false;
        rnd = new System.Random(); 
    }

    public void CheckField()
    {
        Colors color = PlayerControls.actualField.color;
        switch (color)
        {
            case Colors.Blue:
                OpenWindow(blue1, blue2, blue3);
                break;

            case Colors.Red:
                OpenWindow(red1, red2, red3);
                break;

            case Colors.Green:
                OpenWindow(green1, green2, green3);
                break;

            case Colors.Yellow:
                OpenYellow();
                break;
        }
    }

    void OpenWindow(GameObject first, GameObject second, GameObject third)
    {
        var option = rnd.Next(1, 4);
        switch (option)
        {
            case 1:
                first.SetActive(true);
                break;

            case 2:
                second.SetActive(true);
                break;

            case 3:
                third.SetActive(true);
                break;
        }
    }

    public void OpenYellow()
    {
        switch(yNum)
        {
            case 1:
                yellow1.SetActive(true);
                yNum++;
                break;
            case 2:
                yellow2.SetActive(true);
                yNum++;
                break;
            case 3:
                yellow3.SetActive(true);
                yNum++;
                break;
        }
    }

    public void FulfillBlue1()
    {
        var b = StatusScript.balance;
        b = (int.Parse(b) - 200).ToString();
        var chance = rnd.Next(1, 3);
        if (chance == 2) b = (int.Parse(b) + 1000).ToString();
        StatusScript.balance = b;
    }

    public void FulfillBlue2()
    {
        var b = StatusScript.balance;
        var e = StatusScript.energy;
        if (int.Parse(e) >= 10)
        {
            b = (int.Parse(b) + 300).ToString();
            e = (int.Parse(e) - 10).ToString();
        }
        else warning.SetActive(true);
        StatusScript.balance = b;
        StatusScript.energy = e;
    }

    public void FulfillBlue3Pt1()
    {
        var b = StatusScript.balance;
        b = (int.Parse(b) - 150).ToString();
        StatusScript.balance = b;
    }

    public void FulfillBlue3Pt2()
    {
        var e = StatusScript.energy;
        if (int.Parse(e) >= 10)
            e = (int.Parse(e) - 10).ToString();
        else
        {
            warning.SetActive(true);
            var b = StatusScript.balance;
            b = (int.Parse(b) - 150).ToString();
            StatusScript.balance = b;
        }
        StatusScript.energy = e;
    }

    public void FulfillGreen1()
    {
        var e = StatusScript.energy;
        e = (int.Parse(e) + 30).ToString();
        if (int.Parse(e) > 100) e = "100";
        StatusScript.energy = e;
    }

    public void FulfillGreen2()
    {
        var b = StatusScript.balance;
        StatusScript.eFactor = 0.5;
        StatusScript.eAmmount = 1;
    }

    public void FulfillGreen3()
    {
        var b = StatusScript.balance;
        b = (int.Parse(b) + 300).ToString();
        StatusScript.balance = b;
    }

    public void FulfillRed1()
    {
        var e = StatusScript.energy;
        e = (int.Parse(e) - 20).ToString();
        if (int.Parse(e) < 0) e = "0";
        StatusScript.energy = e;
    }

    public void FulfillRed2()
    {
        var b = StatusScript.balance;
        StatusScript.eFactor = 1.25;
        StatusScript.eAmmount = 3;
    }

    public void FulfillRed3()
    {
        StatusScript.fAmmount = 1;
        StatusScript.failure1 = (0 + StatusScript.chance + 25).ToString();
        StatusScript.failure2 = (5 + StatusScript.chance + 25).ToString();
        StatusScript.failure3 = (10 + StatusScript.chance + 25).ToString();
        StatusScript.failure4 = (15 + StatusScript.chance + 25).ToString();
    }

    void Update()
    {
        if (StatusScript.triggerTablet)
        {
            anim.Play("CloseTablet");
            StatusScript.triggerTablet = false;
            animStarted = true;
        }
        if (tablet.transform.position == trigger.transform.position && animStarted)
        {
            animStarted = false;
            closeButton.SetActive(false);
            openButton.SetActive(true);
        }
    }
}