using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 現在の状態(スコア、 レベル)を司る
/// </summary>
public class status_manager : MonoBehaviour {

    /// <summary>
    /// 現在のレベル
    /// </summary>
    public int now_level = 1;

    public int score = 0;

    /// <summary>
    /// (現在のLEVELで)倒した数
    /// </summary>
    int kill_score = 0;

    int[] level_table = new int[] { 0, 3, 3, 3, 4, 5, 5, 5, 5, 5, 5 };


    [SerializeField]
    Time_manager Time_manager;

    [SerializeField]
    Sound_maneger Sound_maneger;

    [SerializeField]
    Anime_maneger Anime_maneger;

    [SerializeField]
    GameObject score_txt;

    [SerializeField]
    GameObject level_txt;

    [SerializeField]
    Admob admob;

    [SerializeField]
    GameObject admob_obj;

    [SerializeField]
    touch_input touch_input;

    /// <summary>
    /// finish_statusプレハブ
    /// </summary>
    [SerializeField]
    GameObject finish_status_pre;

    [SerializeField]
    GameObject ready;

    [SerializeField]
    GameObject go;

    /// <summary>
    /// タイムアップ画像
    /// </summary>
    [SerializeField]
    GameObject time_up_obj;


    /// <summary>
    /// 1文字打つごとに入る得点
    /// </summary>
    [SerializeField]
    int score_par_char = 1;

    /// <summary>
    /// 1問ごとに入る得点
    /// </summary>
    [SerializeField]
    int score_par_Question = 10;

    GameObject property;
    Property Property;

    void Start () {
        DontDestroyOnLoad(admob_obj);
        admob.showBannerAd(1);
        property = GameObject.FindGameObjectWithTag("property");
        Property = property.GetComponent<Property>();
        Anime_maneger.Zombie_Appear();
        Time_manager.setNowMode(Property.Now_mode);
        Time_manager.SetStartTime();
        redeygo();
    }

  

    /// <summary>
    /// 1文字毎のスコア加算
    /// </summary>
    /// <param name="par_score">毎のスコア</param>
    public void add_score()
    {
        score += score_par_char;
        score_txt.GetComponent<Text>().text = "SCORE:" + score.ToString();
    }

    /// <summary>
    /// 問題を打ち切ったときのスコア加算
    /// </summary>
    /// <param name="par_score">毎のスコア</param>
    /// <param name="char_length">問題の文字数</param>
    public void add_score(int char_length)
    {
        score += score_par_Question * char_length;
        score_txt.GetComponent<Text>().text = "SCORE:" + score.ToString();
        kill_score++;
        if (kill_score >= level_table[now_level])
        {
            kill_score = 0;
            if (now_level < level_table.Length -1)
            {
                now_level++;
            }
            level_txt.GetComponent<Text>().text = "LEVEL:" + now_level.ToString();
        }
    }



    /// <summary>
    /// 終了時処理
    /// </summary>
    public void time_up()
    {
        Sound_maneger.play_finish_se();
        Instantiate(time_up_obj, new Vector3(0f, 3.5f, -4f), Quaternion.identity);
        touch_input.GetComponent<touch_input>().enabled = false;
        GameObject finish = Instantiate(finish_status_pre) as GameObject;//finish_statusを作る
        finish_status finish_status = finish.GetComponent<finish_status>();//スクリプトを探す
        finish_status.finish_state(score , now_level);//状態をつっこむ
        Sound_maneger.play_bgm_stop();
        StartCoroutine(endwait());


    }

 


    IEnumerator endwait()
    {
        yield return new WaitForSecondsRealtime(3f);
        admob.Bannerdestroy();
        admob.RequestInterstitialAds();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Result");
    }




    void redeygo()
    {
        StartCoroutine(readywait());
        
    }

    
    IEnumerator readywait()
    {
        yield return new WaitForSeconds(1.9f);
        Destroy(ready);
        Instantiate(go, new Vector3(0f, 3f, -3f), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.Find("go(Clone)"));
        touch_input.GetComponent<touch_input>().enabled = true;
        Time_manager.setTimeCount(true);
        Sound_maneger.play_bgm_play();
        }



    /// <summary>
    /// スコアアタック時のメニューに戻る挙動
    /// </summary>
    public void Retun_Top()
    {
        admob.Bannerdestroy();
        Destroy(admob_obj);

    }


}
