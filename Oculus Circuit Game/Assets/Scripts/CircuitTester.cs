using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [UNUSED]
/// This class is not currently in use, but would serve as an abstraction for level design.
/// The idea is that you give this object a Truth Table (see TruthTable.cs) with the solution,
/// give this object the inputs and outputs, and you call a single method, TestCircuit() to 
/// check if the level is complete/incomplete.
/// 
/// Not implemented due to time, and due to the surprising complexity of making a flexible truth table.
/// Refactoring the game to use this class and make a Level prefab is a high priority for future work.
/// 
/// @author Alex Wills
/// </summary>
public class CircuitTester : MonoBehaviour
{
    [Tooltip("All inputs that this circuit uses, in the order they appear in the truth table.")]
    public ToggleInput[] inputs;
    [Tooltip("All outputs that this circuit uses, in the order they appear in the truth table.")]
    public LightOutput[] outputs;

    [Tooltip("Truth Table containing the correct solution for this circuit.")]
    public TruthTable solution;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Tests the circuit and returns a truth table
    /// </summary>
    /// <returns> truth table where each column is an input/output (last column(s) are output(s)) </returns>
    
}
