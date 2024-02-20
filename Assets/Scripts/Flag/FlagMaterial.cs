using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FlagMaterial : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material = FlagData.ownedFlags[PlayerPrefs.GetInt("EquippedFlag", 0)].material;

        //Debug.Log("Equipped flag index is: " + PlayerPrefs.GetInt("EquippedFlag"));
    }
}