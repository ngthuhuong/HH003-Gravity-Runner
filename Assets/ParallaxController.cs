using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform cam; // Main Camera (drag vào Inspector)
    private Vector3 camStartPos;
    private float distance;

    [SerializeField] private GameObject[] backgrounds; // drag background layers vào đây
    private Material[] mats;
    private float[] backSpeed; // chỉnh tốc độ từng layer

    [Range(0.01f, 0.1f)]
    public float parallaxSpeed = 0.05f;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = backgrounds.Length;
        mats = new Material[backCount];
        backSpeed = new float[backCount]; 

        for (int i = 0; i < backCount; i++)
        {
            mats[i] = backgrounds[i].GetComponent<Renderer>().material;
            backSpeed[i] = 0.03f + (i * 0.02f); //tốc độ của layer sau cùng là 0.03
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mats[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}