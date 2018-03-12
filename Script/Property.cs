using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 各ゲームシーン間のデータやり取りに使う
/// </summary>
public class Property : MonoBehaviour {


    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 現在のモード。
    /// </summary>
    public int Now_mode;

    public int Get_Now_mode()
    {
        return Now_mode;
    }


    //シーンがメニューに戻るときは破壊する
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
