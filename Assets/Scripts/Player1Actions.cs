using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpHeight = 0.01f;
    public GameObject Player1;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpHeight, 0);
    }
    public void JumpForward()
    {
        Player1.transform.Translate(0, JumpHeight, 0);
        Player1.transform.Translate(0.5f, 0, 0);
    }
    public void JumpBack()
    {
        Player1.transform.Translate(0, JumpHeight, 0);
        Player1.transform.Translate(-0.5f, 0, 0);
    }
}
