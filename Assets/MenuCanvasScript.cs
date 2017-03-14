using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCanvasScript : MonoBehaviour {

    public Button startButton;

    public Button quitButton;

    public EventSystem localEventSystem;

    public void BeginButtonClicked()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

	public void QuitButtonClicked()
    {
#if UNITY_EDITOR
        Debug.Log("Attempting to quit game.");
#endif
        Application.Quit();
    }

    private void Update()
    {
        // Prevent players from deselecting a menu item with a click and making the menu unusable.

        // If there was more time I would detect button presses on the game pad.
        if (localEventSystem.currentSelectedGameObject == null)
        {
            localEventSystem.SetSelectedGameObject(startButton.gameObject);
        }
    }
}
