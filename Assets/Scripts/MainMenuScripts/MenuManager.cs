using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void QuitGame(){
        Application.Quit(); 
    }

    public void ShowControls(){
        
    }
}
