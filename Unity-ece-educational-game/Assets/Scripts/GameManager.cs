using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public int chapter = 1;

    public int currentLevel = 1;
#if UNITY_EDITOR
    [SerializeField]
    SceneAsset[] levelScenes;
#endif
    [SerializeField]
    Text levelText;
    [SerializeField]
    Canvas levelClearScreen;
    public UnityEvent OnLevelClear;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SetLevel();
        levelText.text = string.Format("Chapter {0} Resistor - Level {1}", chapter, currentLevel);
        OnLevelClear.AddListener(SetLevelClearScreen);
    }

    public void LevelUp()
    {
        currentLevel++;
        SetLevel();
    }

    private void SetLevel()
    {
        levelClearScreen.enabled = false;
#if UNITY_EDITOR
        if (currentLevel - 1 < levelScenes.Length)
        {
            levelText.text = string.Format("Chapter {0} Resistor - Level {1}", chapter, currentLevel);
            SceneManager.LoadScene(levelScenes[currentLevel - 1].name);
        }
#endif

#if !UNITY_EDITOR
//quick solution. need better one
        if (currentLevel <= 5)
                {
                    levelText.text = string.Format("Chapter {0} Resistor - Level {1}", chapter, currentLevel);
                    SceneManager.LoadScene(currentLevel);
                }
#endif
    }
    private void SetLevel(int level)
    {
        currentLevel = level;
        SetLevel();
    }


#if !PLATFORM_WEBGL
    private async void SetLevelClearScreen()
    {
        await Task.Delay(1000);
        levelClearScreen.enabled =true;
    }
# endif

#if PLATFORM_WEBGL
    private void SetLevelClearScreen()
    {
        levelClearScreen.enabled =true;
    }
# endif
}
