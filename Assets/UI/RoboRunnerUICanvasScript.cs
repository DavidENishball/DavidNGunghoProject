using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RoboRunnerUICanvasScript : MonoBehaviour {

    public const string SCORE_FORMAT = "SCORE: {0}";
    public Text scoreDisplay;
    public GameObject gameOverMenuRoot;
    public EventSystem localEventSystem;
    public Text readyPrompt;
    public Button RetryButton;

	// Use this for initialization
	void Start () {
        RoboRunnerSceneManager.Instance.SceneManagerStateChanged += Instance_SceneManagerStateChanged;
	}

    private void Instance_SceneManagerStateChanged(RoboRunnerSceneManager manager, RoboRunnerSceneManager.EGameState previousState)
    {
        if (manager.GameState == RoboRunnerSceneManager.EGameState.GAME_OVER)
        {
            gameOverMenuRoot.SetActive(true);
            RetryButton.Select();
            scoreDisplay.gameObject.SetActive(false);
        }
        else
        {
            gameOverMenuRoot.SetActive(false);
            scoreDisplay.gameObject.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update () {
		if (RoboRunnerSceneManager.Instance != null)
        {
            // String Code F0 means to show zero decimal places for a float.
            scoreDisplay.text = string.Format(SCORE_FORMAT, RoboRunnerSceneManager.Instance.playerScore.ToString("F0"));
        }

        // Prevent players from deselecting a menu item with a click and making the menu unusable.

        // If there was more time I would detect button presses on the game pad.
        if (localEventSystem.currentSelectedGameObject == null)
        {
            localEventSystem.SetSelectedGameObject(RetryButton.gameObject);
        }
    }

    public void RetryButtonClicked()
    {
        RoboRunnerSceneManager.Instance.ChangeGameState(RoboRunnerSceneManager.EGameState.READY);
        
    }

    public void QuitButtonClicked()
    {
        // Go to original scene.
    }
}
