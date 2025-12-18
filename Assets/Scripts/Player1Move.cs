using UnityEngine;
using System.Collections;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.01f;
    private bool isJumping;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1;
    public GameObject Player2;
    public Vector3 Player2Position;
    public GameObject ground;
    private Collider groundCollider;

    private bool isGrounded = true;
    private bool FacingLeft = false;
    private bool FacingRight = true;
    private bool isWalkingLeft = false;
    private bool isWalkingRight = false;


    void Start()
    {
        groundCollider = ground.GetComponent<Collider>();
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
    }

    void FixedUpdate()
    {
        if(isWalkingLeft == true){
            transform.Translate(-WalkSpeed, 0, 0);
        }
        if(isWalkingRight == true){
            transform.Translate(WalkSpeed, 0, 0);
        }
    }

    void OnCollisionStay(Collision other){
        if(other.collider.CompareTag("Ground")){
            isGrounded = true;
            isJumping = false;
            Debug.Log("On the ground");
        }
    }

    void OnCollisionExit(Collision other){
        if(other.collider.CompareTag("Ground")){
            isGrounded = false;
            Debug.Log("In the air !");
        }
    }



    void Update()
    {
        // Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);


        //Limit movement to visible screen
        if(ScreenBounds.x > Screen.width - 200)
        {
            CanWalkRight=  false;
        }
        if(ScreenBounds.x < 200)
        {
            CanWalkLeft=  false;
        }
        else if (ScreenBounds.x > 200 && ScreenBounds.x < Screen.width - 200)
        {
            CanWalkRight=  true;
            CanWalkLeft=  true;
        }

        //Get opponent position and flip if appropriate
        Player2Position = Player2.transform.position;


        //Facing left or right of player2
        if(Player2Position.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if(Player2Position.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }


        //Detecting left and right input
        if(isGrounded){
            if(Input.GetAxis("Horizontal") == 0)
            {
                Anim.SetBool("Forward", false);
                Anim.SetBool("Backward", false);
                isWalkingLeft = false;
                isWalkingRight = false;
            }
            if(Input.GetAxis("Horizontal") > 0)
            {
                if(CanWalkRight == true)
                {
                    Anim.SetBool("Forward", true);
                    isWalkingLeft = false;
                    isWalkingRight = true;
                }

            }
            if(Input.GetAxis("Horizontal") < 0)
            {
                if(CanWalkLeft == true)
                {
                    Anim.SetBool("Backward", true);
                    isWalkingLeft = true;
                    isWalkingRight = false;
                }
            }
        }


        //Jumping and crouching
        if(Input.GetAxis("Vertical") > 0)
        {
            if(isJumping == false)
            {
                isGrounded = false;
                isJumping = true;
                Anim.SetTrigger("Jump");
                // StartCoroutine(JumpPause());
            }            
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }
    }

    IEnumerator FaceLeft()
    {
        if(FacingLeft == true){
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
        }
    }

    IEnumerator FaceRight()
    {
        if(FacingRight == true){
            FacingRight = false;
            FacingLeft = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);
        }
    }
}
