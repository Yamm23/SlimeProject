using UnityEngine;
using static SlimeManager;

public class SlimeManager : MonoBehaviour
{
    public enum Slimestate
    {
        Idle,
        Moving,
        Dashing,
        Jumping,
        Dead
    }
    private SlimeMovement slimeMovement;
    private SlimeMovement slimeJumping;
    private SlimeHealth slimeHealth;
    public CoinManager cm;
    public PauseMenu pauseMenu;

    public Slimestate currentState = Slimestate.Idle;

    private void Awake()
    {
        slimeMovement = GetComponent<SlimeMovement>();
        slimeHealth = GetComponent<SlimeHealth>();
    }

    private void Update()
    {
        HandleState();
        HandleTransition();
    }

    private void HandleTransition()
    {
        if (Input.GetAxis("Horizontal") != 0 && currentState != Slimestate.Moving)
        {
            SetState(Slimestate.Moving);
        }
        else if (Input.GetAxis("Horizontal") == 0 && currentState == Slimestate.Moving)
        {
            SetState(Slimestate.Idle);
        }
        if (Input.GetKeyDown(KeyCode.Space) && currentState != Slimestate.Jumping)
        {
            SetState(Slimestate.Jumping);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentState != Slimestate.Dashing)
        {
            SetState(Slimestate.Dashing);
        }
    }
    

    private void HandleState()
    {
        switch (currentState)
        {
            case Slimestate.Idle:

                break;

            case Slimestate.Moving:
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                slimeMovement.HandleMovement(moveHorizontal);
                break;

            case Slimestate.Jumping:
                slimeMovement.HandleJumping();
                break;

            case Slimestate.Dashing:
                slimeMovement.HandleDash();
                break;

            case Slimestate.Dead:

                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CoinItem"))
        {
            AudioManager.instance.Play("Coin");
            Destroy(other.gameObject);
            cm.AddCoin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            pauseMenu.LevelComplete();
        }
    }

    public void SetState(Slimestate newState)
    {
        switch (currentState)
        {
            case Slimestate.Idle:
                if (newState == Slimestate.Moving || newState == Slimestate.Dashing || newState == Slimestate.Jumping || newState == Slimestate.Dead)
                {
                    currentState = newState;
                }
                    break;

            case Slimestate.Moving:
                if (newState == Slimestate.Idle || newState == Slimestate.Dashing || newState == Slimestate.Jumping || newState == Slimestate.Dead)
                {
                    currentState = newState;
                }
                break;

            case Slimestate.Dashing:
                if (newState == Slimestate.Idle || newState == Slimestate.Moving || newState == Slimestate.Jumping || newState == Slimestate.Dead)
                {
                    currentState = newState;
                }
                break;

            case Slimestate.Jumping:
                if (newState == Slimestate.Idle || newState == Slimestate.Dashing || newState == Slimestate.Moving || newState == Slimestate.Dead)
                {
                    currentState = newState;
                }
                break;

            case Slimestate.Dead:
                //No transitions allowed from dead
                break;

            default:
                currentState = newState; 
                break;


        }
        Debug.Log($"Slime state changed to: {currentState}");
    }
}
