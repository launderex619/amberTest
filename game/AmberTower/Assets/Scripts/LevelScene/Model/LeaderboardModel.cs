using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardModel
{
    public string status;
    public int results;
    public Data data;
    public static LeaderboardModel CreateFromJSON(string jsonContent) {
        return JsonUtility.FromJson<LeaderboardModel>(jsonContent);
    }

    [Serializable]
    public class InnerData
    {
        public string _id;
        public string player;
        public int score;
        public string level_id;
        public int __v;

    }

    [Serializable]
    public class Data
    {
        public List<InnerData> data;
    }

}