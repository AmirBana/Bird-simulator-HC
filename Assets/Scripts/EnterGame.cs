using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class EnterGame : MonoBehaviour
{
    SaveData data;
    int sceneIndex;
    private void Awake()
    {
        LoadGame();
    }
    void Start()
    {

        StartCoroutine(LoadScene());
    }

    void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            sceneIndex = data.sceneIndex;
            print(data);
        }
        else sceneIndex = 1;
    }
    IEnumerator LoadScene()
    {
        var levelSync = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        while (!levelSync.isDone)
            yield return null;
    }
}
