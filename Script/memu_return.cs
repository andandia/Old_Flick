using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class memu_return : MonoBehaviour {

    bool Show_Yes_No_flag = false;

    [SerializeField]
    GameObject Yes_No;

    [SerializeField]
    GameObject Pause_UI;

    [SerializeField]
    GameObject Back_menu;

    GameObject property;
    Property Property;

    [SerializeField]
    Sound_maneger Sound_maneger;

    [SerializeField]
    status_manager status_manager;

    /*
     * はいいいえを表示させる
     * いいえを押したらその2つは消えてまたメニューに戻るを表示
     * はいを押したらモードによってリザルトにするのか
     * メニューに戻るのか
     * */

    /// <summary>
    ///  メニューに戻るをタッチした時に はい いいえボタンを表示させる
    /// </summary>
    public void Show_Yes_No()
    {
        Yes_No.SetActive(true);
        Back_menu.SetActive(false);
        button_se();
    }




    public void Tap_No() //いいえをタップした時
    {
        Yes_No.SetActive(false);
        Back_menu.SetActive(true);
        button_se();

    }


    public void Tap_Yes() //はいをタップした時
    {
        Yes_No.SetActive(false);
        Back_menu.SetActive(true);
        button_se();
        property = GameObject.FindGameObjectWithTag("property");
        Property = property.GetComponent<Property>();
        switch (Property.Get_Now_mode())
        {
            case 1://スコアアタック
                Pause_UI.SetActive(false);
                Yes_No.SetActive(false);
                button_se();
                Property.Destroy();
                status_manager.Retun_Top();
                SceneManager.LoadSceneAsync("Top");
                break;
            case 2://ENDLESS
                Pause_UI.SetActive(false);
                Yes_No.SetActive(false);
                button_se();
                Property.Destroy();
                status_manager.time_up();
                break;
            default:
                break;
        }
        
    }


    void button_se()
    {
        Sound_maneger.play_button_se();
    }



   


}
