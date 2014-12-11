using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player:AbsPersonagem
{
    //Atributos Basicos
    public int mp;
    public int mpMax;
	public int exp;
	public int expMax;
	public int level;
	public int skillPoints;

	//Atributos que podem ser melhorados
	public int meleeDamage;
	public int rangedDamage;
	//Nao esta aqui, mas pode ser melhorado
		//hpMax
		//velocidade

	//Auxiliares
	public bool jumping;
    public EnumEstado estadoPlayer;
    public bool isGrounded; // verifica se esta no solo
    public bool jumped; //verifica se esta pulando
    public float jumpTime; // usando para verificar se parou de pular
    public float force;	//forca do pulo
    public float jumpDelay; // tempo da animaçao

	//GUI
	public bool showGameOver = false; // usado para mostrar o menu de Game Over
	public bool showLevelUp = false; // usado para mostrar o menu de Level Up
	private GUISkin skin; //Stylize the Gui

	public Vector2 meleeDamagePosition;
	public Vector2 rangedDamagePosition;
	public Vector2 hpMaxPosition;
	public Vector2 velocidadePosition;
		
	public override void Andar()
    {
        this.isGrounded = Physics2D.Raycast(this.transform.position, this.ground.position, 1 << LayerMask.NameToLayer("Plataforma"));
        this.animator.SetFloat("run", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.eulerAngles = new Vector2(0, 0);
            this.transform.Translate(Vector2.right * this.velocidade * Time.deltaTime);

        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.eulerAngles = new Vector2(0, 180);
            this.transform.Translate((Vector2.right) * this.velocidade * Time.deltaTime);
        }


        if(Input.GetButtonDown("Jump") && this.isGrounded && !this.jumped){
            this.rigidbody2D.AddForce(this.transform.up * this.force);
            this.jumpTime = this.jumpDelay;
            this.animator.SetTrigger("jump");
            this.jumped = true;
        }

        this.jumpTime -= Time.deltaTime;

        if (this.jumpTime <= 0 && this.isGrounded && this.jumped)
        {
            this.animator.SetTrigger("ground");
            this.jumped = false;
        }

    }

    public override void Cair()
    {
        throw new System.NotImplementedException();
    }

    public override void Morrer()
    {
        throw new System.NotImplementedException();
    }

    public override void TomarDano()
    {
        throw new System.NotImplementedException();
    }

    public override void Ataque()
    {
        throw new System.NotImplementedException();
    }

    public override void Bloqueio()
    {
        throw new System.NotImplementedException();
    }

    public override void Parador()
    {
        throw new System.NotImplementedException();
    }

    public override void Pular()
    {
        throw new System.NotImplementedException();
    }
	
	void OnGUI(){
		const int buttonWidth = 120;
		const int buttonHeight = 60;
		GUI.skin = skin;

		/* Game Over Menu Begin */
		if(showGameOver){
			// Center in X, 1/3 of the height in Y
			if(GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 3) -
			                       (buttonHeight / 2), buttonWidth, buttonHeight), "Retry!")){
				// Reload the level
				Application.LoadLevel("level1");
			}
			// Center in X, 2/3 of the height in Y
			if(GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) -
			                       (buttonHeight / 2), buttonWidth, buttonHeight), "Back to menu")){
				// Reload the level
				Application.LoadLevel("MainMenu");
			}
		}
		/* Game Over Menu End */

		/* Level Up Menu Begin */
		/*meleeDamagePosition;
		rangedDamagePosition;
		hpMaxPosition;
		velocidadePosition;*/
		const float buttonWidth2 = 0.3f;
		const float buttonHeight2 = 0.1f;

		if(showLevelUp){
			// Center in X, 1/3 of the height in Y
			if(GUI.Button(new Rect(Screen.width * meleeDamagePosition.x, Screen.height * meleeDamagePosition.y,
			                       Screen.width * buttonWidth2, Screen.height * buttonHeight2), "Dano Fisico")){
				if(skillPoints > 0){
					meleeDamage++;
					skillPoints--;
				}else{
					print("Voce nao tem pontos!");
				}
			}
			if(GUI.Button(new Rect(Screen.width * rangedDamagePosition.x, Screen.height * rangedDamagePosition.y,
			                       Screen.width * buttonWidth2, Screen.height * buttonHeight2), "Dano Baladeira")){
				if(skillPoints > 0){
					rangedDamage++;
					skillPoints--;
				}else{
					print("Voce nao tem pontos!");
				}
			}
			if(GUI.Button(new Rect(Screen.width * hpMaxPosition.x, Screen.height * hpMaxPosition.y,
			                       Screen.width * buttonWidth2, Screen.height * buttonHeight2), "Vida")){
				if(skillPoints > 0){
					hpMax += 50;
					hp += 50;
					skillPoints--;
				}else{
					print("Voce nao tem pontos!");
				}
			}
			if(GUI.Button(new Rect(Screen.width * velocidadePosition.x, Screen.height * velocidadePosition.y,
			                       Screen.width * buttonWidth2, Screen.height * buttonHeight2), "Velocidade")){
				if(skillPoints > 0){
					velocidade++;
					skillPoints--;
				}else{
					print("Voce nao tem pontos!");
				}
			}
		}
		/* Level Up Menu End */
	}
	
	public override void Update()
    {
		if(hp <= 0){
			showGameOver = true;
		}

		if(exp >= expMax){
			exp -= expMax;
			expMax *= 2;
			level++;
			skillPoints++;
		}
		//showLevelUp
		this.Andar();
    }


    public override void Start()
    {
        base.Start();
        this.jumping = false;
        this.jumpTime = 0.0f;
        this.jumpDelay = 0.3f;
        this.isGrounded = true;
        this.force = 200f;
        this.gameObject.tag = "Player";
        this.hpMax = 100;
        this.hp = 10;
        this.mpMax = 100;
        this.mp = 10;
		this.expMax = 100;
		this.exp = 0;
		this.level = 1;
		this.skillPoints = 0;
		this.meleeDamage = 5;
		this.rangedDamage = 5;

		skin = Resources.Load("GUISkin") as GUISkin;
    }

}
