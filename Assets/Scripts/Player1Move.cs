using UnityEngine;
using System.Collections;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.01f;
    private bool IsJumping;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1;
    public GameObject Player2;
    public Vector3 Player2Position;
    private bool FacingLeft = false;
    private bool FacingRight = true;



    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
    }

    void Update()
    {
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

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

        if(Player2Position.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if(Player2Position.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }

        // if(Player2Position.x > transform.position.x)
        // {
        //     StartCoroutine(LeftIsTrue());
        // }
        // if(Player2Position.x < transform.position.x)
        // {
        //     StartCoroutine(RightIsTrue());
        // }

        //Walking left and right
        if(Player1Layer0.IsTag("Motion"))
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                if(CanWalkRight == true)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed, 0, 0);
                }
            }
            if(Input.GetAxis("Horizontal") < 0)
            {
                if(CanWalkLeft == true)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(-WalkSpeed, 0, 0);
                }

            }
            if(Input.GetAxis("Horizontal") == 0)
            {
                Anim.SetBool("Forward", false);
                Anim.SetBool("Backward", false);
            }
        }

        //Jumping and crouching
        if(Input.GetAxis("Vertical") > 0)
        {
            if(IsJumping == false)
            {
                IsJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
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


    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }

    IEnumerator FaceLeft()
    {
        if(FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
        }
    }

    IEnumerator FaceRight()
    {
        if(FacingRight == true)
        {
            FacingLeft = true;
            FacingRight = false;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);
        }
    }
}
