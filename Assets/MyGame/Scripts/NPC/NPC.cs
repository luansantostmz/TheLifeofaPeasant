using UnityEngine;

public class NPC : MonoBehaviour
{
	public GameObject seed; // Refer�ncia ao objeto semente a ser plantado
	public float moveSpeed = 3f; // Velocidade de movimento do NPC
	public float plantingDistance = 2f; // Dist�ncia entre o NPC e o objeto "Solo" durante o plantio

	private enum NPCState
	{
		Idle,
		Planting
	}

	private NPCState currentState = NPCState.Idle;
	private GameObject[] soilObjects; // Lista de objetos com a tag "Soil"
	private GameObject currentSoil; // Solo atualmente selecionado para plantar
	private float plantTimer = 0f; // Temporizador para controlar o plantio

	private void Start()
	{
		InvokeRepeating("CheckTasks", 0f, 1f); // Verifica as tarefas a cada segundo
	}

	private void Update()
	{
		if (currentState == NPCState.Planting)
		{
			// Verifica se h� solo selecionado
			if (currentSoil != null)
			{
				// Calcula a dire��o e a dist�ncia para o solo
				Vector3 directionToSoil = currentSoil.transform.position - transform.position;
				directionToSoil.y = 0f; // Garante que o NPC fique na mesma altura do solo
				float distanceToSoil = directionToSoil.magnitude;

				// Se o NPC estiver longe demais, move-se em dire��o ao solo
				if (distanceToSoil > plantingDistance)
				{
					transform.position += directionToSoil.normalized * moveSpeed * Time.deltaTime;
				}
				else
				{
					// Caso contr�rio, ele est� perto o suficiente para plantar
					// Aguarda por 2 segundos antes de plantar
					plantTimer += Time.deltaTime;
					if (plantTimer >= 2f)
					{
						Plant(seed, currentSoil); // Planta a semente
						Destroy(currentSoil); // Destr�i o solo
						currentState = NPCState.Idle; // Retorna ao estado de repouso
					}
				}
			}
			else
			{
				// Se o solo n�o estiver mais dispon�vel, volta ao estado ocioso
				currentState = NPCState.Idle;
			}
		}
	}

	private void CheckTasks()
	{
		if (currentState == NPCState.Idle)
		{
			// Verifica se h� solo dispon�vel para plantar
			soilObjects = GameObject.FindGameObjectsWithTag("Soil");

			if (soilObjects.Length > 0)
			{
				// Seleciona um solo aleat�rio para plantar
				currentSoil = soilObjects[Random.Range(0, soilObjects.Length)];
				currentState = NPCState.Planting; // NPC entra no estado de plantio
			}
		}
	}

	private void Plant(GameObject seed, GameObject soil)
	{
		// L�gica para plantar a semente no solo
		Instantiate(seed, soil.transform.position, Quaternion.identity); // Instancia a semente
	}
}
