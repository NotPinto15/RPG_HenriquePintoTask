using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions input;

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;

    // Set the LayerMask for clickable areas (ensure the "Movable" layer is selected in the Inspector)
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    // Threshold distance to determine when the player is "close enough" to the target
    [SerializeField] float stopDistanceThreshold = 0.1f;

    // Audio
    [Header("Audio")]
    [SerializeField] AudioClip walkClip; // Walking sound clip
    [SerializeField] AudioClip idleClip; // Idle sound clip
    private AudioSource audioSource;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure AudioSource component is attached

        input = new CustomActions();
        AssignInputs();
    }

    // Assign input actions
    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    // Handle click-to-move functionality
    void ClickToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // Create a ray from the camera to the mouse position
        RaycastHit hit;

        // Attempt to raycast to objects in the "Movable" layer
        if (Physics.Raycast(ray, out hit, 100f, clickableLayers))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);  // Log the name of the hit object for debugging

            agent.destination = hit.point;  // Set the destination to the clicked point

            // Instantiate a click effect where the player clicked (optional)
            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit a valid object on the 'Movable' layer.");
        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        FaceTarget();
        SetAnimations();
        HandleAudio();
    }

    // Rotate the player towards the movement direction
    void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    // Handle animations based on movement
    void SetAnimations()
    {
        // Use the remaining distance to decide if the player is still moving towards the target
        if (agent.remainingDistance > stopDistanceThreshold && !agent.pathPending)
        {
            // Play walk animation if the player is still moving and not too close to the destination
            animator.Play(WALK);
        }
        else
        {
            // Play idle animation if the player has reached the destination or stopped moving
            animator.Play(IDLE);
        }
    }

    // Handle audio based on movement
    void HandleAudio()
    {
        if (agent.remainingDistance > stopDistanceThreshold && !agent.pathPending)
        {
            // If the player is walking and the walk sound is not playing, start playing it
            if (!audioSource.isPlaying || audioSource.clip != walkClip)
            {
                audioSource.clip = walkClip;
                audioSource.loop = true; // Loop the walking sound
                audioSource.Play();
            }
        }
        else
        {
            // If the player is idle and the idle sound is not playing, play the idle sound
            if (!audioSource.isPlaying || audioSource.clip != idleClip)
            {
                audioSource.clip = idleClip;
                audioSource.loop = false; // Don't loop idle sound (play once)
                audioSource.Play();
            }
        }
    }
}