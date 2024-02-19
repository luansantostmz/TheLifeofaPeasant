using UnityEngine;

public class NPC : MonoBehaviour
{
	public GameObject seed; // Referência ao objeto semente a ser plantado
	public GameObject soilPrefab; // Prefab do terreno
	public float plantDistance = 2f; // Distância mínima para plantar
	public float plantTime = 2f; // Tempo necessário para plantar (em segundos)

	private enum NPCState
	{
		Idle,
		Planting,
		Harvesting
	}

	private NPCState currentState = NPCState.Idle;
	private GameObject currentSoil; // Solo atualmente selecionado para plantar ou colher
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
				// Implemente a lógica de colheita aqui
				break;
			default:
				break;
		}
	}

	private void CheckTasks()
	{
		if (currentState == NPCState.Idle)
		{
			GameObject[] soilObjects = GameObject.FindGameObjectsWithTag("Soil");
			currentSoil = FindNearestSoil(soilObjects);

			if (currentSoil != null)
			{
				currentState = NPCState.Planting;
			}
		}
	}

	private GameObject FindNearestSoil(GameObject[] soilObjects)
	{
		GameObject nearestSoil = null;
		float shortestDistance = Mathf.Infinity;

		foreach (GameObject soil in soilObjects)
		{
			float distance = Vector3.Distance(transform.position, soil.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				nearestSoil = soil;
			}
		}

		return nearestSoil;
	}

	private void Plant(GameObject seed, GameObject soil)
	{
		Instantiate(seed, soil.transform.position, Quaternion.identity);
		Destroy(soil);
	}

	// Método para desenhar um Gizmo para visualizar a distância de plantio
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, plantDistance);
	}
}
