using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSimpleTap : MonoBehaviour {
    public scrPlayerControll player;
    public GameObject Prefab;

    protected virtual void OnEnable()
    {
        // Hook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
        //Lean.LeanTouch.OnFingerSwipe    += OnFingerSwipe;
        //Lean.LeanTouch.OnSoloDrag       += OnSoloDrag;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap -= OnFingerTap;
        //Lean.LeanTouch.OnFingerSwipe    -= OnFingerSwipe;
        //Lean.LeanTouch.OnSoloDrag       -= OnSoloDrag;
    }

    public void OnFingerTap(Lean.LeanFinger finger)
    {
        // Does the prefab exist?
        if (Prefab != null)
        {
           
        }

        // Make sure the finger isn't over any GUI elements
        if (finger.IsOverGui == false)
        {
            // Clone the prefab, and place it where the finger was tapped
            //var position = finger.GetWorldPosition(50.0f);
            //var rotation = Quaternion.identity;
            if (player.arms.isLaserEnable) {
                player.PlayerShoot(finger);
                Debug.Log("Finger " + finger.Index + " tapped the screen");
            }
            
            //var clone    = (GameObject)Instantiate(Prefab, position, rotation);

            // Make sure the prefab gets destroyed after some time
            //Destroy(clone, 2.0f);
        }
    }


//    public void OnFingerSwipe(Lean.LeanFinger finger)
//    {
//        Debug.Log("Finger " + finger.Index + " swiped the screen");
//    }

//    public void OnSoloDrag(Vector2 pixels)
//    {
//        Debug.Log("One finger moved " + pixels + " across the screen");
//
//    }
}
