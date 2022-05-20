using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Defines the NAND level behavior.
/// Includes the test for whether the circuit is correctly built or not, and sounds for feedback.
/// 
/// @author Alex Wills
/// </summary>
public class NANDLevel : MonoBehaviour
{
    [Tooltip("The inputs in this level.")]
    public ToggleInput[] inputs;
    [Tooltip("The outputs in this level.")]
    public LightOutput output;
    [Tooltip("The button to activate when the level is complete.")]
    public GameObject exitLevelButton;

    private TextMeshProUGUI truthTableText;
    private AudioSource incorrectSound;
    private AudioSource correctSound;


    // Start is called before the first frame update
    void Start()
    {
        truthTableText = GameObject.Find("Truth Table").GetComponent<TextMeshProUGUI>();

        incorrectSound = GameObject.Find("Incorrect").GetComponent<AudioSource>();
        correctSound = GameObject.Find("Correct").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Test the circuit for completeness.
    /// </summary>
    public void TestCircuit()
    {
        bool circuitWrong = false;
        string truthTable = "Your Circuit:\n| A | B || out |";

        // Figure out if the circuit works as intended, testing all inputs and checking their outputs.
        // 0, 0
        truthTable += "\n| 0 | 0 ||";
        inputs[0].SetSignal(false);
        inputs[1].SetSignal(false);
        if (output.GetSignal())
        {
            truthTable += "  1 |";
        } else
        {
            truthTable += "  0 |";
            circuitWrong = true;
        }

        // 0, 1
        truthTable += "\n| 0 | 1 ||";
        inputs[1].SetSignal(true);
        if (output.GetSignal())
        {
            truthTable += "  1 |";
        }
        else
        {
            truthTable += "  0 |";
            circuitWrong = true;
        }

        // 1, 0
        truthTable += "\n| 1 | 0 ||";
        inputs[0].SetSignal(true);
        inputs[1].SetSignal(false);
        if (output.GetSignal())
        {
            truthTable += "  1 |";
        }
        else
        {
            truthTable += "  0 |";
            circuitWrong = true;
        }

        // 1, 1
        truthTable += "\n| 1 | 1 ||";
        inputs[1].SetSignal(true);
        if (output.GetSignal())
        {
            truthTable += "  1 |";
            circuitWrong = true;
        }
        else
        {
            truthTable += "  0 |";
        }

        // Display the results of the check
        truthTableText.SetText(truthTable);

        // If the circuit is wrong, play the incorrect sound.
        if (circuitWrong)
        {
            incorrectSound.time = 1;
            incorrectSound.Play();
        } else
        // If the circuit is correct, change the text and congratulate the player
        {
            correctSound.Play();
            truthTableText.SetText("Congratulations! You have completed this level!");
            LevelSelectSceneBehavior.CompleteLevel(1);
            exitLevelButton.SetActive(true);
        }


    }
}
