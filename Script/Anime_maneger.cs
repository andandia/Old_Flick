using DG.Tweening;
using UnityEngine;


/// <summary>
/// アニメーションを司るマネージャー
/// </summary>
public class Anime_maneger : MonoBehaviour {

    [SerializeField]
    GameObject[] Zombie  = new GameObject[3];//0が左,1が中央,2が右

    [SerializeField]
    RectTransform[] UIs = new RectTransform[3];//0:Yes,1:No,2:説明


    float UIsMoveSec = 0.3f;

    Vector3[] ConvergencePos = { new Vector3(-120f, -400f, 100f),//はい
                                                       new Vector3(135f, -400f, 100f) ,//いいえ
                                                       new Vector3(0f, 0f, 0f)  };//説明


    Vector3[] DivergencePos = { new Vector3(-465f, -400f, 100f),//はい
                                                       new Vector3(465f, -400f, 100f) ,//いいえ
                                                       new Vector3(0f, 1170f, 0f)  };//説明

    /// <summary>
    /// ゾンビ出現アニメーション
    /// </summary>
    Sequence[] appear = new Sequence[3];

    //前回ランダムで出た数
    int BeforeRandom;

    // Use this for initialization
    void Start () {
        // Zombie_Down();
        
    }

    /*-------------------------------------------
      * Top画面でのアニメーション
      * --------------------------------------------*/


    /// <summary>
    /// 収束、選択時のアニメーション
    /// </summary>
    public void Convergence()
    {
        UIs[0].DOLocalMove(ConvergencePos[0], UIsMoveSec).SetEase(Ease.Linear);//はい
        UIs[1].DOLocalMove(ConvergencePos[1], UIsMoveSec).SetEase(Ease.Linear);//いいえ
        UIs[2].DOLocalMove(ConvergencePos[2], UIsMoveSec).SetEase(Ease.Linear);//ウインドウ
    }



    /// <summary>
    /// 発散、いいえの時のアニメーション
    /// </summary>
    public void Divergence()
    {
        UIs[0].DOLocalMove(DivergencePos[0], UIsMoveSec).SetEase(Ease.Linear);//はい
        UIs[1].DOLocalMove(DivergencePos[1], UIsMoveSec).SetEase(Ease.Linear);//いいえ
        UIs[2].DOLocalMove(DivergencePos[2], UIsMoveSec).SetEase(Ease.Linear);//ウインドウ
    }


    /*-------------------------------------------
     * ゾンビ系のアニメーション
     * --------------------------------------------*/

    /// <summary>
    /// ゾンビ出現アニメーション
    /// </summary>
    public void Zombie_Appear()
    {
        appear[0] = DOTween.Sequence();
        appear[1] = DOTween.Sequence();
        appear[2] = DOTween.Sequence();

        float Tilt_time1 = 0.25f;
        float Tilt_time2 = 0.5f;

        float Rote1 ;
        float Rote2 ;

        for (int a = 0; a < 3; a++)
        {
            if (a != 1)//中央以外のゾンビ
            {
                Rote1 = 5f;
                Rote2 = 10f;
            }
            else//中央のゾンビは逆
            {
                Rote1 = -5f;
                Rote2 = -10f;
            }
            //左右に揺らぐ
            appear[a].Append(Zombie[a].transform.DORotate(new Vector3(0f, 0f, (-1)*Rote1), Tilt_time1).SetRelative().SetEase(Ease.Linear));
            for (int i = 0; i < 2; i++)
            {
                appear[a].Append(Zombie[a].transform.DORotate(new Vector3(0f, 0f, Rote2), Tilt_time2).SetRelative().SetEase(Ease.Linear));
                appear[a].Append(Zombie[a].transform.DORotate(new Vector3(0f, 0f, (-1) * Rote2), Tilt_time2).SetRelative().SetEase(Ease.Linear));
            }
            appear[a].Append(Zombie[a].transform.DORotate(new Vector3(0f, 0f, Rote2), Tilt_time2).SetRelative().SetEase(Ease.Linear));
            appear[a].Append(Zombie[a].transform.DORotate(new Vector3(0f, 0f, (-1) * Rote1), Tilt_time1).SetRelative().SetEase(Ease.Linear));
            appear[a].Play();
            if (a != 1)//中央以外のゾンビ
            {
                Zombie[a].transform.DOScale(new Vector3(0.45f, 0.45f, 0.45f), 3f).SetEase(Ease.Linear);
            }
            else
            {
                Zombie[a].transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 3f).SetEase(Ease.Linear);
            }
        }

    }

    /// <summary>
    /// ゾンビのやられモーション
    /// </summary>
    public  void Zonbi_Shake()
    {
        int Zonbi_ID = RouletteShakeZonbi();
        Zombie[Zonbi_ID].transform.DOShakePosition(0.2f, 0.2f, 15, 50.0f,false,false).SetEase(Ease.Linear);
    }
    
    /// <summary>
    /// ゾンビが倒れるアニメーション
    /// </summary>
    /// <param name="pos">ゾンビの位置</param>
    void Zombie_Down(int pos)
    {
        Zombie[pos].transform.DORotate(new Vector3(90f, 0f, 0f),2f ).SetEase(Ease.OutCirc);
    }




    /// <summary>
    /// どのゾンビをシェイクさせるか抽選
    /// </summary>
    int RouletteShakeZonbi()
    {

        int Roulette = Random.Range(0, 3);

        while (Roulette == BeforeRandom)
        {
            Roulette = Random.Range(0, 3);
        }
        BeforeRandom = Roulette;
        
        
        return Roulette;
    }



}
