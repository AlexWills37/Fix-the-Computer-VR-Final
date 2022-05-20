using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the tutorial scene to progress the user and display text as the user completes different tasks.
/// The goal is to teach the player how to use the circuit building system and connect the dots.
/// 
/// @author Alex Wills
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // Sounds to play for correct/incorrect circuits
    private AudioSource incorrectSound;
    private AudioSource correctSound;

    // There is a single static TutorialManager for other objects in the tutorial to call its methods
    // Particularly to progress the tutorial
    public static TutorialManager tutorialManager;

    // Text objects to update
    private TextMeshProUGUI taskWall;
    private TextMeshProUGUI mainWall;
    private TextMeshProUGUI truthTable;

    [Header("Inputs and Outputs")]

    [Tooltip("Tutorial input to start with")]
    public TutorialButton input;
    [Tooltip("Tutorial output to start with")]
    public TutorialLight output;

    [Tooltip("Actual input to check")]
    public ToggleInput circuitInput;
    [Tooltip("Actual output to check")]
    public LightOutput circuitOutput;


    [Header("Teaching Components")]

    [Tooltip("Game object to move out once the user progresses through the tutorial")]
    public Transition tutorialComponents;

    
    [Header("Actual Level Components")]

    [Tooltip("Game object to move in (the actual level)")]
    public Transition workbench;
    [Tooltip("NOT gate to activate for the real level")]
    public GameObject notGate;
    [Tooltip("Button to test circuit")]
    public GameObject testCircuitButton;
    [Tooltip("Button to show the user once they beat the level")]
    public GameObject nextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        // Find and initialize all of the components
        taskWall = GameObject.Find("TaskWall").GetComponent<TextMeshProUGUI>();
        mainWall = GameObject.Find("Tutorial Text").GetComponent<TextMeshProUGUI>();
        truthTable = GameObject.Find("Truth Table").GetComponent<TextMeshProUGUI>();
        mainWall.fontSize = 19.6f;
        tutorialManager = this;
        incorrectSound = GameObject.Find("Incorrect").GetComponent<AudioSource>();
        correctSound = GameObject.Find("Correct").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Progress the tutorial to a specific step. Changes text and activates scripts as different steps see fit.
    /// </summary>
    /// <param name="tutorialStep"> The step of the tutorial you are switching to. </param>
    public void ContinueTutorial(int tutorialStep)
    {
        // Once the user presses the button, teach them how to toggle the input
        if (tutorialStep == 1)
        {
            taskWall.SetText("Look back to the left for this task.");
            mainWall.SetText("In front of you are two boxes: an input and an output." +
                "\n\nThe input has a red button. Press the button to turn the input on/off.");
            input.ActivateTutorialStep();

        }

        // Then teach the user how to connect the input and output
        else if (tutorialStep == 2)
        {
            mainWall.fontSize = 14;
            mainWall.SetText("Now we have to connect the input and output." +
                "\n\nAll components, like the input and output, have a sphere to connect to other components." +
                "\n\nClick one sphere to start a connection, then click the other to finish it.");
            output.ActivateTutorialStep();
        }

        // Then transition to the real level
        else if (tutorialStep == 3)
        {
            // In this step, we move the large input/output off screen.
            tutorialComponents.MoveToDestination();
            output.DestroyLine();

            
        } else if (tutorialStep == 4)
        {
            // In this step, we bring the real workbench to the screen and let the user build their circuit.
            mainWall.SetText("Let's make a circuit!" +
                "\n\nTeleport over to this workstation." +
                "\nHold down the left thumbstick to aim your teleport, then let go to teleport.");
            workbench.MoveToDestination();

        } else if (tutorialStep == 5)
        {
            // In this step, we activate the circuit-checking button, allowing the user to finish the level.
            taskWall.SetText("Goal: NOT Circuit" +
                "\n\nA circuit that inverts the input!" +
                "\nPress the button below when you are finished.");
            notGate.SetActive(true);
            testCircuitButton.SetActive(true);
        }
    }

    /// <summary>
    /// Test the circuit for correctness (in this case, a NOT gate)
    /// </summary>
    public void TestCircuit()
    {
        // Test every possible input combination
        bool circuitWrong = false;

        // Input off, output should be on
        string table = "| off  ||";
        circuitInput.SetSignal(false);
        if(circuitOutput.GetSignal() == false)
        {
            circuitWrong = true;
            table += " off  |";
        } else
        {
            table += "  on |";
        }

        // Input on, output should be off
        table += "\n|  on ||";
        circuitInput.SetSignal(true);
        if(circuitOutput.GetSignal() == true)
        {
            circuitWrong = true;
            table += "  on |";
        } else
        {
            table += " off  |";
        }

        // If circuit is incorrect, play an error sound
        if (circuitWrong)
        {
            incorrectSound.time = 1;
            incorrectSound.Play();
        } else
        // If circuit is correct, play a congratulatory sound and complete the level
        {
            correctSound.Play();
            nextLevelButton.SetActive(true);
            LevelSelectSceneBehavior.CompleteLevel(0);
        }

        // Show the user the truth table of their circuit
        truthTable.SetText("Your Circuit:" +
            "\n|  in  || out |\n" + table);
    }
}
