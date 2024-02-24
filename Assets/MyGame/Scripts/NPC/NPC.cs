using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
	[SerializeField] private float actionDistance = 2f; // Distância mínima para ação
	[SerializeField] private float actionTime = 2f; // Tempo necessário para ação (em segundos)
	[SerializeField] private float speedNpc;

	public GameObject seed; // Referência ao objeto semente a ser plantado
	public GameObject soilPrefab; // Prefab do terreno


	NavMeshAgent agent;

	public enum NPCState
	{
		Idle,
		Planting,
		Harvesting
	}

	public NPCState currentState = NPCState.Idle;
	private GameObject currentTarget; // Objeto atualmente selecionado para ação (plantar ou colher)
	private float actionTimer = 0f; // Temporizador para controlar a ação

	private void Start()
	{
		InvokeRepeating("CheckTasks", 0f, 1f); // Verifica as tarefas a cada segundo

		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

		agent.speed = speedNpc;
	}

	private void Update()
	{
		switch (currentState)
		{
			case NPCState.Planting:
			case NPCState.Harvesting:
				if (currentTarget != null)
				{
					Vector3 directionToTarget = currentTarget.transform.position - transform.position;
					float distanceToTarget = directionToTarget.magnitude;

					if (distanceToTarget <= actionDistance)
					{
						actionTimer += Time.deltaTime;
						if (actionTimer >= actionTime)
						{
							if (currentState == NPCState.Planting)
								Plant(seed, currentTarget);
							else if (currentState == NPCState.Harvesting)
								Harvest(currentTarget);

							currentState = NPCState.Idle;
						}
					}
					else
					{
						GoToTarget(directionToTarget);
						//transform.position += directionToTarget.normalized * Time.deltaTime * speedNpc;
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
			if (currentState != NPCState.Planting)
			{
				GameObject[] soilObjects = GameObject.FindGameObjectsWithTag("Soil");
				currentTarget = FindNearestObject(soilObjects);
				if (currentTarget != null)
				{
					currentState = NPCState.Planting;
					return;
				}
			}

			if (currentState != NPCState.Harvesting)
			{
				GameObject[] harvestableObjects = GameObject.FindGameObjectsWithTag("Harvestable");
				currentTarget = FindNearestObject(harvestableObjects);
				if (currentTarget != null)
				{
					currentState = NPCState.Harvesting;
					return;
				}
			}
		}
	}

	private GameObject FindNearestObject(GameObject[] objects)
	{
		GameObject nearestObject = null;
		float shortestDistance = Mathf.Infinity;

		foreach (GameObject obj in objects)
		{
			float distance = Vector3.Distance(transform.position, obj.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				nearestObject = obj;
			}
		}

		return nearestObject;
	}

	private void Plant(GameObject seed, GameObject soil)
	{
		Instantiate(seed, soil.transform.position, Quaternion.identity);
		Destroy(soil);
	}

	private void Harvest(GameObject harvestable)
	{
		// Implementar lógica de colheita aqui
		Debug.Log("Colheita de " + harvestable.name);
		Destroy(harvestable);
	}

	// Método para desenhar um Gizmo para visualizar a distância de ação
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, actionDistance);
	}

	public void GoToTarget(Vector2 target) 
	{
		agent.SetDestination(target);
	}
}
