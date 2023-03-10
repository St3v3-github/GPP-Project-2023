using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    } 

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //animationSnapping
        float snappedHorizontal = 0;
        float snappedVertical = 0;

        #region snapped horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }

        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }

        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }

        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }

        else
        {
            snappedHorizontal = 0;
        }

        #endregion
        #region snapped vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }

        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }

        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }

        else
        {
            snappedVertical = 0;
        }

        #endregion

        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);

    }
}
*/