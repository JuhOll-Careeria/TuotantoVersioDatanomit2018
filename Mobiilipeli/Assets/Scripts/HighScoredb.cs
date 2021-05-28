using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class HighScoredb : MonoBehaviour
{
    public string userName;
    public string passWord;

    public Text UserNameText;
    public Text PassWordText;


    private void Start()
    {
        ReadDatabase();
    }

    public void ReadLogIn()
    {
        userName = UserNameText.text;
        passWord = PassWordText.text;
        Adduser();
    }


    void ReadDatabase()
    {
        string conn = "URI=file:" + Application.dataPath + "/HighScoredb.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT username, password " + "FROM user";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string username = reader.GetString(0);
            string password = reader.GetString(1);

            Debug.Log("Username: " + username + "  Password: " + password);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    void Adduser()
    {
        string conn = "URI=file:" + Application.dataPath + "/HighScoredb.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery =  "INSERT INTO user VALUES ('" + userName + "', '" + passWord +"')";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string username = reader.GetString(0);
            string password = reader.GetString(1);

            Debug.Log("Username: " + username + "  Password: " + password);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}

