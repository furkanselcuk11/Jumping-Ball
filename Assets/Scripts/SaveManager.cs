﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{

    [Header("Meta")]
    public string saveName;
    [Header("Scriptable Objects")]
    public List<ScriptableObject> objectsToSave;
    private void OnEnable()
    {
        // Veri Cekme        
        for (int i = 0; i < objectsToSave.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", saveName, i)))
            {
                Debug.Log("Veriler Alýndý...");
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", saveName, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToSave[i]);
                file.Close();

            }
            else
            {
                //Do Nothing
                Debug.Log("Daha Onve Veri Kaydedilmedi...");
            }
        }
    }
    private void OnDisable()
    {
        // Veri Kaydetme
        Debug.Log("Veriler Kaydedildi...");
        for (int i = 0; i < objectsToSave.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", saveName, i));
            var json = JsonUtility.ToJson(objectsToSave[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }
}