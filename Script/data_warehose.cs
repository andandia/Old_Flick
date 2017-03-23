using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 読み込まれたデータが保存される
/// </summary>
public class data_warehose : MonoBehaviour {


	public Question[] words_lv1;
    public Question[] words_lv2;
    public Question[] words_lv3;
    public Question[] words_lv4;
    public Question[] words_lv5;
    public Question[] words_lv6;
    public Question[] words_lv7;
    public Question[] words_lv8;
    public Question[] words_lv9;
    public Question[] words_lv10;


    void Start()
    {
        DontDestroyOnLoad(this);
    }



    /// <summary>
    /// 問題を定義する構造体
    /// </summary>
    public struct Question
	{
		public string disp, Typ;

		public Question(string display, string Typing)
		{
			disp = display;
			Typ = Typing;
		}
	}


	/// <summary>
	/// 各レベルの配列を作らせる(loaderから)
	/// </summary>
	public void array_create(int[] lv_size)
	{
		words_lv1 = new Question[lv_size[1]];
        words_lv2 = new Question[lv_size[2]];
        words_lv3 = new Question[lv_size[3]];
        words_lv4 = new Question[lv_size[4]];
        words_lv5 = new Question[lv_size[5]];
        words_lv6 = new Question[lv_size[6]];
        words_lv7 = new Question[lv_size[7]];
        words_lv8 = new Question[lv_size[8]];
        words_lv9 = new Question[lv_size[9]];
        words_lv10 = new Question[lv_size[10]];
    }
}
