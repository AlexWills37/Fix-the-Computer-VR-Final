using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switch between materials to reflect change in a game object's status.
/// 
/// @author Alex Wills
/// </summary>
public class ChangeColor : MonoBehaviour
{
    // Materials to switch between
    [Tooltip("Material to use when this object is off.")]
    public Material offMaterial;
    [Tooltip("Material to use when this object is on.")]
    public Material onMaterial;

    // Mesh renderer containing the object's material
    private MeshRenderer meshRender;

    // Boolean to store which material this object should display
    private bool isOnMaterial = false;


    // Start is called before the first frame update
    void Start()
    {
        this.meshRender = this.GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Switch from the current material to the other one to indicate change.
    /// </summary>
    public void ToggleMaterial()
    {
        // Toggle a boolean
        this.isOnMaterial = !this.isOnMaterial;

        // Set material based on the boolean
        UpdateMaterial();
    }

    /// <summary>
    /// Set the material based on a boolean value.
    /// </summary>
    /// <param name="onMaterial"> true to use "onMaterial", false to use "offMaterial" </param>
    public void SetMaterialState(bool onMaterial)
    {
        this.isOnMaterial = onMaterial;

        UpdateMaterial();
    }

    /// <summary>
    /// Update the material to the current state, in case the material was changed through a different means
    /// </summary>
    public void UpdateMaterial()
    {
        if (this.isOnMaterial)
        {
            this.meshRender.material = this.onMaterial;
        }
        else
        {
            this.meshRender.material = this.offMaterial;
        }
    }

    /// <summary>
    /// Set the material to one not in the provided list
    /// </summary>
    /// <param name="newMaterial"> the new material for the object </param>
    public void SetMaterial(Material newMaterial)
    {
        this.meshRender.material = newMaterial;
    }
}
