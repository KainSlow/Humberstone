using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    [SerializeField] GameObject PopUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SceneManager.GetActiveScene().name == "Town")
        {
            PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "¿Ir a la Zona de extracción?";
        }else if(SceneManager.GetActiveScene().name == "TownNight")
        {
            PopUp.GetComponentInChildren<TextMeshProUGUI>().text = "¿Ir a dormir?";
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
