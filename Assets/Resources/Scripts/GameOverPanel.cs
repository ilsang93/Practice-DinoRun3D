using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        gameObject.SetActive(false);
    }
}
