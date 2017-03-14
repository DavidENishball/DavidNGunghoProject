using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRunner_LevelSpawner : MonoBehaviour {
    [System.Serializable]
    public class SpawnedObjectTimingTracker
    {
        public GameObject prefab;

        // Provide a designated height to avoid situations where hazards are too high to be jumped over and too low to be dodged.
        public List<float> validHeights = new List<float>();
        public float minimumDelay = 1;
        public float maxiimumDelay = 10;
        public float currentTimeLeft;

        public virtual void ResetRemainingTime()
        {
            currentTimeLeft= Random.Range(minimumDelay, maxiimumDelay);
        }
    }

    public bool isOperating;
    // A coefficient to slow down the spawning of obstacles to manageable levels at high game speeds.
    public float spawnSpeedCoefficient = 0.9f;
    [SerializeField]
    public List<SpawnedObjectTimingTracker> PossibleSpawnedObjectsList = new List<SpawnedObjectTimingTracker>();

	// Use this for initialization
	void Start () {
        RoboRunnerSceneManager.Instance.SceneManagerStateChanged += Instance_SceneManagerStateChanged;
	}

    private void Instance_SceneManagerStateChanged(RoboRunnerSceneManager manager, RoboRunnerSceneManager.EGameState previousState)
    {
        if (manager.GameState == RoboRunnerSceneManager.EGameState.PLAYING)
        {
            foreach(SpawnedObjectTimingTracker n in PossibleSpawnedObjectsList)
            {
                n.ResetRemainingTime();
                // Divide times by four to make them appear early at the beginning of the game.
                n.currentTimeLeft /= 4;
            }
            isOperating = true;
        }
        else if (manager.GameState == RoboRunnerSceneManager.EGameState.READY)
        {
            isOperating = false;
        }
        else
        {
            isOperating = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (isOperating)
        {
            foreach (SpawnedObjectTimingTracker n in PossibleSpawnedObjectsList)
            {
                // Scale the spawn speed by how fast the game is going,
                // Include a coefficient to slightly slow down the obstacle spawning.
                float sceneManagerTimeScale = RoboRunnerSceneManager.Instance.scrollSpeed / RoboRunnerSceneManager.Instance.startingScrollSpeed;
                n.currentTimeLeft -= Time.deltaTime * sceneManagerTimeScale * spawnSpeedCoefficient;
                if (n.currentTimeLeft <= 0)
                {
                    SpawnFromTracker(n);
                }
            }
        }
    }

    public void SpawnFromTracker(SpawnedObjectTimingTracker tracker)
    {
        Vector3 spawnPosition = this.transform.position;
        int randomIndex = Random.Range(0, tracker.validHeights.Count);
        spawnPosition.y = tracker.validHeights[randomIndex];
        Instantiate(tracker.prefab, spawnPosition, tracker.prefab.transform.rotation);
        tracker.ResetRemainingTime();
    }
}
