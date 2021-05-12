using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private UIManager uiManager;
    private int count=0;
    void Start()
    {
    	uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawn()
    {
    	StartCoroutine(SpawnEnemy());
    	StartCoroutine(SpawnPowerup());
    }

    public IEnumerator SpawnEnemy()
    {
    	while(true)
    	{
    		// if(uiManager!=null && uiManager.gameStarted)
    		// {
    			float x=Random.Range(-8,8);
    			float x2=Random.Range(-8,8);
    			Instantiate(enemyPrefab,new Vector3(x,6.0f,0),Quaternion.identity);
    			if(count%2==0)
    			Instantiate(enemyPrefab,new Vector3(x2,6.0f,0),Quaternion.identity);
    			count++;
    			yield return new WaitForSeconds(4.90f);
    		// }
    	}
    	
    }
    public IEnumerator SpawnPowerup()
    {
    	while(true)
    	{
    		// if(uiManager!=null && uiManager.gameStarted)
    		// {
    			float x=Random.Range(-8,8);
    			int powerup=Random.Range(0,5);
    			Instantiate(powerups[powerup],new Vector3(x,6.0f,0),Quaternion.identity);
    			yield return new WaitForSeconds(6.0f);
    		// }
    	}
    }
}
