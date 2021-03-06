using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum Colors
{
    Uncolored,
    Red,
    Green,
    Blue,
    Yellow
}

public class Field
{

    public Vector3 fPosition { get; set; }
    public Colors color { get; set; }
    public bool inShadow { get; set; }

    public Field(Vector3 p, Colors c, bool s)
    {
        fPosition = p;
        color = c;
        inShadow = s;
    }
}

public class PlayerControls : MonoBehaviour
{
    private Vector3 pos;
    public Field actualField;
    private int step = 1;
    public int circles = 0;
    private System.Random rnd;
    private List<Field> list;

    void Start()
    {
        var nl = new List<Field>();
        actualField = new Field(GameObject.Find("1").transform.position, Colors.Uncolored, false);
        pos = actualField.fPosition;
        for (int i = 1; i <= 90; i++)
        {
            var obj = GameObject.Find(i.ToString());
            if (obj.tag == "Uncolored")
                nl.Add(new Field(obj.transform.position, Colors.Uncolored, false));
            else if (obj.tag == "Red") 
                nl.Add(new Field(obj.transform.position, Colors.Red, false));
            else if (obj.tag == "Blue")
                nl.Add(new Field(obj.transform.position, Colors.Blue, false));
            else if (obj.tag == "Green")
                nl.Add(new Field(obj.transform.position, Colors.Green, false));
            else nl.Add(new Field(obj.transform.position, Colors.Yellow, false));
        }
        list = nl;
        rnd = new System.Random();
    }

    public void MakeMove()
    {
        int move = rnd.Next(1, 7);
        step += move;
        if (step > 90)
        {
            step %= 90;
            circles++;
        }
        actualField = list[step - 1];
        pos = actualField.fPosition;
    }

    void Update()
    {
        transform.position = new Vector3(pos.x, pos.y + 5.5f, pos.z);
    }
}