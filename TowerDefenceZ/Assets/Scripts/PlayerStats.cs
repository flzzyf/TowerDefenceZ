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

    public GameObject qwe;
    public GameObject asd;

    void Start ()
    {
        money = startMoney;
        lives = startLives;


    }

    private void Update()
    {
        //Debug.Log(Vector3.Angle(asd.transform.position, qwe.transform.position));

    }

}
