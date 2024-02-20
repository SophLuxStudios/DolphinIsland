using UnityEngine;

public class ObstacleMaterialAssigner : MonoBehaviour
{
    [SerializeField] private Material[] obstacleMaterial;

    private void Awake()
    {
        Material();
    }

    private void Material()
    {
        GetComponent<Renderer>().material = obstacleMaterial[Random.Range(0, obstacleMaterial.Length)];
    }
}