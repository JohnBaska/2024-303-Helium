using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightBeamController : MonoBehaviour
{ 
    public TextMeshProUGUI contadorText;
    private LineRenderer lineRenderer;
    public float moveSpeed = 3f;
    public Transform posInicial;

    public GameObject objectToDuplicate;
    GameObject objetoAtual;

    private int contador = 0; 
    public int direcao_move_x;
    public int direcao_move_y;
    public bool movimento;

    void Start()
    {
	GameObject[] allGridTiles ;
	allGridTiles = GameObject.FindGameObjectsWithTag("gridTile");

	foreach (GameObject tile in allGridTiles)
	{
	    Debug.Log("making " + gameObject.name + " ignore colision with " + tile.name);
	    Physics2D.IgnoreCollision(tile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}


	lineRenderer = GetComponent<LineRenderer>();
        movimento = false;
	lineRenderer.startWidth = 0.4f;
	lineRenderer.endWidth = 0.4f;
    }

    void Update (){
        if (movimento == true){

            // Movimento com setas do teclado
	    float moveX = direcao_move_x * moveSpeed * Time.deltaTime;
	    float moveY = direcao_move_y * moveSpeed * Time.deltaTime;
	    transform.Translate(moveX, moveY, 0);

            // Atualiza a posição do feixe de luz
            lineRenderer.SetPosition(0, posInicial.position);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
	    Debug.Log("Buceta de game engine");
	    Debug.Log(col.transform.position);
	    lineRenderer.SetPosition(lineRenderer.positionCount - 1, col.transform.position);
	    GetComponent<Transform>().position = col.transform.position;
	    lineRenderer.positionCount += 1;
	    direcao_move_x = 1;
	    direcao_move_y = 0;
    }

    // Método chamado quando uma colisão 2D termina
    void OnCollisionEnter2D(Collision2D collision)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

        if (collision.gameObject.name.Contains("maca")){
            collision.gameObject.SetActive(false);

            contador++;

            // Inicia o contador com zero
            contadorText.text = contador.ToString();
        }

        if (collision.gameObject.tag == "ref_right_up")
        {
            if (direcao_move_x == 1 && direcao_move_y == 0)
            {
                direcao_move_x = 0;
                direcao_move_y = 1;
            }else{
                direcao_move_x = -1;
                direcao_move_y = 0;
            }
        }else if(collision.gameObject.tag == "ref_right_down"){
            if (direcao_move_x == 1 && direcao_move_y == 0)
            {
                direcao_move_x = 0;
                direcao_move_y = -1;
            }else{
                direcao_move_x = -1;
                direcao_move_y = 0;
            }
        }else if(collision.gameObject.tag == "ref_up_right"){
            if (direcao_move_x == -1 && direcao_move_y == 0)
            {
                direcao_move_x = 0;
                direcao_move_y = 1;
            }else{
                direcao_move_x = 1;
                direcao_move_y = 0;
            }
        }else if(collision.gameObject.tag == "ref_down_right"){
            if (direcao_move_x == 0 && direcao_move_y == 1)
            {
                direcao_move_x = 1;
                direcao_move_y = 0;
            }else{
                direcao_move_x = 0;
                direcao_move_y = -1;
            }
        }
    }
}
