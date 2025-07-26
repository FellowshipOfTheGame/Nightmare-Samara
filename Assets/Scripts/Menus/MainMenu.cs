using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Troca para a próxima cena (ajuste o nome ou índice)
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame()
    {
        // Encerra o jogo
        Application.Quit();

        // Apenas para testes no editor
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
