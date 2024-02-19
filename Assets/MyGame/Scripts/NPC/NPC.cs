using UnityEngine;

public class NPC : MonoBehaviour
{
	public GameObject seed; // Refer�ncia ao objeto semente a ser plantado
	public GameObject soilPrefab; // Prefab do terreno
	public float plantDistance = 2f; // Dist�ncia m�nima para plantar
	public float plantTime = 2f; // Tempo necess�rio para plantar (em segundos)

	private enum NPCState
	{
		Idle,
		Planting,
		Harvesting
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
		switch (currentState)
		{
			case NPCState.Planting:
				if (currentSoil != null)
				{
					Vector3 directionToSoil = currentSoil.transform.position - transform.position;
					float distanceToSoil = directionToSoil.magnitude;

					if (distanceToSoil <= plantDistance)
					{
						plantTimer += Time.deltaTime;
						if (plantTimer >= plantTime)
						{
							Plant(seed, currentSoil);
							currentState = NPCState.Idle;
						}
					}
					else
					{
						transform.position += directionToSoil.normalized * Time.deltaTime;
					}
				}
				break;
			case NPCState.Harvesting:
				// Implemente a l�gica de colheita aqui
				break;
			default:
				break;
		}
	}

	private void CheckTasks()
	{
		if (currentState == NPCState.Idle)
		{
			soilObjects = GameObject.FindGameObjectsWithTag("Soil");

			if (soilObjects.Length > 0)
			{
				currentSoil = soilObjects[Random.Range(0, soilObjects.Length)];
				currentState = NPCState.Planting;
			}
		}
	}

	private void Plant(GameObject seed, GameObject soil)
	{
		Instantiate(seed, soil.transform.position, Quaternion.identity);
		Destroy(soil);
	}

	// M�todo para desenhar um Gizmo para visualizar a dist�ncia de plantio
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, plantDistance);
	}
}
