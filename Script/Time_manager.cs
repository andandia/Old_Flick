using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_manager : MonoBehaviour {

    [SerializeField]
    status_manager status_manager;

    /// <summary>
    /// 現在の時間
    /// </summary>
    float time = 0;

    /// <summary>
	/// 制限時間表示テキスト
	/// </summary>
	[SerializeField]
    GameObject time_txt;



    /// <summary>
    /// 現在のモード。毎回Propatyに取りに行くのはめんどくさいので
    /// </summary>
    int now_mode = 0;

    /// <summary>
    /// 時間カウントフラグ
    /// </summary>
    bool _isTimeCount = false;

    /// <summary>
    /// デバック用に時間を止めるか
    /// </summary>
    [SerializeField]
    bool debug_time_stop;


    // Use this for initialization
    void Start () {
		
	}
	
	void Update () {
        if (_isTimeCount == true && debug_time_stop == false)
        {
            TimeMove();
        }

    }


    /// <summary>
    /// 開始時間を決める
    /// </summary>
    public void SetStartTime()
    {
        switch (now_mode)
        {
            case 1://スコアアタックモード
                time = 60f;
                break;
            case 2://エンドレスモード
                time = 0f;
                break;
            default:
                break;
        }
    }

    // <summary>
    /// 時間を動かす
    /// </summary>
    void TimeMove()
    {
        switch (now_mode)
        {
            case 1://スコアアタックモード
                if (time >= 0)
                {
                    time -= Time.deltaTime;
                }
                break;
            case 2://エンドレスモード
                time += Time.deltaTime;
                break;
            default:
                break;
        }

        time_txt.GetComponent<Text>().text = "TIME:" + ((int)time).ToString();
        if (time < 0 && _isTimeCount == true)
        {
            _isTimeCount = false;
            status_manager.time_up();
        }
    }


    public void setNowMode(int NowMode)
    {
        now_mode = NowMode;
    }


    public void setTimeCount(bool CountStatus)
    {
        _isTimeCount = CountStatus;
    }


}
