using UnityEngine;

public class CharacterCamSpawn : MonoBehaviour
{
    [SerializeField] GameObject Character; // Prefab of the character
    GameObject ActChar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Character)
        {
            ActChar = Instantiate(Character, transform.position, Quaternion.identity);
            MainInstance.I.ActChar = ActChar.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ActChar)
        MainInstance.I.ActChar = ActChar.transform;
    }
}
