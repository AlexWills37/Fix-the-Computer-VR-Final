using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Behavior for a Level Gate, a physical interface for the user to enter levels.
/// Level Gates should have an attached Canvas as the first child.
/// The canvas should have a button to enter the level as the second child.
/// The canvas should have a TextMeshPro to display the level status as the third child.
/// 
/// @author Alex Wills
/// </summary>
public class LevelGateBehavior : MonoBehaviour
{
    [Tooltip("Where the player should spawn when they leave this level.")]
    public Transform playerSpawnLocation;

    // Text to indicate level status
    private TextMeshProUGUI levelStatus;
    private GameObject enterLevelButton;


    public bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = this.transform.GetChild(0);
        levelStatus = canvas.GetChild(2).GetComponent<TextMeshProUGUI>();
        enterLevelButton = canvas.GetChild(1).gameObject;
        
    }

    /// <summary>
    /// Set the level status based on if it is unlocked and completed.
    /// </summary>
    /// <param name="levelComplete"> true if the level has bene completed. </param>
    public void SetLevelCompleteTextActive(bool levelComplete)
    {
        // If level is completed, set the text to reflect it.
        if (levelComplete)
        {
            levelStatus.SetText("Level Complete!");
        }
        // If the level is not complete or unlocked, mark it as such
        else if(!unlocked)
        {
            levelStatus.SetText("Complete the previous level to unlock this one!");
            levelStatus.fontSize = 9f;
            levelStatus.color = new Color(1f, 0.5f, 0.5f);
            enterLevelButton.SetActive(false);
           
        // If the level is unlocked, but not completed, do not display text.
        } else
        {
            levelStatus.SetText("");
        }
    }

    /// <summary>
    /// Access the transform to indicate where the player should spawn when exiting a level.
    /// </summary>
    /// <returns> transform representing where the player should be after exiting the level. </returns>
    public Transform GetLevelLocation()
    {
        // If the transform is not set, use this object's transform.
        if(playerSpawnLocation != null)
        {
            return playerSpawnLocation;
        } else
        {
            return this.gameObject.transform;
        }
    }
}
