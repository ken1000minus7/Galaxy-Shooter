using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private GameObject[] engine;

	[SerializeField]
    private float fireRate=0.25f;

    private float nextFire=0.0f;

    [SerializeField]
    private float speed = 8.0f;

    private UIManager uiManager;

    private GameManager gameManager;

    private SpawnManager spawnManager;

    private AudioSource audioSource;
    
    public bool canTripleShot=false;

    public bool speedBoostOn=false;

    public bool shieldEnabled=false;

    public int lives=3;

    void Start()
    {
        transform.position=new Vector3(0,0,0);
        uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager=GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource=GetComponent<AudioSource>();
        if(uiManager!=null)
        {
        	uiManager.UpdateLives(lives);
        }
        if(spawnManager!=null)
        {
        	spawnManager.StartSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(canTripleShot)
        {
        	//Tripleshot();
        	if(Input.GetKeyDown(KeyCode.Space) && Time.time>=nextFire)
        	{
        		audioSource.Play();
        		Instantiate(laserPrefab,transform.position+ new Vector3(0,1.01f,0),Quaternion.identity);
        		Instantiate(laserPrefab,transform.position+ new Vector3(0.54f,0.12f,0),Quaternion.identity);
        		Instantiate(laserPrefab,transform.position+ new Vector3(-0.54f,0.12f,0),Quaternion.identity);
        		nextFire=Time.time+fireRate;
        	}
        }
        else
        {
        	Shoot();
        }
    }

    private void Shoot()
    {
    	if(Input.GetKeyDown(KeyCode.Space) && Time.time>=nextFire)
        {
        	Instantiate(laserPrefab,transform.position + new Vector3(0,1.01f,0), Quaternion.identity);
        	audioSource.Play();
        	nextFire=Time.time + fireRate;
        }
    }

    private void Movement()
    {
    	float hinput=Input.GetAxis("Horizontal");
        float vinput=Input.GetAxis("Vertical");
        float boost=1.0f;

        if(speedBoostOn) boost=1.5f;
        else boost=1.0f;

        transform.Translate(Vector3.right*Time.deltaTime*speed*hinput*boost);
        transform.Translate(Vector3.up*Time.deltaTime*speed*vinput*boost);

        if(transform.position.y>0) transform.position= new Vector3(transform.position.x,0,0);
        else if(transform.position.y<-4) transform.position= new Vector3(transform.position.x,-4.0f,0);

        // if(transform.position.x>8.3f) transform.position= new Vector3(8.3f,transform.position.y,0);
        // else if(transform.position.x<-8.3f) transform.position= new Vector3(-8.3f,transform.position.y,0);

        if(transform.position.x>9.2f) transform.position= new Vector3(-9.1f,transform.position.y,0);
        else if(transform.position.x<-9.2f) transform.position= new Vector3(9.1f,transform.position.y,0);
    }

    private void Tripleshot()
    {
    	if(Input.GetKeyDown(KeyCode.Space) && Time.time>=nextFire)
        {
        	Instantiate(tripleShotPrefab,transform.position, Quaternion.identity);
        	audioSource.Play();
        	nextFire=Time.time + fireRate;
        }
    }

    public void TripleShotOn()
    {
    	canTripleShot=true;
    	StartCoroutine(TripleShotOff());
    }

    public IEnumerator TripleShotOff()
    {
    	yield return new WaitForSeconds(6.0f);
    	canTripleShot=false;
    }

    public void SpeedBoostOn()
    {
    	speedBoostOn=true;
    	StartCoroutine(SpeedBoostOff());
    }

    public IEnumerator SpeedBoostOff()
    {
    	yield return new WaitForSeconds(6.0f);
    	speedBoostOn=false;
    }

    public void Damage()
    {
    	if(!shieldEnabled)
    	{
    		lives--;
    		int x=Random.Range(0,2);
    		if(lives==2)
    		{
    			engine[x].SetActive(true);
    			engine[1-x].SetActive(false);
    		}
    		else if(lives==1)
    		{
    			engine[0].SetActive(true);
    			engine[1].SetActive(true);
    		}
    	} 
    	shield.SetActive(false);
    	uiManager.UpdateLives(lives);
    	if(lives<=0)
        {
        	Destroy(this.gameObject);
        	Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        	gameManager.GameEnd();
        }
        shieldEnabled=false;
    }

    public void ShieldOn()
    {
    	shieldEnabled=true;
    	shield.SetActive(true);
    }

    public void GiveLife()
    {
    	lives++;
    	int x=Random.Range(0,2);
    	if(lives==2)
    	{
    		engine[x].SetActive(true);
    		engine[1-x].SetActive(false);
    	}
    	else if(lives>=3)
    	{
    		lives=3;
    		engine[0].SetActive(false);
    		engine[1].SetActive(false);
    	}
    	uiManager.UpdateLives(lives);
    }

    public void DoublePointsOn()
    {
    	uiManager.DoubleOn();
    	StartCoroutine(DoublePointsOff());
    }

    public IEnumerator DoublePointsOff()
    {
    	yield return new WaitForSeconds(6.0f);
    	uiManager.DoubleOff();
    }
}
