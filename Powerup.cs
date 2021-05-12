using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed=2.0f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip audioClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*Time.deltaTime*speed);
        if(transform.position.y<-6)
        {
        	Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    	//Debug.Log(other.name);
    	if(other.tag=="Player")
    	{
    		Player player=other.GetComponent<Player>();
    		AudioSource.PlayClipAtPoint(audioClip,Camera.main.transform.position);
    		if(player!=null)
    		{
    			switch(powerupID)
    			{
    				case 0:
    				player.TripleShotOn();
    				break;

    				case 1:
    				player.SpeedBoostOn();
    				break;

    				case 2:
    				player.ShieldOn();
    				break;

    				case 3:
    				player.GiveLife();
    				break;

    				case 4:
    				player.DoublePointsOn();
    				break;
    			}
    		}
    		Destroy(this.gameObject);
    	}
    }
}
