using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public int SceneBuildIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneBuildIndex);
    }
}
