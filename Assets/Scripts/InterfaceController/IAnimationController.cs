using UnityEngine;

public interface IAnimationController 
{
    Animator animator { get; set; }

    void Running_Anim();
    void Idle_Anim();
}
