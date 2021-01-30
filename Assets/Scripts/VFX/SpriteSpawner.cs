using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spritePrefab;
    [SerializeField]
    private float lifetime = 1f;
    [SerializeField]
    private Color color = Color.white;
    public void Spawn(Vector3 position)
    {
        SpriteRenderer spriteRenderer = Instantiate(spritePrefab, position, Quaternion.identity).GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
            Destroy(spriteRenderer.gameObject, lifetime);
        }
    }
    public void Spawn()
    {
        Spawn(transform.position);
    }
}
