using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 問題に関するものを司る。
/// 格納されたものをランダムに読み、入力結果による正誤判定をする
/// </summary>
public class Q_manager : MonoBehaviour {
	

	[SerializeField]
	input Play;//Playクラス

	[SerializeField]
	status_manager status_manager;

    GameObject data_warehose;
    data_warehose d_w;

	/// <summary>
	/// 打つべき文字
	/// </summary>
	[SerializeField]
	GameObject type_txt;

	/// <summary>
	/// 表示する文字
	/// </summary>
	[SerializeField]
	GameObject disp_txt;

    /// <summary>
	///脚注として表示する文字
	/// </summary>
	[SerializeField]
    GameObject supplement_txt;


    /// <summary>
    ///ジャンルとして表示する文字
    /// </summary>
    [SerializeField]
    GameObject genre_txt;

    /// <summary>
    /// 正解時のエフェクト
    /// </summary>
    [SerializeField]
	GameObject hit_bullet;


    [SerializeField]
    Anime_maneger Anime_maneger;



    [SerializeField]
    AudioSource gun_se;


    //ヒットエフェクトを出すポジション
    Vector3[] hit_position = new Vector3[]{
	  new Vector3 (-2.2f, 2.5f, -2.5f),
	  new Vector3 (-2f, 3.4f, -2.5f),
	  new Vector3 (-1.3f, 2.6f, -2.5f),
      new Vector3 (0, 2.6f, -2.5f),
      new Vector3 (0.7f, 3.5f, -2.5f),
      new Vector3 (1.6f, 2.7f, -2.5f),
      new Vector3 (2f, 3.5f, -2.5f),
      new Vector3 (2.6f, 2.6f, -2.5f)
    };


	string now_question;//打たなければいけない文字

    /// <summary>
    /// 現在の問題の文字数
    /// </summary>
    int now_question_length;


	int now_level ;

    /// <summary>
    /// データベースのテーブル名
    /// </summary>
    string table_name;


	/// <summary>
	/// 各レベルに当てはまる問題数
	/// </summary>
	int level_Q_length;

	/// <summary>
	/// 問題番号
	/// </summary>
	int Q_number;

    SqliteDatabase sqlDB;


    void Start () {
        /*いらない
        data_warehose = GameObject.Find("data_warehose");
        d_w = data_warehose.GetComponent<data_warehose>();
        */
        type_txt.GetComponent<Text>().resizeTextForBestFit = true;
        disp_txt.GetComponent<Text>().resizeTextForBestFit = true;

        table_name = "Normal_Q";
        sqlDB = new SqliteDatabase("word.db");
        DB_Access();
    }

    /*流れ
    * 現在のレベルを取る
    * そのレベルにあった問題をsqlで取ってくる(候補)
    *その候補の個数を取る
    * 個数に合わせてランダムな数を生成する
    * 問題を指定する
    * 問題を出題する
    */



    void DB_Access()
    {
        
        string query = Make_query();
        DataTable wordDB = sqlDB.ExecuteQuery(query);

        string furigana;
        string disp_string;
        string genre;
        string supplement;

        foreach (DataRow dr in wordDB.Rows)
        {

            furigana = (string)dr["ふりがな"];
            disp_string = (string)dr["表示文字"];
            genre = (string)dr["ジャンル"];
            supplement = (string)dr["脚注"];
            Debug.Log("ふりがな " + furigana + " 表示文字 "  + disp_string + " ジャンル " + genre + " 脚注 " + supplement);

            Q_place((string)dr["ふりがな"], (string)dr["表示文字"], (string)dr["ジャンル"],(string)dr["脚注"]);
        }
    }


    

    string Make_query( )
    {
        //select [カラム],[カラム](略) from [テーブル名] where [条件] order by random() limit 1    でランダム1件取得
        string query;
        query = "select レベル,文字数,表示文字,ふりがな,ジャンル,脚注 from " +
            table_name + " where レベル = " + status_manager.now_level + " ORDER BY RANDOM() LIMIT 1 ";
        return query;
    }






	/// <summary>
	/// ゾンビがアニメーションするのかを決定
	/// </summary>
	public void lottery()
	{
            Anime_maneger.Zonbi_Shake();
        //毎回ブラせてもウザくなかったので毎回アニメするが頻度を減らしたければこれを
        //int Roulette = random(1, 100);
        //if (Roulette <= 50)
        //{
        //}
    }


/// <summary>
/// ランダムな数字を返す
/// </summary>
/// <param name="start">開始値、以上</param>
/// <param name="end">終了値、未満</param>
/// <returns></returns>
	int random(int start, int end)
	{
        int random_number = Random.Range(start, end);
		return random_number;
	}



	/// <summary>
	/// 現在の問題を指定する
	/// </summary>
	/// <param name="question">打つ問題(文字列)</param>
	public void Q_place(string type_question , string disp_question ,string question_genre, string question_supplement)
	{
		now_question = type_question;
        now_question_length = type_question.Length;
        //if (now_question_length>14)
        //{

        //}
        type_txt.GetComponent<Text>().text = now_question;
		disp_txt.GetComponent<Text>().text = disp_question;
        genre_txt.GetComponent<Text>().text = question_genre;
        if (question_supplement != null)
        {
            supplement_txt.GetComponent<Text>().text = question_supplement;
        }

    }


	/// <summary>
	/// 打った文字の正誤判定
	/// </summary>
	/// <param name="type">打った文字</param>
	public void judge(string type)
	{

		if (now_question.Length != 0 && type == now_question.Substring(0, 1))//先頭文字と一緒
		{
            gun_se.Play();
            now_question = now_question.Remove(0, 1);//その文字を消す
			type_txt.GetComponent<Text>().text = now_question;
            int pos = random(0,8);
            Instantiate(hit_bullet, hit_position[pos], Quaternion.identity) ;
            status_manager.add_score();
            lottery();
        }
		if (now_question.Length == 0)//打ち切った
		{
			shoot();
            
        }

	}



    /// <summary>
    /// 打ち切ったときの処理
    /// </summary>
	void shoot()
	{
        status_manager.add_score(now_question_length);
        DB_Access();
    }



    /// <summary>
    /// デバッグボタン用処理
    /// </summary>
    public void DebugDB()
    {
        DB_Access();
    }
}
