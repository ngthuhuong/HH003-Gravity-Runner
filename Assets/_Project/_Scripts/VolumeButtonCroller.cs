using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Image volumeImage;      // Image hiển thị icon volume
    public Sprite unmuteSprite;    // Sprite khi không mute
    public Sprite muteSprite;      // Sprite khi mute

    private bool isMuted = false;  // Biến lưu trạng thái mute

    // Hàm gọi khi nhấn nút volume
    public void ToggleMute()
    {
        isMuted = !isMuted;  // Đổi trạng thái

        if (isMuted)
        {
            volumeImage.sprite = muteSprite;
            AudioListener.volume = 0f;  // Tắt âm toàn bộ
        }
        else
        {
            volumeImage.sprite = unmuteSprite;
            AudioListener.volume = 1f;  // Bật âm toàn bộ
        }
    }
}