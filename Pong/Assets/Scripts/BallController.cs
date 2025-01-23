using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 5f;
    
    // direction
    private Vector2 ballDirection;
    private float randomX;
    private float randomY;
    private float yNew;

    // texts
    public TextMeshProUGUI scoreBoard1;
    public TextMeshProUGUI scoreBoard2;
    public TextMeshProUGUI EndingText;
    
    // buttons
    public GameObject restartButton;
    public GameObject startButton;
    public GameObject PlayerController;

    private int score1 = 0;
    private int score2 = 0;

    private bool gameStarted = false;

    void Start()
    {
        restartButton.SetActive(false);
        startButton.SetActive(true);

        DisableBoardMovement();

        // the X-Speed cannot be 0, otherwise it will never reach the board
        if (Random.Range(0, 2) == 0)
        {
            randomX = -1f;
        }
        else
        {
            randomX = 1f;
        }
        randomY = Random.Range(-1f, 1f);
        
        // normalize the direction so that it's more accurate when it comes to the speed multiply
        ballDirection = new Vector2(randomX, randomY).normalized;
    }

    void Update()
    {
        if (gameStarted)
        {
            transform.Translate(ballDirection * ballSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Board1"))
        {
            ballDirection.x = -ballDirection.x;

            // new y direction is ball's y position - board's y position
            yNew = transform.position.y - collision.transform.position.y;
            ballDirection.y = yNew;
            ballDirection = ballDirection.normalized;
            
            score1++;
            scoreBoard1.text = score1.ToString();
        }
        else if (collision.gameObject.CompareTag("Board2"))
        {
            ballDirection.x = -ballDirection.x;
            yNew = transform.position.y - collision.transform.position.y;
            ballDirection.y = yNew;
            ballDirection = ballDirection.normalized;
            
            score2++;
            scoreBoard2.text = score2.ToString();
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            ballDirection.y = -ballDirection.y;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("End"))
        {
            Debug.Log("Game Over");
            
            if (score1 > score2)
            {
                EndingText.text = "Player1 Wins";
            }
            else if (score2 > score1)
            {
                EndingText.text = "Player2 Wins";
            }
            else
            {
                EndingText.text = "Deuce";
            }
            restartButton.SetActive(true);
            ballSpeed = 0f;

            DisableBoardMovement(); 
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartGame()
    {
        gameStarted = true;
        startButton.SetActive(false);

        EnableBoardMovement(); 
    }
    
    private void DisableBoardMovement()
    {
        PlayerController.GetComponent<PlayerController>().enabled = false;
    }
    private void EnableBoardMovement()
    {
        PlayerController.GetComponent<PlayerController>().enabled = true;
    }
    
}
