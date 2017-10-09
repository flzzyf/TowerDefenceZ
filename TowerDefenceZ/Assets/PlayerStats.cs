using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    //初始属性
    public static int money;
    public int startMoney = 400;

    public static int lives;
    public int startLives = 5;

    public static int rounds = 0;

    void Start ()
    {
        money = startMoney;
        lives = startLives;
	}

}
