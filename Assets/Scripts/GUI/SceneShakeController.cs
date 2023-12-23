using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneShakeController : MonoBehaviour
{
    public static SceneShakeController Instance {  get; private set; }
    [SerializeField] private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void ShakeCamera()
    {
        animator.SetTrigger("Shake");
    }
}
