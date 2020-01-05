using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyAnimatorHolder : MonoBehaviour
{
    public void animate()
    {

        Animator anim = GetComponent<Animator>();
        anim.Play("SnakeBodyAnim01");
      
    }

    public void animateHead()
    {

        Animator anim = GetComponent<Animator>();
        anim.Play("SnakeHeadAnim01");
    }

}
