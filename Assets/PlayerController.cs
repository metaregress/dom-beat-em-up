using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public int maxHealth;
    int currentHealth;

    bool attacking;

    public List<GameObject> moves;
    Move currentMove = null;

    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleState();
    }

    void HandleInput()
    {
        if (!attacking)
        {
            float xMovement = moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            float yMovement = moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.Translate(new Vector3(xMovement, yMovement, 0));
        }

        if (Input.GetKeyDown(KeyCode.J) && !attacking)
        {
            Vector3 instantiatePosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            GameObject currentMoveGO = Instantiate(moves[0], instantiatePosition, Quaternion.identity);
            attacking = true;
            currentMove = currentMoveGO.GetComponent<Move>();
        }
    }

    void HandleState()
    {
        if(currentMove != null)
        {
            AttackStateMachine currentMachine = currentMove.GetComponent<AttackStateMachine>();
            if (currentMachine.currentState == AttackStateMachine.States.None)
            {
                attacking = false;
                // TODO: handle move cleanup here?
                Destroy(currentMove.gameObject); // attacks should probably clean themSELVES up instead of relying on the player/enemy to do it...
                currentMove = null;
            }
        }
    }
}
