using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    bool Pause_flag = false;

    [SerializeField]
    GameObject Pause_UI;

    [SerializeField]
    GameObject Yes_No;

    [SerializeField]
    GameObject Back_menu;


    [SerializeField]
    touch_input touch_input;

    [SerializeField]
    Sound_maneger Sound_maneger;

    public void Pauseing()
    {
        if (Pause_flag == false)//ポーズ
        {
            Pause_UI.SetActive(true);
            Back_menu.SetActive(true);
            touch_input.enabled = false;
            Sound_maneger.bgm_pause(false);
            Time.timeScale = 0;
            Pause_flag = true;
        }
        else//アンポーズ
        {
            Pause_UI.SetActive(false);
            Yes_No.SetActive(false);
            touch_input.enabled = true;
            Sound_maneger.bgm_pause(true);
            Time.timeScale = 1;
            Pause_flag = false;
        }
        
    }
}
