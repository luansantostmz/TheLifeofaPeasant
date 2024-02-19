using UnityEngine;

public class House : MonoBehaviour
{
	public int houseLevel = 1; // Nível inicial da casa

	// Método para aumentar o nível da casa
	public void UpgradeHouse()
	{
		houseLevel++; // Incrementa o nível da casa
		Debug.Log("A casa foi atualizada para o nível " + houseLevel);
		// Aqui você pode adicionar lógica adicional, como alterações visuais ou estatísticas da casa
	}
}
