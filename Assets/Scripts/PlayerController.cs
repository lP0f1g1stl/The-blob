using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 mousePos1;
    [SerializeField] private Vector2 mousePos2;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject trajectoryPoint;

    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] private GameObject[] trajectoryPoints = new GameObject[10];

    private Vector2 trajectory;
    private Vector2 initialSpeed;

    private bool isPlaying = true;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        for (int i = 0; i < 10; i++)
        {
            trajectoryPoints[i] = Instantiate(trajectoryPoint, new Vector2(-100, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        camera.transform.position = new Vector3(player.transform.position.x + 0.714f, player.transform.position.y + 0.300f, -10);
    }

    private void CalculatingTrajectory() 
    {
        initialSpeed = (mousePos1 - mousePos2)/250;
        for (int i = 0; i < 10; i++) 
        {
            float time = i / 10f;
            trajectory = new Vector2(initialSpeed.x * time, initialSpeed.y * time - 4.905f * time * time);
            Vector3 pos = new Vector3(player.transform.position.x + trajectory.x, player.transform.position.y + trajectory.y, 1);
            trajectoryPoints[i].transform.position = pos;
        }
    }

    private void OnMouseDown()
    {
        if (rb.velocity.x == 0 && rb.velocity.y == 0 && isPlaying == true)
        {
            mousePos1 = Input.mousePosition;
            animator.SetFloat("VelocityY", 0);
            animator.SetBool("p", true);
        }
    }

    private void OnMouseDrag()
    {
        if (rb.velocity.x == 0 && rb.velocity.y == 0 && isPlaying == true)
        {
            mousePos2 = Input.mousePosition;
            CalculatingTrajectory();
        }
    }

    private void OnMouseUp()
    {
        if (rb.velocity.x == 0 && rb.velocity.y == 0 && isPlaying == true)
        {
            for (int i = 0; i < 10; i++)
            {
                trajectoryPoints[i].transform.position = new Vector2(0, -100);
            }
            animator.SetFloat("VelocityY", initialSpeed.y);
            animator.SetBool("p", false);
            rb.velocity = initialSpeed;
            audioSource.PlayOneShot(audioClips[0]);
        }
    }

    public void PlayLandingSound() 
    {
        animator.SetFloat("VelocityY", 0);
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void SetGameState(bool _isPlaying) 
    {
        isPlaying = _isPlaying;
    }
}
