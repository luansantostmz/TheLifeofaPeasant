using UnityEngine;

public class House : MonoBehaviour
{
	public int houseLevel = 1; // N�vel inicial da casa

	// M�todo para aumentar o n�vel da casa
	public void UpgradeHouse()
	{
		houseLevel++; // Incrementa o n�vel da casa
		Debug.Log("A casa foi atualizada para o n�vel " + houseLevel);
		// Aqui voc� pode adicionar l�gica adicional, como altera��es visuais ou estat�sticas da casa
	}
}
