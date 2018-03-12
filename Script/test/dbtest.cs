using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dbtest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        dbtests();
    }


    // Update is called once per frame
    void Update()
    {

    }



    private void dbtests()
    {
        SqliteDatabase sqlDB = new SqliteDatabase("word.db");
        string query = "select 文字数,表示文字,ひらがな from Sheet1 where id=2";
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        int char_num = 0;
        string disp_name = "";
        string hiragana = "";
        foreach (DataRow dr in dataTable.Rows)
        {
            char_num = (int)dr["文字数"];
            disp_name = (string)dr["表示文字"];
            hiragana = (string)dr["ひらがな"];
            Debug.Log("char_num:" + char_num + "  disp_name: " + disp_name + " hiragana " + hiragana);
        }
    }
}
