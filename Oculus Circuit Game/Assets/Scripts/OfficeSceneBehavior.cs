using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the office scene to display the proper text and butotns.
/// 
/// @author Alex Wills
/// </summary>
public class OfficeSceneBehavior : MonoBehaviour
{
    [Tooltip("Set to true if this instance of the script is in the actual office scene (as opposed to" +
        "being used in another scene to change the office scene)")]
    public bool gameObjectInOfficeScene = false;

    // Static bool used to keep track of the game's completion status
    private static bool computerIsBroken = true;

    // Game Objects and components to change whether the computer is fixed or not.
    private ParticleSystem computerSmoke;
    private TextMeshProUGUI wallText;
    private GameObject closeGameButton;
    private TextMeshProUGUI enterComputerText;
    private GameObject desktopImage;


    // Start is called before the first frame update
    void Start()
    {
        // Only initialize and change objects if this script is in the office scene.
        if (gameObjectInOfficeScene)
        {
            // Find all of the objects to manage
            computerSmoke = GameObject.Find("Computer Smoke").GetComponent<ParticleSystem>();
            wallText = GameObject.Find("Main Text").GetComponent<TextMeshProUGUI>();
            closeGameButton = GameObject.Find("Close Game Button");
            enterComputerText = GameObject.Find("Enter Computer Button").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            desktopImage = GameObject.Find("Desktop Image");

            // If computer is fixed, change the text, remove the smoke, and turn the computer on!
            if (!computerIsBroken)
            {
                computerSmoke.Stop();
                wallText.SetText("Congratulations! It seems your computer is working perfectly again!");
                closeGameButton.SetActive(true);
                enterComputerText.SetText("Enter the computer (again)");
                enterComputerText.fontSize = 13f;
            } else
            {
                closeGameButton.SetActive(false);
                desktopImage.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set the game's status to complete, so that when you enter the office again, the computer is fixed.
    /// </summary>
    public static void FixComputer()
    {
        computerIsBroken = false;
    }


}
