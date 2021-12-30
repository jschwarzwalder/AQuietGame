using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   #region  Singleton

    public static PlayerManager instance;
   

    void Awake(){
        instance = this;
    }

   #endregion

    public GameObject player;
    public int health;
    public ArrayList<String> weapons;

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
