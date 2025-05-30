using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController character;
    public float speed = 6;
    private Vector3 moveDirection = Vector3.zero;

    //Animation
    Animator anim;

    //การโจมตี
    public float fireRate = 0.4f;
    public float nextFire = 0.0f;
    public GameObject spawnPoint, weapon;

    //จัดการพลังชีวิต
    public int health = 100;
    public Slider slider;

    //การเล่นเสียง
    AudioSource audioSource;
    public AudioClip hit,die;
    public static bool isAlive = true;

    public ParticleSystem blood;
    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;        
    }

    // Update is called once per frame
    void Update()
    {
        if (character.isGrounded && isAlive)
        {
            anim.SetBool("IsWalk", false);
            //การเคลื่อนที่
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if(Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anim.SetBool("IsWalk", true);    
                    character.Move(moveDirection * Time.deltaTime);
                }                    
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            rotatePlayer();
            playerAttack();            
        } 

        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            audioSource.PlayOneShot(die);
            anim.SetTrigger("IsDeath");
            StartCoroutine(GameOver());
        }       
    }

    void rotatePlayer()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //หมุนในแนวนอน
            if (Input.GetAxis("Horizontal") < 0)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
            }

            else if(Input.GetAxis("Horizontal") > 0)
            {
            this.transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
            }
            //หมุนในแนวตั้ง
            if (Input.GetAxis("Vertical") < 0)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
            }

            else if (Input.GetAxis("Vertical") > 0)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }        
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Home");
    }

    void playerAttack()
    {
        if (Input.GetMouseButton(0) && Time.time>nextFire)
        {
            nextFire = Time.time + fireRate;
            shootWeapon();
        }
    }

    void shootWeapon()
    {
        anim.SetBool("IsAttack", true);
        StartCoroutine(resetAttack());
    }

    IEnumerator resetAttack()
    {
        yield return new WaitForSeconds(1.5f);        
        anim.SetBool("IsAttack", false);
        nextFire = 0.0f;
    }

    void createArrow()
    {
        Instantiate(weapon, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyWeapon" && Enemy.checkAttack)
        {
            anim.Play("Damage");
            health -= 10;
            slider.value = health;
            audioSource.PlayOneShot(hit);
            blood.Play();
            Enemy.checkAttack = false;
        }
    }
}
