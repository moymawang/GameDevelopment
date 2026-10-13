
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChooseBeginner()
    {
        SceneManager.LoadScene("Beginner");
    }

    public void ChoosePro()
    {
        SceneManager.LoadScene("Pro");
    }
}
