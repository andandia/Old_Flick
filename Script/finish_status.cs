using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 終了時の状態を保存するスクリプト
/// </summary>
public class finish_status : MonoBehaviour {

	/// <summary>
	/// 終了時のスコア
	/// </summary>
	[SerializeField]
	public int total_score;
	[SerializeField]
    public int total_level;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void finish_state(int score , int level)
	{
		total_score = score;
		total_level = level;
	}
	
	
	
}
