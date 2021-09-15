using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public static float speed = 5;
    public float jumpForce;
    public float laneDistance;//khoan cach giua cac lane
    private int numberOflane = 1;// 0,1,2 : left, mid, right
    private Vector3 moveVector;
    private float veticalVelocity;
    private float gravity = 12.0f;
    private Animator anim;
    private float newXPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;
        moveVector.z = speed;

        //up and down
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)||SwipeController.swipeUp)
            {
                anim.SetBool("isJump", true);
                veticalVelocity = jumpForce;
            }
            else
            {
                anim.SetBool("isJump", false);
            }
        }
        else
        {
            veticalVelocity -= gravity * Time.deltaTime;
        }
       
        if (Input.GetKeyDown(KeyCode.DownArrow)||SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }
        moveVector.y = veticalVelocity;

        //left and right
        if (Input.GetKeyDown(KeyCode.LeftArrow)||SwipeController.swipeLeft)
        {
            numberOflane--;
            if (numberOflane == -1)
                numberOflane = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)||SwipeController.swipeRight)
        {
            numberOflane++;
            if (numberOflane == 3)
                numberOflane = 2;
        }
        if (numberOflane == 2)
        {
            newXPos = laneDistance;
        }
        else if (numberOflane == 0)
        {
            newXPos = -laneDistance;
        }
        else if (numberOflane == 1)
        {
            newXPos = 0;
        }
        moveVector.x = (newXPos-transform.position.x)*speed;

    }
    private void FixedUpdate()
    {
        controller.Move(moveVector * Time.fixedDeltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag== "Obstacle")
        {
            StartCoroutine(Death());
            GameManagers.isGameOver = true;
            FindObjectOfType<AudioManager>().PauseSound("MainTheme");
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
           
        }
      
    }

    private IEnumerator Slide()
    {
        anim.SetBool("isSlide", true);
        controller.height = (float)0.2;
        controller.center = new Vector3(0, (float)0.2, 0);
        yield return new WaitForSeconds(1.0f);//doi 1s sau se thuc hien cac lenh tiep
        anim.SetBool("isSlide", false);
        controller.height = (float)1.1;
        controller.center = new Vector3(0, (float)0.5, 0);
    }
    IEnumerator Death()
    { 
        FindObjectOfType<AudioManager>().PlaySound("Hit");
        anim.SetBool("isDeath", true);
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0;
       
    }
}
