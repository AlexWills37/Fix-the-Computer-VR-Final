/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

See SampleFramework license.txt for license terms.  Unless required by applicable law
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific
language governing permissions and limitations under the license.

************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

// By default, this script uses the "active controller," which is based on the order
// Oculus stores the controllers. 
// I have modified this script to allow for the main controller to switch based on which trigger was
// most recently pressed. Depends on ControllerInputEvents to detect when a trigger is pressed.
public class HandedInputSelector : MonoBehaviour
{
    OVRCameraRig m_CameraRig;
    OVRInputModule m_InputModule;

    void Start()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        m_InputModule = FindObjectOfType<OVRInputModule>();

        SetActiveController(false);
    }

    /// <summary>
    /// Choose which hand is being used for raycasting
    /// </summary>
    /// <param name="leftHand"> true if the left hand should be used for raycasting, false if the right hand should be.</param>
    public void SetActiveController(bool leftHand)
    {
        // Set the input module's starting point based on which hand is chosen.
        Transform t;
        if(leftHand)
        {
            t = m_CameraRig.leftHandAnchor;
        }
        else
        {
            t = m_CameraRig.rightHandAnchor;
        }
        m_InputModule.rayTransform = t;
    }
}
