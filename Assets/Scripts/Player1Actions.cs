using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpHeight = 0.01f;
    public GameObject Player1;
    public Rigidbody rb;
    private bool JumpUpTrue = false;
    private bool JumpForwardTrue = false;
    private bool JumpBackTrue = false;
    private float thrust = 40f;
    

    void Start()
    {
        
    }

    //Apply force to jumping rigidbody in fixedUpdate
    void FixedUpdate()
    {
        if(JumpUpTrue == true){
            JumpUpTrue = false;
            rb.AddForce(0, thrust, 0, ForceMode.Impulse);
        }
        if(JumpForwardTrue == true){
            JumpForwardTrue = false;
            rb.AddForce(15f, thrust, 0, ForceMode.Impulse);
        }
        if(JumpBackTrue == true){
            JumpBackTrue = false;
            rb.AddForce(15f, thrust, 0, ForceMode.Impulse);
        }
    }


    //jump functions called in animation
    public void JumpUp()
    {
        JumpUpTrue = true;
    }
    public void JumpForward()
    {
        JumpForwardTrue = true;
    }
    public void JumpBack()
    {
        JumpBackTrue = true;
    }
}
