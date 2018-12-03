using SerializableCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class IOManager : MonoBehaviour, IIOManager {
    //game dictionary to be saved/loaded
    private SerializableDictionary<string,object> gameData;

    //Singleton enforcement
    private static IOManager instance;
    public static IOManager GetInstance() {
        return instance;
    }

    [SerializeField]
    private string gameDataFilename = "gamedata.dat";
    public void SetGameDataFilename(string filename) 
        { gameDataFilename = filename; }
    public string GetGameDataFilename() 
        { return gameDataFilename; }

    private string gameDataFilepath;

    public void Awake() {
        if (instance == null) {
            instance = this;

            gameDataFilepath = Application.persistentDataPath + Path.DirectorySeparatorChar + gameDataFilename;
            LoadGameData();
        } else {
            Debug.Log("A singleton of type \"IOManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }

    public T GetData<T>(string dataKey) {
        if (gameData.ContainsKey(dataKey)) {
            return (T)gameData[dataKey];
        } else {
            return default(T);
        }
    }

    public bool PutData<T>(string dataKey, T data, bool overwrite) {
        if (gameData.ContainsKey(dataKey) && !overwrite) {
            return false;
        } else {
            gameData[dataKey] = data;
            return true;
        }
    }

    public bool SaveGameData() {
        try {
            BinaryFormatter bf = new BinaryFormatter();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
            FileStream file = File.Create(gameDataFilepath); //you can call it anything you want
            bf.Serialize(file, gameData);
            file.Close();
            return true;
        } catch (Exception e) {
            Debug.Log("Exception saving game data: "+e.Message);
            return false;
        }
    }

    public bool LoadGameData() {
        if (File.Exists(gameDataFilepath)) {
            try {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(gameDataFilepath, FileMode.Open);
                gameData = (SerializableDictionary<string, object>)bf.Deserialize(file);
                file.Close();
                return true;
            } catch(Exception e) {
                Debug.Log("Exception loading game data: "+e.Message);
                return false;
            }
        } else {
            if(gameData == null) {
                gameData = new SerializableDictionary<string, object>(100);
            }
            return false;
        }
    }
}
