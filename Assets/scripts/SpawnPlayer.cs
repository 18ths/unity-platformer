using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

    public GameObject playerPrefab;

    public GameObject Spawn()
    {
        return Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
    }
}
