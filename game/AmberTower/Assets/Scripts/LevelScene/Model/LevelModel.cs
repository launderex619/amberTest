using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LevelModel
{
    public string status;
    public int results;
    public Data data;

    public static LevelModel CreateFromJSON (string jsonContent) {
        return JsonUtility.FromJson<LevelModel>(jsonContent);
    }

    [Serializable]
    public class InnerData
    {
        public string image;
        public string _id;
        public int index;
        public string name;
        public string description;
        public int __v;

    }

    [Serializable]
    public class Data
    {
        public List<InnerData> data;
    }
}