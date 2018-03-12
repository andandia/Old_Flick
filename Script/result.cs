using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour {



	[SerializeField]
	GameObject LEVEL_num;

	[SerializeField]
	GameObject SCORE_num;

    [SerializeField]
    Sound_maneger Sound_maneger;

    GameObject finish;
	finish_status finish_status;


    GameObject admob_obj;
    Admob admob;

    GameObject d_w;


    [SerializeField]
    AudioSource se_Source;


    // Use this for initialization
    void Start () {
        admob_obj = GameObject.Find("admob");
        admob = admob_obj.GetComponent<Admob>();
        admob.showBannerAd(2);
        finish = GameObject.Find("finish_status(Clone)");
		finish_status = finish.GetComponent<finish_status>();
        // d_w = GameObject.Find("data_warehose");
        //Destroy(d_w);
        admob.showInterstitialAd();
        disp_result();

	}
	

	/// <summary>
	/// 結果発表
	/// </summary>
	void disp_result()
	{
		LEVEL_num.GetComponent<Text>().text = finish_status.total_level.ToString();
		SCORE_num.GetComponent<Text>().text = finish_status.total_score.ToString();
	}


    /// <summary>
    /// リザルトのOKボタンをした時の挙動
    /// </summary>
    public void on_click()
    {

        admob.Bannerdestroy();

        Destroy(finish);
        Destroy(admob_obj);
        Sound_maneger.play_button_se();

        StartCoroutine("LoadSceneAndWait");
    }



    IEnumerator LoadSceneAndWait()
    {
        AsyncOperation ope = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Top");
        ope.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5f);
        ope.allowSceneActivation = true;
    }
}
