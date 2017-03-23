using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// CSVを読み込む、読み込んだデータをdata_warehouseに格納してシーンを変える
/// </summary>
public class loader : MonoBehaviour {

    string filepath = "";
    string wordDB = "";
    string[] lines;
    TextReader txtReader;
    TextAsset csv;
    string description = "";
    string txtBuffer = "";


    int word_count;//(csc上の「文字数」の行に格納されている)文字数

    List<string> disp_words_lv1 = new List<string>();
    List<string> Typ_words_lv1 = new List<string>();
    List<string> disp_words_lv2 = new List<string>();
    List<string> Typ_words_lv2 = new List<string>();
    List<string> disp_words_lv3 = new List<string>();
    List<string> Typ_words_lv3 = new List<string>();
    List<string> disp_words_lv4 = new List<string>();
    List<string> Typ_words_lv4 = new List<string>();
    List<string> disp_words_lv5 = new List<string>();
    List<string> Typ_words_lv5 = new List<string>();
    List<string> disp_words_lv6 = new List<string>();
    List<string> Typ_words_lv6 = new List<string>();
    List<string> disp_words_lv7 = new List<string>();
    List<string> Typ_words_lv7 = new List<string>();
    List<string> disp_words_lv8 = new List<string>();
    List<string> Typ_words_lv8 = new List<string>();
    List<string> disp_words_lv9 = new List<string>();
    List<string> Typ_words_lv9 = new List<string>();
    List<string> disp_words_lv10 = new List<string>();
    List<string> Typ_words_lv10 = new List<string>();

    /// <summary>
    /// 各レベルの配列の長さ
    /// </summary>
    public int[] lv_size;


    [SerializeField]
    data_warehose d_w;


   



    void Start () {
        lv_size = new int[11];
        filepath_decide();
        ReadFile();
        lv_size_set();
        d_w.array_create(lv_size);
        data_place();
        SceneManager.LoadScene("play");
    }

    void ReadFile()
    {
        FileInfo file = new FileInfo(filepath);
        try
        {
            csv = Resources.Load("word") as TextAsset;
            wordDB = csv.text;
            lines = wordDB.Split('\n'); //linesに1行毎にツッコむ
            
        }
        catch (Exception)
        {
            // 改行コード
            wordDB += SetDefaultText();
            Debug.Log("エラー ");
            
        }



        foreach (var now_line in lines)//読み込み
        {
             //char sharp = '#';
            //Debug.Log(now_line == string.Empty);
                
            //ワード指定行で行う処理
            if (now_line != string.Empty && now_line[0] == '#')//空行でなく、行の先頭が#なら
            {
                    //まず文字数を取得
                    /*
                    for (int i = 1; i < now_line.Length -1 ; i++)
                    {
                        Debug.Log(now_line);
                        //Debug.Log(now_line.Length -1 );
                        //tmp_count = now_line.Substring(2, i);//まずは1文字は入れる
                        //now_line.Substring (a b) aからb文字

                        if (now_line.Substring(2+i, 1) == ",")//,次の文字が,なら抜ける
                        {

                            Debug.Log("ddd " + now_line.Substring(2 + i, 1));
                            Debug.Log(now_line.Substring(2 , i));
                            word_count = int.Parse(now_line.Substring(2 , i));
                            Debug.Log(word_count);
                            break;
                        }
                        //Debug.Log("no ,");


                        word_count = int.Parse(now_line.Substring(2, 1));
                        Debug.Log(word_count);

                        //文字数からレベル分け
                        int Lv = (word_count - 1) / 2;
                        if (Lv == 0)
                        {
                            Lv = 1;
                        }
                        Debug.Log("adb"+ now_line.Substring(i, 1));

                    }
                    */
                //表示文字の切り出し
                Debug.Log(now_line.Length + "now_line.Length");
                Debug.Log(now_line.Length -2  + "now_line.Length -2");
                List<int> commas = new List<int>();//カンマが何文字目か
                for (int i = 0; i < (now_line.Length - 1); i++)//どこに,があるのかを読む
                {
                    Debug.Log( i + "文字目");
                    //Debug.Log("今見てる文字" + now_line.Substring(i));
                    Debug.Log("今見てる文字" +  now_line[i]);
                    //Debug.Log("now_line.Substring " + now_line.Substring(i, 1));
                    if (now_line[i] == ',')//今見ているのが,なら
                    {
                        Debug.Log(",があるのは" + i +"文字目");
                        Debug.Log(now_line.Substring(i, 1));
                        commas.Add(i);
                    }
                }
                //文字数データからレベル判断して突っ込む
                /*ややこしいけど正確(だと思う)な方式。
                //Debug.Log("文字数 " + now_line.Substring((commas[0]+1), (commas[1] - commas[0] - 1)));
                word_count = double.Parse(now_line.Substring((commas[0] + 1), (commas[1] - commas[0] - 1)));//文字数を突っ込む(double)
                Debug.Log("word_count " + word_count);
                //double dab = Math.Ceiling((word_count - 1) / 2);
                //Debug.Log("Lv " + Math.Ceiling((word_count - 1)/2 ));
                int i_word_count = (int)Math.Ceiling((word_count - 1) / 2);//割って少数切り上げ
                Debug.Log("i_word_count " + i_word_count);
                */
                //文字列をint化
                word_count = int.Parse(now_line.Substring((commas[0] + 1), (commas[1] - commas[0] - 1)));
                Debug.Log("word_count " + word_count);
                Debug.Log("レベル " + (word_count-1)/2);

                //レベル計算式 (文字数-1)/2 ＝レベル
                switch ((word_count - 1) / 2)//レベル判断、配列格納
                {
                    case 0:
                    case 1:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv1.Add( now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1) );
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv1.Add( now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1) );
                        break;
                    case 2:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv2.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv2.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 3:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv3.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv3.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 4:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv4.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv4.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 5:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv5.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv5.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 6:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv6.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv6.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 7:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv7.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv7.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 8:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv8.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv8.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 9:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv9.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv9.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    case 10:
                        //Debug.Log("表示文字 " + now_line.Substring((commas[1] + 1 ), (commas[2] - commas[1]) - 1) );
                        disp_words_lv10.Add(now_line.Substring((commas[1] + 1), (commas[2] - commas[1]) - 1));
                        //Debug.Log("ひらがな " + now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1)  );
                        Typ_words_lv10.Add(now_line.Substring((commas[2] + 1), (commas[3] - commas[2]) - 1));
                        break;
                    default:
                        Debug.Log("未実装");
                        break;
                }
               
                
            }

            


        }
        

    }



    // 改行コード処理 エラー処理
    string SetDefaultText()
    {
        return "C#あ\n";
    }




    void filepath_decide()//ファイルパスを決定する。現時点でエディタ上のみ対応
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject externalFilesDir = currentActivity.Call<AndroidJavaObject>("getExternalFilesDir", null))
                    {
                        filepath = "jar:file://" + Application.dataPath + "!/assets" + "/" + "word.csv";
                    }//後で/songs/以下をロード結果無いし指定で変化させる
                }
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            filepath = Application.dataPath + "/Raw/Songs/Winter,again/" + "Winter,again10key.bms";
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)//エディタ
        {
            filepath = Application.dataPath + "/StreamingAssets/"+"word.csv";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            filepath = Application.dataPath + "/" + "Songs/" + "Winter,again/" + "Winter,again10key.bms";
        }

    }

    

   


    /// <summary>
    /// 各レベルの配列の長さを格納する
    /// </summary>
    void lv_size_set()
    {
        lv_size[0] = 0;//インデックスは0から始まるがレベルとの対比をわかりやすくするためにレベル0の個数＝0にしている
        //※各レベルに当たる文字数の問題が必ず1問はあるように
        lv_size[1] = disp_words_lv1.Count;
        lv_size[2] = disp_words_lv2.Count;
        lv_size[3] = disp_words_lv3.Count;
        lv_size[4] = disp_words_lv4.Count;
        lv_size[5] = disp_words_lv5.Count;
        lv_size[6] = disp_words_lv6.Count;
        lv_size[7] = disp_words_lv7.Count;
        lv_size[8] = disp_words_lv8.Count;
        lv_size[9] = disp_words_lv9.Count;
        lv_size[10] = disp_words_lv10.Count;
    }




    /// <summary>
    /// データをdata_warehouseへと格納し直す
    /// </summary>
    void data_place()
    {
        for (int i = 0; i < disp_words_lv1.Count; i++)
        {
            d_w.words_lv1[i].disp = disp_words_lv1[i];
            d_w.words_lv1[i].Typ = Typ_words_lv1[i];
        }
        for (int i = 0; i < disp_words_lv2.Count; i++)
        {
            d_w.words_lv2[i].disp = disp_words_lv2[i];
            d_w.words_lv2[i].Typ = Typ_words_lv2[i];
        }
        for (int i = 0; i < disp_words_lv3.Count; i++)
        {
            d_w.words_lv3[i].disp = disp_words_lv3[i];
            d_w.words_lv3[i].Typ = Typ_words_lv3[i];
        }
        for (int i = 0; i < disp_words_lv4.Count; i++)
        {
            d_w.words_lv4[i].disp = disp_words_lv4[i];
            d_w.words_lv4[i].Typ = Typ_words_lv4[i];
        }
        for (int i = 0; i < disp_words_lv5.Count; i++)
        {
            d_w.words_lv5[i].disp = disp_words_lv5[i];
            d_w.words_lv5[i].Typ = Typ_words_lv5[i];
        }
        for (int i = 0; i < disp_words_lv6.Count; i++)
        {
            d_w.words_lv6[i].disp = disp_words_lv6[i];
            d_w.words_lv6[i].Typ = Typ_words_lv6[i];
        }
        for (int i = 0; i < disp_words_lv7.Count; i++)
        {
            d_w.words_lv7[i].disp = disp_words_lv7[i];
            d_w.words_lv7[i].Typ = Typ_words_lv7[i];
        }
        for (int i = 0; i < disp_words_lv8.Count; i++)
        {
            d_w.words_lv8[i].disp = disp_words_lv8[i];
            d_w.words_lv8[i].Typ = Typ_words_lv8[i];
        }
        for (int i = 0; i < disp_words_lv9.Count; i++)
        {
            d_w.words_lv9[i].disp = disp_words_lv9[i];
            d_w.words_lv9[i].Typ = Typ_words_lv9[i];
        }
        for (int i = 0; i < disp_words_lv10.Count; i++)
        {
            d_w.words_lv10[i].disp = disp_words_lv10[i];
            d_w.words_lv10[i].Typ = Typ_words_lv10[i];
        }
    }



    
}
