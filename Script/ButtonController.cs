using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : BaseButtonController
{
    [SerializeField]
    public AudioClip decide_se;
    [SerializeField]
    private AudioSource se_Source;
    [SerializeField]
    admob admob;
    [SerializeField]
    GameObject admob_obj;

    void Start()
    {
        admob.RequestBanner(1);
    }


    /// <summary>
    /// どのオブジェクトをクリックしたか
    /// </summary>
    /// <param name="objectName"></param>
    protected override void OnClick(string objectName)
    {
        // 渡されたオブジェクト名で処理を分岐
        if ("SCORE_ATTACK".Equals(objectName))
        {
            // Button1がクリックされたとき
            this.SCORE_ATTACK();
        }
        else if ("tutorial".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.tutorial();
        }
        else
        {
            throw new System.Exception("Not implemented!!");
        }
    }




    private void SCORE_ATTACK()
    {
        se_Source.clip = decide_se;
        se_Source.Play();
        Debug.Log("SCORE_ATTACK Click");
        admob.destroy();
        StartCoroutine("LoadSceneAndWait");
    }

    private void tutorial()
    {
        Application.OpenURL("http://fzonbie.weebly.com/");
    }



    IEnumerator LoadSceneAndWait()
    {
        AsyncOperation ope = SceneManager.LoadSceneAsync("load");
        ope.allowSceneActivation = false;
        yield return new WaitForSeconds(0.2f);
        ope.allowSceneActivation = true;
    }
}
