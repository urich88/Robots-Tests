using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Buttons : MonoBehaviour {
	
	//public Type type;
	//public Rarity rarity;
	//stats
	public string robotName;
	public float baseHP;
	public float currentHP;
	public float baseAtk;
	public float currentAtk;
	public float criticalHit;
	public float baseDef; //armor 
	public float currentDef;
	public float speed; //alertness
	public bool turn;
	//buffs & debuffs
	public float supersentidos; //swiftness
	public float precision; //entra o no entra el golpe
	public float presNum;
	public float offensive; //additional damage
	//vars to get the input on the GUI
	//HP
	private string inputHP = "0";
	private float hpToFloat;
	private string offenseInp = "0 - 30%";
	//

	
	// Use this for initialization
	void Start () {

		//initializing base stats

		currentHP = 80;
	
		
	}
	
	// Update is called once per frame
	void Update () {


		//currentHP = float.Parse(inputHP); //why it doesnt work the attacks here?
		//checking than HP dont get negatives or pass over the max hp pool

		if(currentHP <= 0)
		{
			currentHP = 0;
		}
		if(currentHP >= 120)
		{
			currentHP = 120;
		}
		//Keyboard input to test certain process, make comment ones the GUI works completely.
		#region $alternativeInput$
		if(Input.GetKeyDown("a"))
		   {

			Pow ();
		}
		if(Input.GetKeyDown("s"))
		{
			
			Pew ();
		}
		if(Input.GetKeyDown("d"))
		{
			
			Protocanon ();
		}
		if(Input.GetKeyDown("p"))
		   {

			Presicion ();
		}
		if(Input.GetKeyDown("r"))
		   {

			CriticalHit ();
		}
		#endregion

		//converting the string to float so we can use it later.




	
	}

	//Using old ugly GUI
	#region $GUI$
	void OnGUI() {

		//input del usuario en HP
		inputHP = GUI.TextField (new Rect (500, 25, 100, 30), currentHP.ToString(), 3);
		//input by button added display, additional damage A.K.A offensive
		offenseInp = GUI.TextField (new Rect(500, 60, 100, 30), offensive.ToString(), 2);




		//solo prueba que este checando lo que se mete
		GUI.Box (new Rect (140, 300, 400, 400), "HP: "+ baseHP + "\ncurrentHP: " + currentHP + "\nbaseAtk: " + baseAtk +
		        "\ncurrentATK: " + currentAtk + "\nbasedef: " + baseDef + "\ncurrentDef: " + currentDef +
		        "\nspeed: " + speed);

		//Buttons in GUI
		#region Buttons

		#region $Player buttons$
		//attack buttons
		if (GUI.Button(new Rect(10, 70, 50, 30), "Punch"))
		{
			currentHP = float.Parse(inputHP);
			Debug.Log("pow!");
			Pow();
		}
		if (GUI.Button(new Rect(10, 100, 50, 30), "Pew"))
		{
			currentHP = float.Parse(inputHP);
			Debug.Log("pew, pew!");
			Pew ();

		}
		if (GUI.Button(new Rect(10, 130, 80, 30), "protocanon"))
		{
			currentHP = float.Parse(inputHP);
			Debug.Log("PROTOCANON!");
			Protocanon();
			
		}

		//Player 2 attack buttons
		if (GUI.Button(new Rect(140, 70, 80, 30), "Punch2"))
		{
			Debug.Log("pow2!");
		}
		if (GUI.Button(new Rect(140, 100, 80, 30), "Pew2"))
		{
			Debug.Log("pew, pew2!");

		}
		if (GUI.Button(new Rect(140, 130, 90, 30), "protocanon2"))
		{
			Debug.Log("PROTOCANON2!");
			
		}
		#endregion

		#endregion $Player buttons$

		#region $Buffs$

		//Regenerate HP only if HP = 0

			if (GUI.Button(new Rect(500, 100, 100, 30), "+80 HP"))
		{
			currentHP += 80;
		}
		//offense button bonus to add to the attack, sums +5 each time
			if (GUI.Button (new Rect (500, 150, 50, 50), "+5"))
		{
			offensive += baseAtk * 5 / 100;

		}

		#endregion $Buffs$



		#endregion $GUI$


}
	#region $Attacks$
	//attacks
	//every attack has a probability to inflinge critical hit
	void Pow() {

		//default val
		baseAtk = 20;
		//base attack mas bonus del jugador
		currentAtk = baseAtk + currentAtk;
		//close combat attack

		//calls critical hit damage * 2
		CriticalHit ();
		//adds offense
		AdiditonalDamage();


		Debug.Log ("swosh");
		if(criticalHit == 8)
		{
			currentHP -= currentAtk * 2;
		}
		else
		{
			currentHP -= currentAtk + 10;
		}



	}

	void Pew() {

		//default val
		currentAtk = 40;

		//range attack
		CriticalHit ();
		//adds offense
		AdiditonalDamage();


		Debug.Log("fuuu");

		if(criticalHit == 8)
		{
			currentHP -= currentAtk + 20 * 2;
		}
		else
		{
			currentHP -= currentAtk + 20;
		}



	}
	//super attack
	void Protocanon() {

		currentAtk = 50;

		CriticalHit ();
		//adds offense
		AdiditonalDamage();


		Debug.Log("fghhh");

		if(criticalHit == 8)
		{
			currentHP -= currentAtk + 50 * 2;
		}
		else
		{
			currentHP -= currentAtk + 50;
		}


	}

	#endregion

	#region $bonuses$

	//the posibility than an attack its succesful or not

	void Presicion() {

		float totalPres;

		presNum = precision/100;
		totalPres = currentAtk * presNum;
		Debug.Log(presNum);
		Debug.Log(totalPres);

	}

	//el golpe critico se obtiene con un random en rango de 1 a 10


	void CriticalHit () {

		//generating random
		criticalHit = Random.Range(1,10);
		Debug.Log(criticalHit);
	}

	//buffs and debuffs

	void AdiditonalDamage() {


		currentAtk = baseAtk + offensive;
	}

	#endregion $bonuses$



	
}