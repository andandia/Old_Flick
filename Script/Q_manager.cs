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
	/// 正解時のエフェクト
	/// </summary>
	[SerializeField]
	GameObject hit_bullet;


    [SerializeField]
    AudioClip se;
    [SerializeField]
    AudioSource se_Source;


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
	/// レベルの配列の長さ
	/// </summary>
	int level_array_length;

	/// <summary>
	/// 問題番号
	/// </summary>
	int Q_number;


    

	void Start () {
        data_warehose = GameObject.Find("data_warehose");
        d_w = data_warehose.GetComponent<data_warehose>();
        type_txt.GetComponent<Text>().resizeTextForBestFit = true;
        disp_txt.GetComponent<Text>().resizeTextForBestFit = true;
        se_Source.clip = se;
        lottery();

	}


	/// <summary>
	/// 問題の抽選
	/// </summary>
	public void lottery()
	{
		
		now_level = status_manager.now_level;//レベルを取る
		switch (now_level)//格納されている各レベルの配列の長さを取る
		{
			case 1:
				level_array_length = d_w.words_lv1.GetLength(0);
				Q_number = random(level_array_length);
				Q_place(d_w.words_lv1[Q_number].Typ, d_w.words_lv1[Q_number].disp);
				break;
            case 2:
                level_array_length = d_w.words_lv2.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv2[Q_number].Typ, d_w.words_lv2[Q_number].disp);
                break;
            case 3:
                level_array_length = d_w.words_lv3.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv3[Q_number].Typ, d_w.words_lv3[Q_number].disp);
                break;
            case 4:
                level_array_length = d_w.words_lv4.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv4[Q_number].Typ, d_w.words_lv4[Q_number].disp);
                break;
            case 5:
                level_array_length = d_w.words_lv5.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv5[Q_number].Typ, d_w.words_lv5[Q_number].disp);
                break;
            case 6:
                level_array_length = d_w.words_lv6.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv6[Q_number].Typ, d_w.words_lv6[Q_number].disp);
                break;
            case 7:
                level_array_length = d_w.words_lv7.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv7[Q_number].Typ, d_w.words_lv7[Q_number].disp);
                break;
            case 8:
                level_array_length = d_w.words_lv8.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv8[Q_number].Typ, d_w.words_lv8[Q_number].disp);
                break;
            case 9:
                level_array_length = d_w.words_lv9.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv9[Q_number].Typ, d_w.words_lv9[Q_number].disp);
                break;
            case 10:
                level_array_length = d_w.words_lv10.GetLength(0);
                Q_number = random(level_array_length);
                Q_place(d_w.words_lv10[Q_number].Typ, d_w.words_lv10[Q_number].disp);
                break;
            default:
				break;
		}
		

	}


	/// <summary>
	/// 問題を抽選するためのランダムな数字を出す
	/// </summary>
	/// <param name="digits">インデックス数(-1までの数が返る)</param>
	int random(int digits)
	{
		System.Random random = new System.Random();
		int random_number = random.Next(digits - 1);
		return random_number;
	}



	/// <summary>
	/// 現在の問題を指定する
	/// </summary>
	/// <param name="question">打つ問題(文字列)</param>
	public void Q_place(string type_question , string disp_question)
	{
		now_question = type_question;
        now_question_length = type_question.Length;
        if (now_question_length>14)
        {

        }
        type_txt.GetComponent<Text>().text = now_question;
		disp_txt.GetComponent<Text>().text = disp_question;
	}


	/// <summary>
	/// 打った文字の正誤判定
	/// </summary>
	/// <param name="type">打った文字</param>
	public void judge(string type)
	{

		if (now_question.Length != 0 && type == now_question.Substring(0, 1))//先頭文字と一緒
		{
            se_Source.Play();
            now_question = now_question.Remove(0, 1);//その文字を消す
			type_txt.GetComponent<Text>().text = now_question;
            int pos = random(8);
            Instantiate(hit_bullet, hit_position[pos], Quaternion.identity) ;
            status_manager.add_score();
        }
		if (now_question.Length == 0)//打ち切った
		{
			shoot();
            
        }

	}



    /// <summary>
    /// 打ったときの処理
    /// </summary>
	void shoot()
	{
        status_manager.add_score(now_question_length);
        lottery();
	}

}
