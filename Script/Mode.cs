using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 選択したモードを保持し、それによってゲーム内容を変える
/// </summary>
public class Mode : MonoBehaviour {

    public int now_mode;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
