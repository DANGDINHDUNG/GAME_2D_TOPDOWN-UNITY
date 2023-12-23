using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] public ScheduleList_SO schedule;

    NavMeshAgent agent;
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private Days day;
    [SerializeField] private Weather weather;
    [SerializeField] private Season season;
    [SerializeField] private float lastXPos, lastYPos = 0;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 movementInput;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        foreach (var pair in schedule.ScheduleList)
        {
            if (hour == pair.Hour && minute == pair.Minutes && day == pair.Day && season == pair.Season)
            {
                agent.SetDestination(new Vector3(pair.Location.X, pair.Location.Y, 0));
                Animate();
            }
        }
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        hour = dateTime.Hour;
        minute = dateTime.Minute;
        day = dateTime.Day;
        season = dateTime.Season;
        //weather = dateTime
    }

    void Animate()
    {
        Vector3 normalizedMovement = agent.desiredVelocity.normalized;

        Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);

        Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

        float forwardVelocity = Vector3.Dot(forwardVector, transform.forward);

        float rightVelocity = Vector3.Dot(rightVector, transform.right);

        movementInput.y = forwardVelocity;
        movementInput.x = rightVelocity;

        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animator.SetInteger("State", 0);
            animator.SetFloat("Horizontal", lastXPos);
            animator.SetFloat("Vertical", lastYPos);
        }
        else
        {
            lastXPos = movementInput.x;
            lastYPos = movementInput.y;
            animator.SetInteger("State", 1);
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
        }
    }

}
