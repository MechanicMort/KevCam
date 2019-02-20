using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float panSpeed;
    private float panDetect = 30f;
    private Vector2 playerMovement;
    private float xMove;
    public float moveSpeed;
    private Rigidbody2D pRGB2D;
    private bool toggleCamFollow;
    public Camera playerCam;
    public bool coolDownOne;
    public bool coolDownTwo;
    public int iBattleTimer;
    public string sThrowAwayString;
    public Dictionary<int, string[]> dAttackOrder;
    // Use this for initialization
    void Start() {
        iBattleTimer = 0;
        pRGB2D = GetComponent<Rigidbody2D>();
        coolDownOne = false;
        coolDownTwo = false;
        StartCoroutine(CountTime());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space"))
        {
            if (toggleCamFollow == true)
            {
                toggleCamFollow = false;
            }
            else if (toggleCamFollow == false)
            {
                toggleCamFollow = true;
            }
        }
        else if (Input.GetKeyDown("q"))
        {
            playerCam.transform.position = new Vector3(0, 0, -23) + GetComponentInParent<Transform>().position;
        }
        else if (Input.GetKeyDown("1"))
        {
            coolDownOne = true;
            dAttackOrder.Add(iBattleTimer + 3, new string[] { "AbilityOne" });
            StartCoroutine(CoolDowns());
        }
        else if (Input.GetKeyDown("2"))
        {
            coolDownTwo = true;
            dAttackOrder.Add(iBattleTimer + 3, new string[] { "AbilityTwo" });
            StartCoroutine(CoolDowns());
        }
        MoveCamera();
        MovePlayer();
        CheckAttackOrder();
    }

    private void CheckAttackOrder()
    {
        print(iBattleTimer);
    }
    public IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            iBattleTimer += 1;
        }
    }

    private IEnumerator CoolDowns()
    {
        print(coolDownOne);
        if (coolDownOne == true )
        {
            yield return new WaitForSeconds(5);
            coolDownOne = false;
        }
        else if (coolDownTwo == true)
        {
            yield return new WaitForSeconds(5);
            coolDownOne = false;
        }
    }

    void MovePlayer()
    {
        xMove = Input.GetAxis("Horizontal");
        playerMovement = new Vector2(xMove* moveSpeed * Time.deltaTime, 0);
        pRGB2D.transform.Translate(playerMovement);
    }

    void MoveCamera()
    {  
        xMove = Input.GetAxis("Horizontal");
        float moveX = Camera.main.transform.position.x;
        float movez = Camera.main.transform.position.z;
        float xPosition = Input.mousePosition.x;
        if (toggleCamFollow == false)
        {
            if (Input.GetAxis("Horizontal") >= 0.01)
            {
                moveX -= xMove;
            }

            else if (Input.GetAxis("Horizontal") <= -0.01)
            {
                moveX -= xMove;
            }
        }

        if ( xPosition > 0 && xPosition < panDetect)
        {
            moveX -= panSpeed;
        }
        else if (xPosition < Screen.width && xPosition > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }
        Vector3 newPosition = new Vector3(moveX,0,movez);
        Camera.main.transform.position = newPosition;
    }
}
