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
	public int now_level = 1 ;

	public int score = 0;

    /// <summary>
    /// (現在のLEVELで)倒した数
    /// </summary>
    int kill_score = 0;

    int[] level_table = new int[] { 0,3, 3, 3, 4, 5, 5, 5, 5,5,5 };


	/// <summary>
	/// 時間
	/// </summary>
	float time = 60;

	/// <summary>
	/// 制限時間表示テキスト
	/// </summary>
	[SerializeField]
	GameObject time_txt;

    [SerializeField]
    GameObject score_txt;

    [SerializeField]
    GameObject level_txt;

    /// <summary>
    /// タイムアップ画像
    /// </summary>
    [SerializeField]
    GameObject time_up;

    [SerializeField]
    admob admob;

    [SerializeField]
    AudioClip bgm;
    [SerializeField]
    AudioSource bgm_Source;

    [SerializeField]
    AudioClip finish_se;
    [SerializeField]
    AudioSource se_Source;


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


    [SerializeField]
    int score_par_char = 1;
    [SerializeField]
    int score_par_Question = 10;

    bool gameover = false;

    bool gamestart = false;



    void Start () {
        admob.RequestBanner(2);
        time_txt.GetComponent<Text>().text = "TIME:" + ((int)time).ToString();
        bgm_Source.clip = bgm;
        se_Source.clip = finish_se;
        redeygo();

    }

	void Update()
    {
        if (gamestart == true)
        {
            countdown();
        }
		
	}


    /// <summary>
    /// カウントダウン
    /// </summary>
	 void countdown()
	{
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        time_txt.GetComponent<Text>().text = "TIME:" + ((int)time).ToString();
        if (time < 0 && gameover == false )
        {
            finish();
        }
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
            now_level++;
            level_txt.GetComponent<Text>().text = "LEVEL:" + now_level.ToString();
        }
    }



    /// <summary>
    /// 終了時処理
    /// </summary>
    void finish()
    {
        se_Source.Play();
        time = 0;
        Instantiate(time_up, new Vector3(0f, 3.5f, -4f), Quaternion.identity);
        gameover = true;
        touch_input.GetComponent<touch_input>().enabled = false;
        GameObject finish = Instantiate(finish_status_pre) as GameObject;//finish_statusを作る
        finish_status finish_status = finish.GetComponent<finish_status>();//スクリプトを探す
        finish_status.finish_state(score , now_level);//状態をつっこむ
        StartCoroutine(endwait());
        bgm_Source.Stop();


    }


    IEnumerator endwait()
    {
        yield return new WaitForSeconds(3f);
        admob.destroy();
        SceneManager.LoadScene("result");
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
        gamestart = true;
        touch_input.GetComponent<touch_input>().enabled = true;
        bgm_Source.Play();
    }


    
    
}
