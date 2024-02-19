using UnityEngine;

public class FarmerHouse : MonoBehaviour
{
	public GameObject objectToSpawn; // Objeto a ser instanciado
	public GameObject spawnDistance; // Dist�ncia � frente do objeto para spawnar

	public House house; // Refer�ncia ao script da casa
	private int maxObjectsToSpawn; // N�mero m�ximo de objetos a serem instanciados

	private int levelFarmerHouse;	

	private void Update()
	{
		CalculateMaxObjectsToSpawn(); // Calcula o n�mero m�ximo de objetos a serem instanciados

		// Verifica se ainda podemos instanciar mais objetos
		if (CountObjectsToSpawn() < maxObjectsToSpawn)
		{
			// Instancia um objeto � frente do objeto atual			
			Instantiate(objectToSpawn, spawnDistance.transform.position, Quaternion.identity);
		}
	}

	// M�todo para contar quantos objetos j� foram instanciados
	private int CountObjectsToSpawn()
	{
		GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag(objectToSpawn.tag);
		return spawnedObjects.Length;
	}

	// M�todo para calcular o n�mero m�ximo de objetos a serem instanciados com base no n�vel da casa
	private void CalculateMaxObjectsToSpawn()
	{
		if (house != null)
		{
			maxObjectsToSpawn = house.houseLevel;
		}
	}
}
