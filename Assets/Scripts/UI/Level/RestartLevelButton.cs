using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartLevelButton : MonoBehaviour
{
    private Scene _activeScene;
    private Button _button;

    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnRestartButtonClick);

    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(_activeScene.buildIndex);
    }
}
