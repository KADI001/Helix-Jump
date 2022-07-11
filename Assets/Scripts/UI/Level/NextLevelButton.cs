using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    private Button _button;
    private ISaveSytem _saveSystem;
    private Scene _activeScene;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _saveSystem = new BinarySaveSystem();
        _activeScene = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnNextLevelButtonClick);
    }

    private void OnNextLevelButtonClick()
    {
        _saveSystem.Save(TowerBuilderData.WithRandomNumberPlatforms, "TowerConfig");
        SceneManager.LoadScene(_activeScene.name);
    }
}
