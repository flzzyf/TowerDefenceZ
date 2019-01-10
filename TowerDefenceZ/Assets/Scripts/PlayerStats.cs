using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour {

    //初始属性
    public static int money;
    public int startMoney = 400;

    public static int lives;
    public int startLives = 5;

    public static int rounds = 0;

    public TextMeshProUGUI text;

    void Start ()
    {
        money = startMoney;
        lives = startLives;

        text.text = money.ToString();
    }

    private void Update()
    {
        //Debug.Log(Vector3.Angle(asd.transform.position, qwe.transform.position));

    }

}
