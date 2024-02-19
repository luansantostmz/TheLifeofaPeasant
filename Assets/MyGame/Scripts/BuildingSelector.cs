using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelector : MonoBehaviour
{
	public enum BuildingType { House, FarmerHouse, FoodStorage, ToolStorage }

	[System.Serializable]
	public class BuildingPrefab
	{
		public BuildingType type;
		public GameObject prefab;
	}

	public BuildingPrefab[] buildingPrefabs; // Array de prefabs de construção

	// Método a ser chamado pelo botão
	public void DestroyAndReplace()
	{
		// Obtém a posição do objeto atual
		Vector3 position = transform.position;

		// Encontra o prefab correspondente ao tipo de construção selecionado
		GameObject prefabToInstantiate = null;
		foreach (var buildingPrefab in buildingPrefabs)
		{
			if (buildingPrefab.type == 0 )
			{
				prefabToInstantiate = buildingPrefab.prefab;
				break;
			}
		}

		// Instancia o novo objeto na mesma posição
		if (prefabToInstantiate != null)
		{
			Instantiate(prefabToInstantiate, position, Quaternion.identity);
		}

		// Destroi o objeto atual
		Destroy(gameObject);
	}
}
