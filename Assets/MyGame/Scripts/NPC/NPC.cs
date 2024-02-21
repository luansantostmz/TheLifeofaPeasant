using UnityEngine;

public class NPC : MonoBehaviour
{

	[SerializeField] private float plantDistance = 2f; // Distância mínima para plantar
	[SerializeField] private float plantTime = 2f; // Tempo necessário para plantar (em segundos)
	[SerializeField] private float speedNpc;

	public GameObject seed; // Referência ao objeto semente a ser plantado
	public GameObject soilPrefab; // Prefab do terreno

	

	private enum NPCState
	{
		Idle,
		Planting,
		Harvesting
	}

	private NPCState currentState = NPCState.Idle;
	private GameObject currentSoil; // Solo atualmente selecionado para plantar ou colher
	private GameObject currentDrop;
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
						transform.position += directionToSoil.normalized * Time.deltaTime * speedNpc;
					}
				}
				break;
			case NPCState.Harvesting:
				if (currentDrop != null)
				{
					Vector3 directionToDrop = currentDrop.transform.position - transform.position;
					float distanceToDrop = directionToDrop.magnitude;

					if (distanceToDrop <= plantDistance)
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
						transform.position += directionToDrop.normalized * Time.deltaTime * speedNpc;
					}
				}
				break;
			default:
				break;
		}
	}

	private void CheckTasks()
	{
		if (currentState == NPCState.Idle)
		{
			//Soil
			GameObject[] soilObjects = GameObject.FindGameObjectsWithTag("Soil");			
			currentSoil = FindNearestSoil(soilObjects);			

			if (currentSoil != null)
			{
				currentState = NPCState.Planting;
			}

			//DROP
			/*GameObject[] dropObjects = GameObject.FindGameObjectsWithTag("Drop");
			currentSoil = FindNearestSoil(dropObjects);

			if (currentDrop != null)
			{
				currentState = NPCState.Harvesting;
			}*/
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
	private GameObject FindNearestDrop(GameObject[] dropObjects)
	{
		GameObject nearestDrop = null;
		float shortestDistance = Mathf.Infinity;

		foreach (GameObject drop in dropObjects)
		{
			float distance = Vector3.Distance(transform.position, drop.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				nearestDrop = drop;
			}
		}

		return nearestDrop;
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
