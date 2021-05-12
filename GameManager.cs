using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool gameStarted=false;
	public UIManager uiManager;
	public GameObject spawnManager;
	public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
    	uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        {
        	if(Input.GetKeyDown(KeyCode.Space))
        	{
        		GameStart();
        	}
        }
    }

    public void GameStart()
    {
    	spawnManager.SetActive(true);
    	gameStarted=true;
    	Instantiate(playerPrefab,new Vector3(0,0,0),Quaternion.identity);
    	uiManager.HideStartMenu();
    }
    public void GameEnd()
    {
    	gameStarted=false;
    	uiManager.ShowStartMenu();
    	spawnManager.SetActive(false);
    }

}
