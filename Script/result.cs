using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour {

	[SerializeField]
	admob admob;

	[SerializeField]
	GameObject LEVEL_num;

	[SerializeField]
	GameObject SCORE_num;


	GameObject finish;
	finish_status finish_status;

    GameObject d_w;



    [SerializeField]
    AudioClip se;
    [SerializeField]
    AudioSource se_Source;


    // Use this for initialization
    void Start () {
		admob.RequestBanner(1);
        admob.RequestInterstitial();
        finish = GameObject.Find("finish_status(Clone)");
		finish_status = finish.GetComponent<finish_status>();
        d_w = GameObject.Find("data_warehose");
        Destroy(d_w);
        se_Source.clip = se;
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


    public void on_click()
    {
        admob.DisplayInterstitial();

        Destroy(finish);
        se_Source.Play();
        admob.destroy();

        StartCoroutine("LoadSceneAndWait");



    }
    IEnumerator LoadSceneAndWait()
    {
        AsyncOperation ope = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("start");
        ope.allowSceneActivation = false;
        yield return new WaitForSeconds(0.2f);
        ope.allowSceneActivation = true;
    }
}
