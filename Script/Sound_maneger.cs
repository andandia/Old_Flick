using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中の音に関することを司る
/// </summary>
public class Sound_maneger : MonoBehaviour {


    [SerializeField]
    AudioSource button_se;

    [SerializeField]
    AudioSource finish_se;


    [SerializeField]
    AudioSource play_bgm;

    [SerializeField]
    AudioSource ResultBgm;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //ボタンseの再生
    public  void play_button_se()
    {
        button_se.Play();
    }

    //終了時のse再生
    public  void play_finish_se()
    {
        finish_se.Play();
    }



    //タイピング中のbgm再生
    public void play_bgm_play()
    {
        play_bgm.Play();
    }

    //タイピング中のbgm停止
    public void play_bgm_stop()
    {
        play_bgm.Stop();
    }

    //終了時のse再生
    public void play_ResultBgm_play()
    {
        ResultBgm.Play();
    }



    public void bgm_pause(bool play)
    {
        if (play == false)
        {
            play_button_se();
            play_bgm.Pause();

        }
        else
        {
            play_button_se();
            play_bgm.UnPause();
        }
    }


}
