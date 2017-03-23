using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// タッチの結果に応じた入力を司る
/// </summary>
public class input : MonoBehaviour {

	string[] a_row;//あ行
	string[] ka_row ;//か行
	string[] sa_row ;//さ行
	string[] ta_row ;//た行
	string[] na_row ;//な行
	string[] ha_row ;//は行
	string[] ma_row ;//ま行
	string[] ya_row ;//や行
	string[] ra_row ;//ら行
	string[] wa_row ;//わ行
	string[] mark_row;//記号
	   

	int key_type = 0;//キー配列タイプ。
	//1＝google日本語準拠。


	string type_beta;//確定前の入力
	string one_before;//濁点半濁点にしたときにその前の状態の文字(”き”→”ぎ”なら”き”)

	
	int touch_id = 0;//行の何文字目かを示す、人間の感覚からは-1

	int char_toggle = 0;//変換が今濁点なのか半濁点なのか小文字なのかを判断する
	int char_type;//その文字が濁点(1)半濁点(2)に変換できるのか小文字(3)に変換できるのか、た行、あ行(4)か

   
	float[] touch_point_line_x = { -2.1f, -0.7f, 0.7f };
	float touch_point_line_x_min = 2.1f;//xの下限
	float[] touch_point_line_y = { -0.4f, -1.7f, -2.9f, -4.2f };
	float touch_point_line_y_min = -5.5f;//yの下限

	int touch_point_x;//タッチした位置がどの行か
	int touch_point_y;//タッチした位置がどの列か
	string touch_line;//タッチした位置の50音行

	

	/// <summary>
	/// 入力した文字
	/// </summary>
	[SerializeField]
	GameObject inp_txt;


	[SerializeField]
	Q_manager Q_manager;




	void Start () {
		key_type = 1;//可変にするときに気をつける
		key_decide(key_type);
	}


	public void On_TouchStart(Vector2 worldPos)
	{
		
		//Debug.Log("worldPos " + worldPos);

		
		if (worldPos.x >= touch_point_line_x[0] && worldPos.x < touch_point_line_x[1])//x範囲判定
		{
			touch_point_x = 0;
		}
		else if (worldPos.x >= touch_point_line_x[1] && worldPos.x < touch_point_line_x[2])
		{
			touch_point_x = 1;
		}
		else if (worldPos.x >= touch_point_line_x[2] && worldPos.x <= touch_point_line_x_min)
		{
			touch_point_x = 2;
		}


		if (worldPos.y <= touch_point_line_y[0] && worldPos.y > touch_point_line_y[1])//x範囲判定
		{
			touch_point_y = 0;
		}
		else if (worldPos.y <= touch_point_line_y[1] && worldPos.y > touch_point_line_y[2])
		{
			touch_point_y = 1;
		}
		else if (worldPos.y <= touch_point_line_y[2] && worldPos.y > touch_point_line_y[3])
		{
			touch_point_y = 2;
		}
		else if (worldPos.y <= touch_point_line_y[3] && worldPos.y >= touch_point_line_y_min)
		{
			touch_point_y = 3;
		}




		if (touch_point_x == 0)
		{
			switch (touch_point_y)
			{
				case 0:
					touch_line = "あ行";
					break;
				case 1:
					touch_line = "た行";
					break;
				case 2:
					touch_line = "ま行";
					break;
				case 3:
					touch_line = "濁点";
					break;
				default:
					break;
			}
		}
		else if (touch_point_x == 1)
		{
			switch (touch_point_y)
			{
				case 0:
					touch_line = "か行";
					break;
				case 1:
					touch_line = "な行";
					break;
				case 2:
					touch_line = "や行";
					break;
				case 3:
					touch_line = "わ行";
					break;
				default:
					break;
			}
		}
		else if (touch_point_x == 2)
		{
			switch (touch_point_y)
			{
				case 0:
					touch_line = "さ行";
					break;
				case 1:
					touch_line = "は行";
					break;
				case 2:
					touch_line = "ら行";
					break;
				case 3:
					touch_line = "記号";
					break;
				default:
					break;
			}
		}


		
		Debug.Log(touch_line);
		
	}


	//濁点 か行 さ行 た行 は行 //半濁点 ぱ行
	//小文字 あ行 や行 「つ」のみ  (「わ」もだが普通の文章で打つことは無いと思われるので除外)
	public void On_TouchUp()
	{
		//Debug.Log("tap " + gesture.pickedObject.name);
		touch_id = 0;
		switch (touch_line)
		{
			case "あ行":
				type_beta = a_row[touch_id];
				char_toggle_set();
				char_type = 4;
				break;
			case "か行":
				type_beta = ka_row[touch_id];
				char_toggle_set();
				char_type = 1;
				break;
			case "さ行":
				type_beta = sa_row[touch_id];
				char_toggle_set();
				char_type = 4;
				break;
			case "た行":
				type_beta = ta_row[touch_id];
				char_toggle_set();
				char_type = 1;
				break;
			case "な行":
				type_beta = na_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "は行":
				type_beta = ha_row[touch_id];
				char_toggle_set();
				char_type = 2;
				break;
			case "ま行":
				type_beta = ma_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "や行":
				type_beta = ya_row[touch_id];
				char_toggle_set();
				char_type = 3;
				break;
			case "ら行":
				type_beta = ra_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "わ行":
				type_beta = wa_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "記号":
				type_beta = mark_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "濁点":
				if (char_type == 1)//濁点のみ
				{
					if (char_toggle == 0)//濁点をつける処理
					{
						one_before = type_beta;
						type_beta = type_beta + "゛";
						type_beta = ToPadding(type_beta);
						char_toggle = 1;
					}
					else//濁点から戻す処理
					{
						type_beta = one_before;
						char_toggle = 0;
					}
				}
				else if (char_type == 2)//半濁点も可
				{
					if (char_toggle == 0)//濁点をつける処理
					{
						one_before = type_beta;
						type_beta = type_beta + "゛";
						type_beta = ToPadding(type_beta);
						char_toggle = 1;
					}
					else if (char_toggle == 1)//半濁点をつける処理
					{
						type_beta = one_before;//清音に戻す
						type_beta = type_beta + "゜";
						type_beta = ToPadding(type_beta);
						char_toggle = 2;
					}
					else//濁点から戻す処理
					{
						type_beta = one_before;
						char_toggle = 0;
					}
				}
				else if (char_type == 3)//小文字のみ
				{
					if (char_toggle == 0)//小文字にする処理
					{
						one_before = type_beta;
						type_beta = mini_char(type_beta);
						char_toggle = 1;
					}
					else//濁点から戻す処理
					{
						type_beta = one_before;
						char_toggle = 0;
					}
				}
				else if (char_type == 4)//特殊
				{
					if (type_beta == "つ")
					{
						type_beta = "っ";
					}
					else if (type_beta == "っ")
					{
						type_beta = "づ";
					}
					else if (type_beta == "づ")
					{
						type_beta = "つ";

					}
					else if(type_beta == "う")
					{
						type_beta = "ぅ";
					}
					else if (type_beta == "ぅ")
					{
						type_beta = "ゔ";
					}
					else if (type_beta == "ゔ")
					{
						type_beta = "う";
					}

					else if (char_toggle == 0 && type_beta != "あ" && type_beta != "い" && type_beta != "え" && type_beta != "お")
					{
						one_before = type_beta;
						type_beta = type_beta + "゛";
						type_beta = ToPadding(type_beta);
						char_toggle = 1;
					}
					else if (type_beta == "あ" || type_beta == "い" || type_beta == "え" || type_beta == "お")//小文字にする処理
					{
						one_before = type_beta;
						type_beta = mini_char(type_beta);
						char_toggle = 1;
					}
					else//濁点から戻す処理
					{
						type_beta = one_before;
						char_toggle = 0;
					}
				}
				break;
			default:
				break;
		}
		//Debug.Log("tap type_beta " + type_beta);
		inp_txt.GetComponent<Text>().text = type_beta;

        Q_manager.judge(type_beta);

	}





	public void On_SwipeEnd(int touch_id)
	{

		//Debug.Log("swipe");
		//Debug.Log("swipe " + gesture.pickedObject.name);
		//Debug.Log("swipe  " + gesture.swipeLength);
		//Debug.Log("swipe  " + gesture.swipe);//up 

		switch (touch_line)
		{
			case "あ行":
				type_beta = a_row[touch_id];
				char_toggle_set();
				char_type = 4;
				break;
			case "か行":
				type_beta = ka_row[touch_id];
				char_toggle_set();
				char_type = 1;
				break;
			case "さ行":
				type_beta = sa_row[touch_id];
				char_toggle_set();
				char_type = 1;
				break;
			case "た行":
				type_beta = ta_row[touch_id];
				char_toggle_set();
				char_type = 4;
				break;
			case "な行":
				type_beta = na_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "は行":
				type_beta = ha_row[touch_id];
				char_toggle_set();
				char_type = 2;
				break;
			case "ま行":
				type_beta = ma_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "や行":
				if (touch_id == 2 || touch_id == 4)
				{
					if (touch_id == 2)
					{
						touch_id = 1;
					}
					else if (touch_id == 4)
					{
						touch_id = 2;
					}
					type_beta = ya_row[touch_id];
					char_toggle_set();
					char_type = 3;
				}
				break;
			case "ら行":
				type_beta = ra_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
			case "わ行":
				if (touch_id != 4)
				{
					type_beta = wa_row[touch_id];
					char_toggle_set();
					char_type = 0;
				}
				break;
			case "記号":
				type_beta = mark_row[touch_id];
				char_toggle_set();
				char_type = 0;
				break;
            case "濁点":
                if (char_type == 1)//濁点のみ
                {
                    if (char_toggle == 0)//濁点をつける処理
                    {
                        one_before = type_beta;
                        type_beta = type_beta + "゛";
                        type_beta = ToPadding(type_beta);
                        char_toggle = 1;
                    }
                    else//濁点から戻す処理
                    {
                        type_beta = one_before;
                        char_toggle = 0;
                    }
                }
                else if (char_type == 2)//半濁点も可
                {
                    if (char_toggle == 0)//濁点をつける処理
                    {
                        one_before = type_beta;
                        type_beta = type_beta + "゛";
                        type_beta = ToPadding(type_beta);
                        char_toggle = 1;
                    }
                    else if (char_toggle == 1)//半濁点をつける処理
                    {
                        type_beta = one_before;//清音に戻す
                        type_beta = type_beta + "゜";
                        type_beta = ToPadding(type_beta);
                        char_toggle = 2;
                    }
                    else//濁点から戻す処理
                    {
                        type_beta = one_before;
                        char_toggle = 0;
                    }
                }
                else if (char_type == 3)//小文字のみ
                {
                    if (char_toggle == 0)//小文字にする処理
                    {
                        one_before = type_beta;
                        type_beta = mini_char(type_beta);
                        char_toggle = 1;
                    }
                    else//濁点から戻す処理
                    {
                        type_beta = one_before;
                        char_toggle = 0;
                    }
                }
                else if (char_type == 4)//特殊
                {
                    if (type_beta == "つ")
                    {
                        type_beta = "っ";
                    }
                    else if (type_beta == "っ")
                    {
                        type_beta = "づ";
                    }
                    else if (type_beta == "づ")
                    {
                        type_beta = "つ";

                    }
                    else if (type_beta == "う")
                    {
                        type_beta = "ぅ";
                    }
                    else if (type_beta == "ぅ")
                    {
                        type_beta = "ゔ";
                    }
                    else if (type_beta == "ゔ")
                    {
                        type_beta = "う";
                    }

                    else if (char_toggle == 0 && type_beta != "あ" && type_beta != "い" && type_beta != "え" && type_beta != "お")
                    {
                        one_before = type_beta;
                        type_beta = type_beta + "゛";
                        type_beta = ToPadding(type_beta);
                        char_toggle = 1;
                    }
                    else if (type_beta == "あ" || type_beta == "い" || type_beta == "え" || type_beta == "お")//小文字にする処理
                    {
                        one_before = type_beta;
                        type_beta = mini_char(type_beta);
                        char_toggle = 1;
                    }
                    else//濁点から戻す処理
                    {
                        type_beta = one_before;
                        char_toggle = 0;
                    }
                }
                break;
        }
		Debug.Log("swipe type_beta " + type_beta);
		inp_txt.GetComponent<Text>().text = type_beta;

        Q_manager.judge(type_beta);

	}




	/// <summary>
	/// 濁点を付ける
	/// </summary>
	/// <param name="str">文字</param>
	/// <returns></returns>
	public static string ToPadding(string str)
	{
		if (str == null || str.Length == 0)
		{
			return str;
		}

		char[] cs = new char[str.Length];
		int pos = str.Length - 1;

		int f = str.Length - 1;

		for (int i = f; 0 <= i; i--)
		{
			char c = str[i];

			// ゛(0x309B) 濁点
			if (c == '゛' && 0 < i)
			{
				char c2 = str[i - 1];
				int mod2 = c2 % 2;
				int mod3 = c2 % 3;

				// か(0x304B) ～ ぢ(0x3062)
				// カ(0x30AB) ～ ヂ(0x30C2)
				// つ(0x3064) ～ ど(0x3069)
				// ツ(0x30C4) ～ ド(0x30C9)
				// は(0x306F) ～ ぽ(0x307D)
				// ハ(0x30CF) ～ ポ(0x30DD)
				if (('か' <= c2 && c2 <= 'ぢ' && mod2 == 1) ||
					('カ' <= c2 && c2 <= 'ヂ' && mod2 == 1) ||
					('つ' <= c2 && c2 <= 'ど' && mod2 == 0) ||
					('ツ' <= c2 && c2 <= 'ド' && mod2 == 0) ||
					('は' <= c2 && c2 <= 'ぽ' && mod3 == 0) ||
					('ハ' <= c2 && c2 <= 'ポ' && mod3 == 0))
				{
					cs[pos--] = (char)(c2 + 1);
					i--;
				}
				else
				{
					cs[pos--] = c;
				}
			}
			// ゜(0x309C) 半濁点
			else if (c == '゜' && 0 < i)
			{
				char c2 = str[i - 1];
				int mod3 = c2 % 3;

				// は(0x306F) ～ ぽ(0x307D)
				// ハ(0x30CF) ～ ポ(0x30DD)
				if (('は' <= c2 && c2 <= 'ぽ' && mod3 == 0) ||
					('ハ' <= c2 && c2 <= 'ポ' && mod3 == 0))
				{
					cs[pos--] = (char)(c2 + 2);
					i--;
				}
				else
				{
					cs[pos--] = c;
				}
			}
			else
			{
				cs[pos--] = c;
			}
		}

		return new string(cs, pos + 1, cs.Length - pos - 1);
	}


    /// <summary>
    /// /小文字への変換
    /// </summary>
    /// <param name="type_beta"></param>
    /// <returns></returns>
	string mini_char(string type_beta)
	{
		string mini_char;
		switch (type_beta)
		{
			case "あ":
				mini_char = "ぁ";
					break;
			case "い":
				mini_char = "ぃ";
				break;
			case "う":
				mini_char = "ぅ";
				break;
			case "え":
				mini_char = "ぇ";
				break;
			case "お":
				mini_char = "ぉ";
				break;
			case "つ":
				mini_char = "っ";
				break;
			case "や":
				mini_char = "ゃ";
				break;
			case "ゆ":
				mini_char = "ゅ";
				break;
			case "よ":
				mini_char = "ょ";
				break;
			default:
				mini_char = "";
				break;
		}
		return mini_char;
	}


	/// <summary>
	/// 文字種切り替えをリセット
	/// </summary>
	void char_toggle_set()
	{
		if (char_toggle == 1)
		{
			char_toggle = 0;
		}
	}



	
    


	/*string[] a_row;//あ行
	string[] ka_row ;//か行
	string[] sa_row ;//さ行
	string[] ta_row ;//た行
	string[] na_row ;//な行
	string[] ha_row ;//は行
	string[] ma_row ;//ま行
	string[] ya_row ;//や行
	string[] ra_row ;//ら行
	string[] wa_row ;//わ行
				mark_row//記号
	 * */


        /// <summary>
        /// キー配列の決定
        /// </summary>
        /// <param name="key_type"></param>
	void key_decide(int key_type)
	{
		if (key_type == 1)//google日本語タイプ
		{
			a_row = new string[] {"あ", "い", "う" , "え" , "お" };
			ka_row = new string[] { "か", "き", "く", "け", "こ" };
			sa_row = new string[] { "さ", "し", "す", "せ", "そ" };
			ta_row = new string[] { "た", "ち", "つ", "て", "と" };
			na_row = new string[] { "な", "に", "ぬ", "ね", "の" };
			ha_row = new string[] { "は", "ひ", "ふ", "へ", "ほ" };
			ma_row = new string[] { "ま", "み", "む", "め", "も" };
			ya_row = new string[] { "や", "ゆ", "よ" };
			ra_row = new string[] { "ら", "り", "る", "れ", "ろ" };
			wa_row = new string[] { "わ", "を", "ん", "ー" };
			mark_row = new string[] { "、", "。", "？", "！" };
		}
	}


	

    
}
