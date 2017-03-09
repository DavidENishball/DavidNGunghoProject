using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRunnerSceneManager : MonoBehaviour {

    public const string PLAYER_CHARACTER_TAG = "Player";

    public enum EGameState
    {
        READY,
        PLAYING,
        GAME_OVER
    }

    public delegate void RoboRunnerSceneManagerDelegate(RoboRunnerSceneManager manager);
    public delegate void RoboRunnerSceneManagerStateDelegate(RoboRunnerSceneManager manager, EGameState previousState );
    public event RoboRunnerSceneManagerStateDelegate SceneManagerStateChanged;

    public EGameState GameState { get; private set; }

    public static RoboRunnerSceneManager Instance;

    [Header("Scrolling")]
    public float startingScrollSpeed = 2;
    public float scrollSpeed = 2f;
    public float scrollSpeedAcceleration = 0.05f;
    public float maximumScrollSpeed = 5;


    [Header("Ready State")]
    public float readyMaxDuration = 2;
    public float readyElapsedTime = 0;

    #region Object References
    [Header ("Object References")]
    public Robo_CharacterController character;
    public Transform playerSpawnTransform;
    #endregion

    #region 
    public float playerScore = 0;
    #endregion

    private void Awake()
    {
        // Singleton.
        
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

    }

    // Use this for initialization
    void Start () {

        character = GameObject.FindObjectOfType<Robo_CharacterController>();

        character.Died += Character_Died;

        ChangeGameState(EGameState.READY);
	}

    private void Character_Died(Robo_CharacterController character)
    {
        ChangeGameState(EGameState.GAME_OVER);
    }

    public void ChangeGameState(EGameState newState)
    {
        EGameState oldState = GameState;
        GameState = newState;

        // Start game: Set player alive.
        // Game Over: Set player dead.  Set speed to zero;

        

        if (SceneManagerStateChanged != null)
        {
            SceneManagerStateChanged.Invoke(this, oldState);
        }

        switch (GameState)
        {
            case EGameState.PLAYING:
                InternalStartPlaying();
                break;
            case EGameState.READY:
                InternalStartReadyState();
                break;
            case EGameState.GAME_OVER:
                InternalStartGameOver();
                break; 
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // For a simple game, a switch statement is enough to handle states.  
        // If the game was more complicated we would use a state machine.
        if (GameState == EGameState.READY)
        {
            UpdateReadyState();
        }
        else if (GameState == EGameState.PLAYING)
        {
            UpdatePlayingState();
        }
        else if (GameState == EGameState.GAME_OVER)
        {
            UpdateGameOverState();
        }
        
	}

    protected virtual void UpdateReadyState()
    {
        readyElapsedTime += Time.deltaTime;
        if (readyElapsedTime > readyMaxDuration)
        {
            ChangeGameState(EGameState.PLAYING);
        }
    }

    protected virtual void UpdatePlayingState()
    {
        // Increase scroll speed until we hit the cap.
        scrollSpeed = Mathf.Min(scrollSpeed + scrollSpeedAcceleration * Time.deltaTime, maximumScrollSpeed);
        playerScore += Time.deltaTime * scrollSpeed;
    }

    protected virtual void UpdateGameOverState()
    {
        // Nothing
    }

    protected virtual void InternalStartPlaying()
    {
        character.allowInput = true;
        scrollSpeed = startingScrollSpeed;
    }

    protected virtual void InternalStartGameOver()
    {
        //character.SetDead(true);
        scrollSpeed = 0;
        
        // Display retry menu.
    }

    protected virtual void InternalStartReadyState()
    {
        ResetGameplayObjects();
        scrollSpeed = 0;

        playerScore = 0;
        readyElapsedTime = 0;
    }

    public virtual void ResetGameplayObjects()
    {
        character.SetDead(false);
        character.allowInput = false;
        character.transform.position = playerSpawnTransform.position;
        character.transform.eulerAngles = new Vector3(0, 90, 0);

    }

    
}
