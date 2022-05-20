using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [UNUSED]
/// Represents a truth table for specifying logic between input and output.
/// 
/// This file has nothing because it is mostly for future work.
/// I started to design the circuit project with this abstraction in mind, but the design problem
/// proved to be much more difficult and time-consuming than anticipated (specifically having a variable
/// number of inputs and outputs).
/// 
/// This class would have a number of inputs and outputs, and for every possible combination of inputs, the outputs
/// would be specified.
/// 
/// This class would be used by CircuitTester.cs to test inputs and outputs to see if they match the solution,
/// greatly reducing the code volume by getting rid of the need for an if-statement for every input combination.
/// 
/// This class would also be used by CircuitComponent.cs to define a circuit's behavior (using TruthTable as a field, 
/// instead of making CircuitComponent abstract and implementing a different file for each logic gate). This would
/// make it easy to create custom user-generated circuit components.
/// 
/// @author Alex Wills
/// </summary>
public class TruthTable
{
   

    public TruthTable()
    {

    }
}
