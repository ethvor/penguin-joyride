using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float scrollSpeed = 0.02f;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
