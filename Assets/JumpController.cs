using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private AnimationController controller;
    void Awake()
    {
        controller = GetComponent<AnimationController>();
    }

    void Update()
    {
        if(controller.velocity[0] == 0 && controller.velocity[1] == 0)
        {
            controller.jump = true;
        }

    }
}
