using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチに関する部分を司る
/// </summary>
public class touch_input : MonoBehaviour {


	Touch start_touch;
	Touch move_touch;
	Touch end_touch;

	
	/// <summary>
	/// スワイプと判定される量
	/// </summary>
	[SerializeField]
	float swipe_capacity;

	/// <summary>
	/// スワイプと判定したか
	/// </summary>
	bool swipe_flag = false;

	/// <summary>
	/// スワイプ終了時のx変化量
	/// </summary>
	float x_change = 0;

	/// <summary>
	/// スワイプ終了時のy変化量
	/// </summary>
	float y_change = 0;

	/// <summary>
	/// フリック方向。1左　2上　3右　4下
	/// </summary>
	int flick_angle = 0;

	/// <summary>
	/// タッチ開始時点の座標
	/// </summary>
	Vector2 startpos;

	/// <summary>
	/// 移動中の座標
	/// </summary>
	Vector2 movepos;

	/// <summary>
	/// タッチ終了時点の座標
	/// </summary>
	Vector2 endpos;

	[SerializeField]
	input play;


	// Use this for initialization
	void Start () {
		//Input.simulateMouseWithTouches = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 1)
		{
			touch_start();
			swipe_start();
			touch_end();
			//Debug.Log(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
			
			
		}
		

	}

	/// <summary>
	/// タッチ開始時処理
	/// </summary>
	void touch_start()
	{
		
		start_touch  = Input.GetTouch(0);
		if (start_touch.phase == TouchPhase.Began)
		{
			//Debug.Log("タッチスタート");
			startpos = Camera.main.ScreenToWorldPoint(start_touch.position);
			play.On_TouchStart(startpos);
			
		}
	}



	/// <summary>
	/// スワイプが始まった(＝と判定された)とき
	/// </summary>
	void swipe_start()
	{
		
		if (start_touch.phase == TouchPhase.Moved && swipe_flag == false)
		{
			move_touch = Input.GetTouch(0);
			movepos = Camera.main.ScreenToWorldPoint(move_touch.position);
			//Debug.Log("Mathf.Abs(movepos.x - startpos.x) " + Mathf.Abs(movepos.x - startpos.x));
			//Debug.Log("Mathf.Abs(movepos.y - startpos.y) " + Mathf.Abs(movepos.y - startpos.y));
			if (Mathf.Abs(movepos.x - startpos.x) >= swipe_capacity)//スワイプ量を超えたら
			{
				//Debug.Log("xスワイプ");
				swipe_flag = true;
				//Debug.Log("スワイプスタート");
			}
			else if(Mathf.Abs(movepos.y - startpos.y) >= swipe_capacity)//スワイプ量を超えたら
			{
				//Debug.Log("yスワイプ");
				swipe_flag = true;
				//Debug.Log("スワイプスタート");
			}
		}
	}






	/// <summary>
	/// タッチが終わった＝指を離したとき
	/// </summary>
	void touch_end()
	{
		
		if (start_touch.phase == TouchPhase.Ended)
		{
			Debug.Log("指を離した");
			if (swipe_flag == false)//スワイプせずに指が離れた＝タッチだけ
			{
				swipe_flag = false;
				startpos = new Vector2(0f, 0f);
				endpos = new Vector2(0f, 0f);
                movepos = new Vector2(0f, 0f);
				play.On_TouchUp();
			}
			else if (swipe_flag == true)//スワイプしていた
			{
				swipe_flag = false;
				
				end_touch = Input.GetTouch(0);
				endpos = Camera.main.ScreenToWorldPoint(end_touch.position);
				swipe_end();
			}
			

			
		}
	}


	/// <summary>
	/// スワイプ終了時処理
	/// </summary>
	void swipe_end()
	{
		Debug.Log("swipe_end");
		x_change = (startpos.x - endpos.x);
		y_change = (startpos.y - endpos.y);
		Debug.Log("x_change " + x_change);
		Debug.Log("y_change " + y_change);

		if (Mathf.Abs(x_change)  >  Mathf.Abs(y_change)  || Mathf.Abs(x_change) - Mathf.Abs(y_change) == 0)//x軸の方が変化多いか対角線上をフリック
		{
			if (x_change > 0)//左フリック
			{
				play.On_SwipeEnd(1);
			}
			else//右フリック
			{
				play.On_SwipeEnd(3);
			}
		}
		else
		{
			if (y_change < 0)//上フリック
			{
				play.On_SwipeEnd(2);
			}
			else//下フリック
			{
				play.On_SwipeEnd(4);
			}
		}
		x_change = 0f;
		y_change = 0f;
		startpos = new Vector2(0f, 0f);
		endpos = new Vector2(0f, 0f);
		movepos = new Vector2(0f, 0f);
		
	}





}
