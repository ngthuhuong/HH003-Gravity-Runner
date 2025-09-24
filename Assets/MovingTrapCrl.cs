using UnityEngine;

//[ExecuteAlways] // nếu muốn update cả trong Editor
[RequireComponent(typeof(BoxCollider2D))]
public class MovingTrap : MonoBehaviour
{
    [Header("----Trap Settings----")]
    [SerializeField] private SpriteRenderer spriteRenderer; // sprite gốc (1x1)
    [SerializeField] private int length = 5;                // số mảnh ngang
    [SerializeField] private float height = 1f;             // số mảnh dọc
    [SerializeField] private float speed = 3f;              // tốc độ
    [SerializeField] private float moveDistance = 5f;       // quãng đường di chuyển

    private Vector3 startPos;
    private int direction = 1;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

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
    /// Xây trap bằng cách scale sprite + update collider
    /// </summary>
    private void BuildTrap()
    {
        if (spriteRenderer == null) return;

        // Set tiled mode
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = new Vector2(length, height);

        // Update collider khớp sprite
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(length, height);
            
        }
    }

    private void MoveTrap()
    {
        transform.Translate( direction * speed * Time.deltaTime*Vector3.right );

        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
            float clampedX = Mathf.Clamp(transform.position.x, startPos.x - moveDistance, startPos.x + moveDistance);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}
