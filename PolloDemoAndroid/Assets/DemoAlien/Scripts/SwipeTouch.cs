using UnityEngine;
using System.Collections;

public class SwipeTouch : MonoBehaviour {

    //public Text InfoText;

    protected virtual void OnEnable()
    {
        // Hook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        Lean.LeanTouch.OnFingerTap -= OnFingerTap;
    }

    public void OnFingerSwipe(Lean.LeanFinger finger)
    {
        string InfoText = "";
        // Make sure the info text exists
        if (InfoText != null)
        {
            // Store the swipe delta in a temp variable
            var swipe = finger.SwipeDelta;

            if (swipe.x < -Mathf.Abs(swipe.y))
            {
                InfoText = "You swiped left!";
                GetComponent<GameController>().DestroyEnemiesBySwipe();
            }else

            if (swipe.x > Mathf.Abs(swipe.y))
            {
                InfoText = "You swiped right!";
                GetComponent<GameController>().DestroyEnemiesBySwipe();
            }
                else
            if (swipe.y < -Mathf.Abs(swipe.x))
            {
                InfoText = "You swiped down!";
            }
                    else
            if (swipe.y > Mathf.Abs(swipe.x))
            {
                InfoText = "You swiped up!";
            }
            else
            
            {
                            
                
            }

            Debug.Log(InfoText);
        }
    }

    public void OnFingerTap(Lean.LeanFinger finger)
    {
       
    }
}
