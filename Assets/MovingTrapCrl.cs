using UnityEngine;

//[ExecuteAlways] // để update cả trong Editor
public class MovingTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer; // sprite gốc (1x1)
    [SerializeField] private int length = 5;                // số mảnh ngang
    [SerializeField] private float height = 1f;             // số mảnh dọc (nếu muốn)
    [SerializeField] private float speed = 3f;              // tốc độ
    [SerializeField] private float moveDistance = 5f;       // quãng đường di chuyển

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
        BuildTrap();
    }

    void Update()
    {
        MoveTrap();
    }

    /// <summary>
    /// Xây trap bằng cách scale hoặc lặp sprite
    /// </summary>
    private void BuildTrap()
    {
        if (spriteRenderer == null) return;

        // Dùng tiling thay vì clone nhiều object
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = new Vector2(length, height);
    }

    private void MoveTrap()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
            float clampedX = Mathf.Clamp(transform.position.x, startPos.x - moveDistance, startPos.x + moveDistance);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}