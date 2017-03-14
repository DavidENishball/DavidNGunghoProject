using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLevelScrolling : MonoBehaviour {

    // Static sets for tracking members.
    public static readonly HashSet<FakeLevelScrolling> GameplayObjectSet = new HashSet<FakeLevelScrolling>();
    public static readonly HashSet<FakeLevelScrolling> CosmeticObjectSet = new HashSet<FakeLevelScrolling>();

    public bool isCosmetic = false;
    public bool wrapWhenExitingSceneBounds = false;
    public bool debugDontDestroyOnSceneStart = false;

    public Vector3 simulatedVelocity;

    private void Awake()
    {
        // Add to static tracking
        if (isCosmetic)
        {
            CosmeticObjectSet.Add(this);
        }
        else
        {
            GameplayObjectSet.Add(this);
        }

        if (RoboRunnerSceneManager.Instance != null)
        {
            RoboRunnerSceneManager.Instance.SceneManagerStateChanged += Instance_SceneManagerStateChanged;
        }
    }

    private void Instance_SceneManagerStateChanged(RoboRunnerSceneManager manager, RoboRunnerSceneManager.EGameState previousState)
    {
        // Make every gameplay-related object reset on spawn.
        if (manager.GameState == RoboRunnerSceneManager.EGameState.READY  && previousState != RoboRunnerSceneManager.EGameState.READY  && !this.isCosmetic)
        {
            //Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update ()
    {
        if (RoboRunnerSceneManager.Instance == null)
        {
            return;
        }
        // Forced scrolling offset.
        Vector3 scrolledOffset = Vector3.left * RoboRunnerSceneManager.Instance.scrollSpeed * Time.deltaTime;
        Vector3 simulatedVelocityOffset = simulatedVelocity * Time.deltaTime;

        transform.position += (scrolledOffset + simulatedVelocityOffset);
	}

    void OnDestroy()
    {
        CosmeticObjectSet.Remove(this);
        GameplayObjectSet.Remove(this);
    }

    protected void ExitedCameraView()
    {
        if (wrapWhenExitingSceneBounds)
        {
            // Add Wrap function.
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
