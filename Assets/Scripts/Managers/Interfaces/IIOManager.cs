using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Utilizes a serializable dictionary based on Open-Source projects:
 * 
 * https://github.com/neuecc/SerializableDictionary
 * 
 * */
public interface IIOManager {
    T GetData<T>(string dataKey);
    bool PutData<T>(string dataKey, T data, bool overwrite);
    bool SaveGameData();
    bool LoadGameData();
}
