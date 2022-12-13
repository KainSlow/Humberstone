using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    [SerializeField] GameObject PopUp;
    LevelLoader ll;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SceneManager.GetActiveScene().name == "Town")
        {
            PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "�Ir a la Zona de extracci�n?";
        }else if(SceneManager.GetActiveScene().name == "TownNight")
        {
            PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "�Ir a dormir?";
        }else if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "�Volver al men�?";
        } 

        if (collision.CompareTag("Player"))
        {
            PopUp.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PopUp.SetActive(false);
        }
    }
}
