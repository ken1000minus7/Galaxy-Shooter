using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed=5.0f;
    [SerializeField]
    private GameObject enemyExplosionPrefab;
    [SerializeField]
    private AudioClip audioClip;
    private UIManager uiManager;
    private GameManager gameManager;

    void Start()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*Time.deltaTime*speed);

        if(transform.position.y<-6)
        {
        	float newX=Random.Range(-8,8);
        	transform.position=new Vector3(newX,6.0f,0);
        }

        if(!gameManager.gameStarted)
        {
        	Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    	Debug.Log("sed");
    	if(other.tag=="Player")
    	{
    		Player player=other.GetComponent<Player>();
    		uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
    		if(uiManager!=null) uiManager.UpdateScore();
    		player.Damage();
    		AudioSource.PlayClipAtPoint(audioClip,Camera.main.transform.position);
    		Instantiate(enemyExplosionPrefab,transform.position,Quaternion.identity);
    		Destroy(this.gameObject);
    		
    	}
    	else if(other.tag=="Laser")
    	{
    		uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
    		if(uiManager!=null) uiManager.UpdateScore();
    		AudioSource.PlayClipAtPoint(audioClip,Camera.main.transform.position);
    		Destroy(this.gameObject);
    		Destroy(other.gameObject);
    		Instantiate(enemyExplosionPrefab,transform.position,Quaternion.identity);
    	}
    }
}
