using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private int sideboders = 7;
    public GameObject rock;
    public GameObject spawner;
    private bool gameOver = false;
    public Button restart;
    //public bool Win;
    public TextMeshProUGUI text;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z > 250)
        {
            text.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            transform.position = new Vector3(this.transform.position.x,0,250);
        }

        if (!gameOver) // Run the game till Game is not Over
        {


            //code to move player forward, right and left 

            float horizontalValue = Input.GetAxis("Horizontal");
            float verticalValue = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(horizontalValue, 0, verticalValue);
            transform.Translate(move * speed * Time.deltaTime);



            // Throw Rocks at obstacles 

            if (Input.GetMouseButtonDown(0)) 
            {
                Instantiate(rock, spawner.transform.position, Quaternion.identity);
            }


            //Animation
            if(Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Static_b", true);
                animator.SetFloat("Speed_f", 0.6f);
            }

            if (!Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Static_b", false);
                animator.SetFloat("Speed_f", 0.1f);
            }



            //Constraint for movement direction after sideboders

            if (transform.position.x < -sideboders) 
            {
                transform.position = new Vector3(-7, 0, transform.position.z);
            }

            else if (transform.position.x > sideboders) 
            {
                transform.position = new Vector3(7, 0, transform.position.z);
            }
            if (transform.position.z < 0) 
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "plane" && collision.gameObject.tag !="Rock")
        {
            Debug.Log(collision.gameObject.tag);
            gameOver = true;
            restart.gameObject.SetActive(true);
            animator.SetBool("Death", true);
        }
    }
    public void OnclickRestart()
    {
        SceneManager.LoadScene(0); //Load scene from starting on GameOver or After Winning
    }
}
