using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameData
{
    public int DefaultPlayerHp;
    public float DefaultPlayerSpeed;
    public float DefaultPlayerJumpPower;
}

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public GameData Data { get; private set; }
    public bool IsLoaded { get => Data != null; }

    public static DataManager Get()
    {
        if (_instance == null)
        {
            var go = new GameObject("[DataManager]");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<DataManager>();
        }

        return _instance;
    }

    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        var dataXml = Resources.Load<TextAsset>("Data").text;

        Data = JsonUtility.FromJson<GameData>(dataXml);
    }
}
