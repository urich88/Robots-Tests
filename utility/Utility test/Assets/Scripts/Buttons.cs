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
	//additional damage
	public float offensive; 
	public float bonus;
	//vars to get the input on the GUI
	//HP
	private string inputHP = "0";
	private float hpToFloat;
	//offense
	private string offenseInp = "0 - 30%";

	//

	
	// Use this for initialization
	void Start () {

		//initializing base stats

		currentHP = 80;
		offensive = 0;
	
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (bonus);
		Debug.Log (offensive);


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
		offenseInp = GUI.TextField (new Rect(500, 60, 100, 30), bonus.ToString(), 2);





		//solo prueba que este checando lo que se mete
		GUI.Box (new Rect (140, 300, 400, 400), "HP: "+ baseHP + "\ncurrentHP: " + currentHP + "\nbaseAtk: " + baseAtk +
		        "\ncurrentATK: " + currentAtk + "\nbasedef: " + baseDef + "\ncurrentDef: " + currentDef +
		        "\nspeed: " + speed);

		//Buttons in GUI
		#region $Buttons$

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
			bonus += 5;


		}
		//presicion button [swiftness/supersentidos]
		if (GUI.Button (new Rect (580, 150, 70, 70), "Swift"))
		{
			Presicion ();
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
		//base attack
		currentAtk += baseAtk;
		//close combat attack

		//calls critical hit damage * 2
		CriticalHit ();
		//adds offense
		AdiditonalDamage();
		//Supersentidos o no?
		Presicion ();

		if (precision >= 40)
		{
			currentAtk += baseAtk;
		}
		if (precision <= 40)
		{
			currentAtk = 0;
		}


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
		baseAtk = 40;
		//base attack
		currentAtk += baseAtk;

		//range attack
		CriticalHit ();
		//adds offense
		AdiditonalDamage();
		//Supersentidos o no?
		Presicion ();
		
		if (precision >= 60)
		{
			currentAtk += baseAtk;
		}
		if (precision <= 60)
		{
			currentAtk = 0;
		}
	

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

		//default val
		baseAtk = 50;
		//base attack
		currentAtk += baseAtk;

		//super attack
		CriticalHit ();
		//adds offense
		AdiditonalDamage();
		//Supersentidos o no?
		Presicion ();
		
		if (precision >= 80)
		{
			currentAtk += baseAtk;
		}
		if (precision <= 80)
		{
			currentAtk = 0;
		}
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

	/*supersentidos*/

	//the posibility than an attack its succesful or not

	void Presicion() {

		//presicion base
		presNum = 40;
		//random del bonificador del personaje en este caso
		//generating random
		supersentidos = Random.Range(0,40);
		Debug.Log(supersentidos);
		//sumando los bonus
		precision = presNum + supersentidos;
		Debug.Log(precision);



	
		/*if (precision <= 40)
		{
			//aqui iria el ataque a uno mismo si fallas?
		}
*/

	}

	//el golpe critico se obtiene con un random en rango de 1 a 10


	void CriticalHit () {

		//generating random
		criticalHit = Random.Range(1,10);
		Debug.Log(criticalHit);
	}

	//buffs and debuffs

	void AdiditonalDamage() {


		offensive += baseAtk * bonus / 100;
	}

	#endregion $bonuses$



	
}