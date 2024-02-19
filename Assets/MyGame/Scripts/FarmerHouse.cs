using UnityEngine;

public class FarmerHouse : MonoBehaviour
{
	public GameObject objectToSpawn; // Objeto a ser instanciado
	public GameObject spawnDistance; // Distância à frente do objeto para spawnar

	public House house; // Referência ao script da casa
	private int maxObjectsToSpawn; // Número máximo de objetos a serem instanciados

	private int levelFarmerHouse;	

	private void Update()
	{
		CalculateMaxObjectsToSpawn(); // Calcula o número máximo de objetos a serem instanciados

		// Verifica se ainda podemos instanciar mais objetos
		if (CountObjectsToSpawn() < maxObjectsToSpawn)
		{
			// Instancia um objeto à frente do objeto atual			
			Instantiate(objectToSpawn, spawnDistance.transform.position, Quaternion.identity);
		}
	}

	// Método para contar quantos objetos já foram instanciados
	private int CountObjectsToSpawn()
	{
		GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag(objectToSpawn.tag);
		return spawnedObjects.Length;
	}

	// Método para calcular o número máximo de objetos a serem instanciados com base no nível da casa
	private void CalculateMaxObjectsToSpawn()
	{
		if (house != null)
		{
			maxObjectsToSpawn = house.houseLevel;
		}
	}
}
