using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoboRunnerUICanvasScript : MonoBehaviour {

    public const string SCORE_FORMAT = "SCORE: {0}";
    public Text scoreDisplay;
    public GameObject gameOverMenuRoot;
    

	// Use this for initialization
	void Start () {
        RoboRunnerSceneManager.Instance.SceneManagerStateChanged += Instance_SceneManagerStateChanged;
	}

    private void Instance_SceneManagerStateChanged(RoboRunnerSceneManager manager, RoboRunnerSceneManager.EGameState previousState)
    {
        if (manager.GameState == RoboRunnerSceneManager.EGameState.GAME_OVER)
        {
            gameOverMenuRoot.SetActive(true);
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
