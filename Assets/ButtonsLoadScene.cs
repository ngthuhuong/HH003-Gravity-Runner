using UnityEngine;
using UnityEngine.UI; // Quan trọng: để dùng Button
using UnityEngine.SceneManagement;

public class ButtonsLoadScene : MonoBehaviour
{
    private Button btn;

    void Awake()
    {
        // Lấy component Button từ chính GameObject này
        btn = GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level_1");
            });
        }
        else
        {
            Debug.LogError("Không tìm thấy Button component trên GameObject " + gameObject.name);
        }
    }
}