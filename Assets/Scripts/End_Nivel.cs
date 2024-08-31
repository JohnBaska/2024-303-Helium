using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Nivel : MonoBehaviour
{
    [HideInInspector]
    public Scene previousScene;
    private string namePS;
    private int level;

    void Start(){
        Debug.Log(previousScene.name);
    }

    public void Next_level(){
        Debug.Log("Next_level");

        namePS = previousScene.name;

        level = int.Parse(namePS.Replace("nivel ", ""));

        SceneManager.LoadScene("nivel " + (level + 1).ToString());
    }

    public void Restart_level(){
        Debug.Log("Restart_level");
        SceneManager.LoadScene(previousScene.name);
    }

    public void Voltar_menu (){
        Debug.Log("Voltar_menu");
        SceneManager.LoadScene("Niveis");
    }
}
