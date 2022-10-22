using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public float jumpForce;
    public GameObject winTextObject;
    public AudioClip musicBackground;
    public AudioClip musicWin;
    public AudioClip musicLose;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winTextObject.SetActive(false);

        musicSource.clip = musicBackground;
        musicSource.loop = true;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
            if(scoreValue == 4)
            {
                livesValue = 3;
                lives.text = livesValue.ToString();
                transform.position = new Vector3(41.0f, 0.0f, 0.0f); 
            }

            if(scoreValue == 8)
            {
                winTextObject.SetActive(true); 
                musicSource.clip = musicWin;
                musicSource.loop = false;
                musicSource.Play();

                Destroy(this);
            }

        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(livesValue <= 0)
        {
            winTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Michael Colon says Game Over!";
            winTextObject.SetActive(true);
            musicSource.clip = musicLose;
            musicSource.loop = false;
            musicSource.Play();
            Destroy(this);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

}
