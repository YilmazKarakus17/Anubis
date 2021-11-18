using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerScript player;
    //Player Stats Text Variables
    public Text hpText;
    public Text dashText;
    public Text soulText;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerScript>();
    }


    /*==================================================== User Stats UI Methods =============================================*/
    void updateTexts(){
        updateHPText();
        updateDashText();
        updateSoulsText();
    }

    private void updateHPText()
    {
        if (player.getHealth() < 0){
            
            this.hpText.text = "HP: 0";
        }
        else{
            if (player.getHealth() >= 75 && player.getHealth() <= 100){
                this.hpText.color = Color.green;
            }
            else if (player.getHealth() >= 30 && player.getHealth() < 75){
                this.hpText.color = new Color(255f, 165f, 0f);
            }
            else{
                this.hpText.color = Color.red;
            }
            this.hpText.text = "HP: " + player.getHealth().ToString();
        }  
    }

    private void updateDashText()
    {
        this.dashText.text = "Dash Count: " + player.getDashCount().ToString();
    }

    private void updateSoulsText()
    {
        this.soulText.text = "Souls: " + player.getSoulCount().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateTexts();
    }
}
