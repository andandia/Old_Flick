using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : BaseButtonController
{

    [SerializeField]
    Admob admob;
    [SerializeField]
    GameObject admob_obj;

    [SerializeField]
    private GameObject mode_name;

    [SerializeField]
    private GameObject mode_copy;

    [SerializeField]
    private GameObject mode_outline;

    [SerializeField]
    private GameObject mode_ask_txt;

    [SerializeField]
    Sound_maneger Sound_maneger;


    GameObject Property_obj;
    Property Property;

    [SerializeField]
    Anime_maneger Anime_maneger;

    /*
     * やること
     * mode_txtも配列化してテキスト指定は別メソッドに切り出し
     * はいを押したら現在のモードをPropertyに格納してシーン遷移
     */

    string[] mode_names =
    {
        "<color=yellow>SCORE ATTACK</color>",//SCORE ATTACK
        "<color=yellow>ENDLESS</color>",//ENDLESS
        "<color=yellow>チュートリアル</color>"//チュートリアル
    };


    string[] mode_copys =
    {
        "<color=red>早さに挑戦！</color>",//SCORE ATTACK
        "<color=red>限界に挑戦！</color>",//ENDLESS
        ""//チュートリアル
    };



    string[] mode_outlines =
    {
        "60秒間でどれだけスコアを獲得できるか\n挑戦するモードです。",//SCORE ATTACK
        "時間制限もライフもなし！\n好きなだけ遊べるモード\nです。",//ENDLESS
        "このゲームの\nチュートリアルを\n見ることができます。",//チュートリアル

    };


    string[] mode_asks =
   {
        "このモードで遊びますか？",//基本これ
         "(ブラウザが開きます)"//チュートリアル
    };


    /// <summary>
    /// 現在のモード。ゲーム終了まで保持される変数なので扱いに注意
    /// モード選択画面の並び順に1から対応
    /// </summary>
    //public static int Now_mode;


    void Start()
    {
        admob.showBannerAd(2);
        //admob.showInterstitialAd();
        Time.timeScale = 1;
        Property_obj = GameObject.FindGameObjectWithTag("property");
        Property = Property_obj.GetComponent<Property>();
    }


    /// <summary>
    /// どのオブジェクトをクリックしたか
    /// </summary>
    /// <param name="objectName"></param>
    protected override void OnClick(string objectName)
    {

        se_play();

        //// 渡されたオブジェクト名で処理を分岐
        //if ("SCORE_ATTACK".Equals(objectName))
        //{
        //    Property.Now_mode = 1;
        //    SCORE_ATTACK();
        //}
        //else if ("ENDLESS".Equals(objectName))
        //{
        //    Property.Now_mode = 2;
        //    ENDLESS();
        //}
        //else if ("tutorial".Equals(objectName))
        //{
        //    Property.Now_mode = 3;
        //    tutorial();
        //}
        //else if ("No".Equals(objectName))
        //{
        //    No();
        //}
        //else if ("Yes".Equals(objectName))
        //{
        //    Yes();
        //}
        //else
        //{
        //    throw new System.Exception("そんなボタンは無い");
        //}

        switch (objectName)
        {
            case "SCORE_ATTACK":
                Property.Now_mode = 1;
                txt_put(0, 0, 0, 0);
                break;
            case "ENDLESS":
                Property.Now_mode = 2;
                txt_put(1, 1, 1, 0);
                break;
            case "tutorial":
                Property.Now_mode = 3;
                txt_put(2, 2, 2, 1);
                break;
            case "Yes":
                Yes();
                break;
            case "No":
                No();
                break;
            default:
                break;
        }


    }






    private void SCORE_ATTACK()
    {

        txt_put(0, 0, 0, 0);
    }



    private void ENDLESS()
    {
        txt_put(1, 1, 1, 0);
    }



    private void tutorial()
    {
        txt_put(2, 2, 2, 1);

    }





    private void No()
    {
        Anime_maneger.Divergence();
    }


    private void Yes()
    {
        admob.Bannerdestroy();
        switch (Property.Now_mode)
        {
            case 1:
            case 2:
                SceneManager.LoadSceneAsync("Play");
                break;
            case 3:
                Application.OpenURL("http://fzonbie.weebly.com/");
                break;
            default:
                break;
        }
    }


    void txt_put(int mode_name, int mode_copy, int mode_outline, int mode_ask)
    {
        this.mode_name.GetComponent<Text>().text = mode_names[mode_name];
        this.mode_copy.GetComponent<Text>().text = mode_copys[mode_copy];
        this.mode_outline.GetComponent<Text>().text = mode_outlines[mode_outline];
        this.mode_ask_txt.GetComponent<Text>().text = mode_asks[mode_ask];
        Anime_maneger.Convergence();
    }




    void se_play()
    {
        Sound_maneger.play_button_se();
    }



  

}
