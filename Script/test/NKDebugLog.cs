/*
 * NKDebugLog.cs
 * 
 * Copyright (c) 2016 by nanoka All rights reserved.
 * Created by nanoka on 2016/11/08.
 * 
 * Qiita  ：http://qiita.com/nanokanato
 * Twitter：https://twitter.com/nanokanato
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NKDebugLog : MonoBehaviour
{

    public Vector2 guiScreenSize = new Vector2(720, 720);
    public bool DebugMode = true;

    private List<string> conditions = new List<string>();
    private List<LogType> logTypes = new List<LogType>();

    /*-----------------------*
     * Default Method
     *-----------------------*/
    //Debug.Logを取得するためのHandler登録
    void Awake()
    {
        if (DebugMode)
        {
            Application.logMessageReceived += LogCallBackHandler;
        }
    }

    //NKDebugLogを表示するためのWindowを表示
    void OnGUI()
    {
        if (DebugMode)
        {
            GUIUtility.ScaleAroundPivot(new Vector2(Screen.width / guiScreenSize.x, Screen.height / guiScreenSize.y), Vector2.zero);
            GUI.ModalWindow(7, new Rect(guiScreenSize.x * 0.1f, guiScreenSize.y * 0.1f, guiScreenSize.x * 0.8f, guiScreenSize.y * 0.8f), modalWindowFunction, "NKDebugLog");
            GUI.matrix = Matrix4x4.identity;
        }
    }

    //NKDebugLogのWindowをレイアウト
    private void modalWindowFunction(int id)
    {
        if (DebugMode)
        {
            if (id == 7)
            {
                //LogType.Logの時のStyle
                GUIStyleState logState = new GUIStyleState();
                logState.textColor = new Color(255, 255, 255, 255);
                GUIStyle logStyle = new GUIStyle();
                logStyle.fontSize = 20;
                logStyle.normal = logState;

                //LogType.Errorの時のStyle
                GUIStyleState errorState = new GUIStyleState();
                errorState.textColor = new Color(255, 0, 0, 255);
                GUIStyle errorStyle = new GUIStyle();
                errorStyle.fontSize = 20;
                errorStyle.normal = errorState;

                //Logを保存した分表示する
                string[] conditionArray = conditions.ToArray();
                LogType[] logTypeArray = logTypes.ToArray();
                for (int i = conditionArray.Length - 1; i > 0; i--)
                {
                    string condition = conditionArray[i];
                    LogType logType = LogType.Log;
                    if (i < logTypeArray.Length)
                    {
                        logType = logTypeArray[i];
                    }
                    if (logType == LogType.Error)
                    {
                        GUI.Label(new Rect(30, (conditionArray.Length - i) * 30, 300, 30), condition, errorStyle);
                    }
                    else
                    {
                        GUI.Label(new Rect(30, (conditionArray.Length - i) * 30, 300, 30), condition, logStyle);
                    }
                }
            }
        }
    }

    /*-----------------------*
     * Application Method
     *-----------------------*/
    //Debug.Logの情報を保存する
    void LogCallBackHandler(string condition, string stackTrace, LogType type)
    {
        if (DebugMode)
        {
            // 取得したログの情報を処理する
            conditions.Add(condition);
            logTypes.Add(type);
        }
    }
}