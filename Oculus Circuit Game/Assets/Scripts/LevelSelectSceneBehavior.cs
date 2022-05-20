using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the level selection scene and keeps track of which levels have been completed.
/// 
/// @author Alex Wills
/// </summary>
public class LevelSelectSceneBehavior : MonoBehaviour
{
    [Tooltip("Game Object with all level gates as children. Levels should be in order.")]
    public Transform levelGates;

    [Tooltip("Set to true if this instance of the script is in the actual level select scene (as opposed to" +
        "being used in another scene to change the level select scene)")]
    public bool gameObjectInLevelSelectScene = false;

    [Tooltip("Object to activate only after all levels are completed.")]
    public GameObject gameCompleteGate;

    // List of all levels
    private LevelGateBehavior[] levels;

    // Boolean used to initialize the levels in Update() instead of Start(), to ensure all the levels are initialized first
    private bool initialized = false;

    // A static list that persevers between scenes to record which levels have been beaten
    static bool[] levelCompletionList;

    // Indicate which level the player is on to keep track of where the player should spawn when they return to this scene
    static int currentLevel = -1;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        initialized = false;
        player = GameObject.Find("OVRPlayerController");
        gameCompleteGate.SetActive(false);    
    }


    private void Update()
    {
        // One time initialization, after all objects' Start() methods have finished
        if (!initialized)
        {
            initialized = true;

            // Only initialize if this script is in the level select scene
            if (gameObjectInLevelSelectScene)
            {
                // Get all of the levels
                levels = new LevelGateBehavior[levelGates.childCount];
                for(int i = 0; i < levelGates.childCount; i++)
                {
                    levels[i] = levelGates.GetChild(i).GetComponent<LevelGateBehavior>();
                }

                // Set up the static completion list if it is empty
                if (levelCompletionList == null)
                {
                    // Make an entry for each level, and by default set them all to false
                    levelCompletionList = new bool[levels.Length];
                    for (int i = 0; i < levelCompletionList.Length; i++)
                    {
                        levelCompletionList[i] = false;
                    }

                }

                // Unlock the levels that should be unlocked
                DetermineUnlockedLevels();
                
                // Mark the levels' statuses
                for (int i = 0; i < levelCompletionList.Length; i++)
                {
                    levels[i].SetLevelCompleteTextActive(levelCompletionList[i]);
                }

                // Spawn the player where the level gate is
                if (currentLevel >= 0 && currentLevel < levelGates.childCount)
                {
                    player.transform.position = levels[currentLevel].GetLevelLocation().position;
                }

                // Activate the end gate and fix computer if the last level is complete
                if(levelCompletionList[levelCompletionList.Length - 1])
                {
                    gameCompleteGate.SetActive(true);
                    // Start the animation for the gate to rise from the ground
                    gameCompleteGate.GetComponent<Animator>().SetBool("isAnim", true);
                    OfficeSceneBehavior.FixComputer();
                }
            } // end of initialization in scene
        } // end of initialization regardless of scene
    }

    /// <summary>
    /// Go through the levels and mark the ones that are unlocked as unlocked.
    /// Also ensure the tutorial is unlocked.
    /// </summary>
    public void DetermineUnlockedLevels()
    {
        // Start with only the tutorial unlocked
        if(levels.Length > 0)
        {
            levels[0].unlocked = true;
        }

        // Go through the list of completed levels and unlock the next level
        for(int i = 0; i < levelCompletionList.Length - 1; i++)
        {
            // If this level is completed, unlock the next one
            if (levelCompletionList[i])
            {
                levels[i + 1].unlocked = true;
            }
        }
    }

    /// <summary>
    /// Mark a level as completed.
    /// </summary>
    /// <param name="levelIndex"> The index of the level to complete </param>
    public static void CompleteLevel(int levelIndex)
    {
        levelCompletionList[levelIndex] = true;
    }

    /// <summary>
    /// Set the player's current level to remember where to put the player when they return to this scene.
    /// </summary>
    /// <param name="levelIndex"></param>
    public void SetLevelIndex(int levelIndex)
    {
        currentLevel = levelIndex;
    }
}
