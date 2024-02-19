using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
	public GameObject objectToActivate; // Objeto a ser ativado quando clicado
	public List<GameObjectNamePair> objectsToInstantiate; // Lista de objetos a serem instanciados

	private void Update()
	{
		// Verifica se o botão esquerdo do mouse foi pressionado
		if (Input.GetMouseButtonDown(0))
		{
			// Verifica se o clique atingiu este objeto
			if (IsPointerOverObject())
			{
				// Ativa o objeto desejado
				if (objectToActivate != null)
				{
					objectToActivate.SetActive(true);
				}
			}
		}

		// Verifica se houve toque na tela
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			// Verifica se o toque atingiu este objeto
			if (IsPointerOverObject())
			{
				// Ativa o objeto desejado
				if (objectToActivate != null)
				{
					objectToActivate.SetActive(true);
				}
			}
		}
	}

	private bool IsPointerOverObject()
	{
		// Converte a posição do clique/tap para um raio na cena
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -Camera.main.transform.position.z;
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

		// Verifica se o raio atingiu este objeto
		if (hit.collider != null && hit.collider.gameObject == gameObject)
		{
			return true;
		}

		return false;
	}

	// Método para instanciar objetos da lista
	public void InstantiateObject(int index)
	{
		Destroy(gameObject);

		if (index >= 0 && index < objectsToInstantiate.Count)
		{
			GameObjectNamePair pair = objectsToInstantiate[index];
			Instantiate(pair.gameObject, transform.position, Quaternion.identity);
		}
		else
		{
			Debug.LogError("Index out of range.");
		}
	}
}

// Classe para armazenar pares de GameObjects e seus nomes
[System.Serializable]
public class GameObjectNamePair
{
	public string name; // Nome do objeto
	public GameObject gameObject; // Objeto associado
}
