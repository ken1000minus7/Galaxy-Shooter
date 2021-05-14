using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Sprite[] lives;
	public Image livesImage;
	public Text scoreText;
	public int point=10;
	public int score=0;
	public GameObject startMenu;
	public GameObject commands;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
        	Application.Quit();
        }
    }

    public void UpdateLives(int currentLives)
    {
    	livesImage.sprite=lives[currentLives];
    }

    public void UpdateScore()
    {
    	score+=point;
    	scoreText.text="Score: "+score;
    }

    public void HideStartMenu()
    {
    	startMenu.SetActive(false);
        scoreText.text="Score: 0";
        score=0;
        commands.SetActive(false);
    }

    public void ShowStartMenu()
    {
    	startMenu.SetActive(true);
    	commands.SetActive(true);
    }

    public void DoubleOn()
    {
    	point=20;
    }

    public void DoubleOff()
    {
    	point=10;
    }
}
