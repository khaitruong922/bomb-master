using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private int length = 2;
    public Action<Vector3> OnExploded { get; set; }
    private bool hasExploded = false;
    private Collider2D bombCollider;
    private void Awake() {
        bombCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Explosion explosion = other.GetComponent<Explosion>();
        if (explosion != null) Explode();
    }
    // private void OnTriggerExit2D(Collider2D other) {
    //     BombPlacer bombPlacer = other.GetComponent<BombPlacer>();
    //     if (bombPlacer != null)bombCollider.isTrigger = false;
    // }
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime > 0) return;
        Explode();
    }
    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CreateExplosions(Vector2.up);
        CreateExplosions(Vector2.down);
        CreateExplosions(Vector2.left);
        CreateExplosions(Vector2.right);
        OnExploded?.Invoke(transform.position);
        Destroy(gameObject);
    }
    private void CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i <= length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, i, wallLayerMask);
            Collider2D collider = hit.collider;
            Vector3 explosionPos = transform.position + (i * direction);
            if (collider)
            {
                collider.GetComponent<DestructibleTilemap>()?.DestroyTile(explosionPos);
                break;
            }
            Instantiate(explosionPrefab, explosionPos, Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        OnExploded = null;
    }

}
