using UnityEngine;
using System.Collections.Generic;
using MoreMountains.Tools;

public class RoadController : MonoBehaviour
{
    [SerializeField] private GameObject road;
    [SerializeField] public List<GameObject> levels;
    private GameObject currentLevel;
    private int currentLevelIndex = 0;

    [SerializeField] private GameObject frame; // Frame 2x2

    void Start()
    {
        if (GameManager.Instance.isLoaded)
        {
            currentLevelIndex = GameManager.Instance.Level;
            if (levels.Count == 0)
            {
                Debug.LogError("No levels assigned in the inspector!");
                return;
            }
            foreach (var level in levels)
            {
                foreach (Transform child in level.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }

            if (currentLevelIndex - 1 >= 0 && currentLevelIndex - 1 < levels.Count)
            {
                currentLevel = levels[currentLevelIndex - 1];
                foreach (Transform child in currentLevel.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }else if (currentLevelIndex - 1 >= levels.Count)
            {
                // Nếu vượt quá số level, giữ ở level cuối
                currentLevel = levels[levels.Count - 1];
                foreach (Transform child in currentLevel.transform)
                {
                    child.gameObject.SetActive(true);
                }
                MMEventManager.TriggerEvent(new NoMap());
            }
            else
            {
                Debug.LogError("Invalid level index or levels list is empty!");
            }

            inFrameCheck();
        }
    }

    void inFrameCheck()
    {
        if (frame == null || currentLevel == null)
        {
            Debug.LogError("Frame or current level is not assigned!");
            return;
        }

        // Lấy bounds của frame
        Bounds frameBounds = GetBounds(frame);

        // Lấy bounds của currentLevel
        Bounds levelBounds = GetBounds(currentLevel);

        // Kiểm tra giao nhau
        if (frameBounds.Intersects(levelBounds))
        {
            Debug.Log($"Level {currentLevelIndex} is inside or intersects with the frame!");
        }
        else
        {
            Debug.Log($"Level {currentLevelIndex} is outside the frame! Moving road inside...");

            // Tìm điểm gần nhất trên frame từ trung tâm của level
            Vector3 levelCenter = levelBounds.center;
            Vector3 closestPoint = frameBounds.ClosestPoint(levelCenter);
            Vector3 moveVector = closestPoint - levelCenter;
            road.transform.position += moveVector;

            Debug.Log($"Moved road by vector: {moveVector}");
        }
    }


    private Bounds GetBounds(GameObject obj)
    {
        // Tìm Renderer hoặc Collider để lấy bounds
        Renderer renderer = obj.GetComponent<Renderer>();
        Collider2D collider2D = obj.GetComponent<Collider2D>();

        if (renderer != null)
        {
            return renderer.bounds;
        }
        else if (collider2D != null)
        {
            return collider2D.bounds;
        }
        else
        {
            // Nếu không có Renderer hoặc Collider, tính bounds từ tất cả các con
            Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
            Renderer[] childRenderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer childRenderer in childRenderers)
            {
                bounds.Encapsulate(childRenderer.bounds);
            }
            return bounds;
        }
    }
}