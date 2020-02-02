using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArmDirection : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    public Rigidbody2D rb;
    public Tilemap hooks;
    private List<Vector2> hooksList = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        var bounds = new BoundsInt(hooks.origin, hooks.size);
        foreach (var position in bounds.allPositionsWithin)
        {
            if (hooks.GetTile(position) != null)
            {
                hooksList.Add(new Vector2(position.x, position.y));
                // Debug.Log(position);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3();
        Vector3 mousePosWorld = new Vector3();
        Vector2 mousePosWorldToCharacter = new Vector3();

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        mousePos.z = 1.0f;

        mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        mousePosWorldToCharacter.x = ((rb.position.x - mousePosWorld.x) < 0 ? mousePosWorld.x - Mathf.Abs((rb.position.x - mousePosWorld.x) * 2) : mousePosWorld.x + Mathf.Abs((rb.position.x - mousePosWorld.x) * 2));
        mousePosWorldToCharacter.y = ((rb.position.y - mousePosWorld.y) < 0 ? mousePosWorld.y - Mathf.Abs((rb.position.y - mousePosWorld.y) * 2) : mousePosWorld.y + Mathf.Abs((rb.position.y - mousePosWorld.y) * 2));

        // Debug.DrawLine(rb.position, new Vector3(mousePosWorldToCharacter.x, mousePosWorldToCharacter.y, mousePosWorld.z), Color.white, Time.deltaTime);

        snapToNearestHook(mousePosWorldToCharacter);
    }

    void snapToNearestHook(Vector2 mousePos)
    {
        var bestTarget = new Vector2(Mathf.Infinity, Mathf.Infinity);
        var distanceToCurrentBestTarget = Vector2.Distance(bestTarget, mousePos);
        foreach (Vector2 hook in hooksList)
        {
            var distanceToCurrentHook = Vector2.Distance(hook, mousePos);
            if (distanceToCurrentHook < 2 && distanceToCurrentHook < distanceToCurrentBestTarget)
            {
                bestTarget = hook;
                distanceToCurrentBestTarget = distanceToCurrentHook;
            }
        }

       // Debug.Log("Closest to position " + mousePos + " = " + bestTarget);
        
        foreach(Vector2 hook in hooksList)
        {
            var position = new Vector3Int((int)hook.x, (int)hook.y, 0);

            hooks.SetTileFlags(position, TileFlags.None);

            // Set the colour.
            hooks.SetColor(position, Color.gray);
        }

        var bestTargetPosition = new Vector3Int((int)bestTarget.x, (int)bestTarget.y, 0);
        //Highlight the best grapple target
        hooks.SetTileFlags(bestTargetPosition, TileFlags.None);

        // Set the colour.
        hooks.SetColor(bestTargetPosition, Color.white);
    }
}
