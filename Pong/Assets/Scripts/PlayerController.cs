using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boundary = 4f;

    public GameObject player1;
    public GameObject player2;

    private Vector3 player1Position;
    private Vector3 player2Position;

    // Player One Controls
    public KeyCode player1MoveUpKey = KeyCode.W;
    public KeyCode player1MoveDownKey = KeyCode.S;

    // Player Two Controls
    public KeyCode player2MoveUpKey = KeyCode.UpArrow;
    public KeyCode player2MoveDownKey = KeyCode.DownArrow;

    void Update()
    {
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;

        if (Input.GetKey(player1MoveUpKey))
        {
            player1Position.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(player2MoveUpKey))
        {
            player2Position.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(player1MoveDownKey))
        {
            player1Position.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(player2MoveDownKey))
        {
            player2Position.y -= moveSpeed * Time.deltaTime;
        }


        player1Position.y = Mathf.Clamp(player1Position.y, -boundary, boundary);
        player2Position.y = Mathf.Clamp(player2Position.y, -boundary, boundary);

        player1.transform.position = player1Position;
        player2.transform.position = player2Position;
        
    }
}
