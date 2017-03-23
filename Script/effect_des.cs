using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 作ったエフェクトを消す
/// </summary>
public class effect_des : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 0.5f);
    }

    
}
