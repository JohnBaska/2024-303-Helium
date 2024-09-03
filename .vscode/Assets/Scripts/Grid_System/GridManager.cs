using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _columns, _rows;
    [SerializeField] private GameObject _tilePrefab;

    public bool doDevMode = false;

    private float width; 
    private float height;

    private float offsetX;
    private float offsetY;

    private float TILE_WIDTH;
    private float TILE_HEIGHT;

    void Start()
    {
	TILE_WIDTH = _tilePrefab.transform.localScale.x;
	TILE_HEIGHT = _tilePrefab.transform.localScale.y;

	width  = _columns * TILE_WIDTH; 
	height = _rows * TILE_HEIGHT;

	offsetX = (width/2 - TILE_WIDTH/2) * -1; 
	offsetY = (height/2 - TILE_HEIGHT/2) * -1;

	GenerateGrid();

	Debug.Log("OFFSET X: " + offsetX);
	Debug.Log("OFFSET Y: " + offsetY);

	GameObject sol = GameObject.Find("SOL");
	GameObject raio = GameObject.Find("LightSource");
	GameObject primeiroTile = GameObject.Find("Tile(0:8)");

	sol.GetComponent<Transform>().position = primeiroTile.GetComponent<Transform>().position +
	    new Vector3(0, 3, 0);
	raio.GetComponent<Transform>().position = sol.GetComponent<Transform>().position; 

	raio.GetComponent<LineRenderer>().SetPosition(0, sol.GetComponent<Transform>().position);
        raio.GetComponent<LineRenderer>().SetPosition(raio.GetComponent<LineRenderer>().positionCount - 1, sol.GetComponent<Transform>().position);
    }
    void GenerateGrid()
    {
	for(int x = 0; x < _columns; x++)
	{
	    for(int y = 0; y < _rows; y++)
	    {
		InstantiateTile(x,y);
	    }
	}
    }

    void InstantiateTile(int x, int y)
    {
	float posX = (_tilePrefab.transform.localScale.x * (float)x) + offsetX;
	float posY = (_tilePrefab.transform.localScale.y * (float)y) + offsetY;

	var spawnedTile = Instantiate(_tilePrefab, new Vector3(posX, posY, 0), Quaternion.identity);
	spawnedTile.name = $"Tile({x}:{y})";
	spawnedTile.GetComponent<GridCell>().DevMode = doDevMode;
    }
}
