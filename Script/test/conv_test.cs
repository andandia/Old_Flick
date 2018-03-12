using UnityEngine;
using System.Collections;

//清音→濁音半濁音 小文字変換テストクラス


public class conv_test : MonoBehaviour {

    [SerializeField]
    string throw_chara;

	// Use this for initialization
	void Start () {
        Debug.Log(ToPadding(throw_chara));
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

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
}
