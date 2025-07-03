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



    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
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

        //Walking left and right
        if(Player1Layer0.IsTag("Motion"))
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                if(CanWalkRight == true)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(0, 0, WalkSpeed);
                }
            }
            if(Input.GetAxis("Horizontal") < 0)
            {
                if(CanWalkLeft == true)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(0, 0, -WalkSpeed);
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
}
