using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speedMultiplier = 0.5f; // lower = farther away, scrolls slower

    private Material mat;
    private Vector2 startOffset;

    void Start()
    {
        // material.mainTextureOffset slides the texture sideways without moving the object
        // the texture needs Wrap Mode set to Repeat in the Inspector for this to loop seamlessly
        mat = GetComponent<Renderer>().material;
        startOffset = mat.mainTextureOffset;

        GameManager.Instance.onReset.AddListener(ResetLayer);
    }

    void Update()
    {
        float offset = GameManager.Instance.scrollSpeed * speedMultiplier * Time.deltaTime;
        mat.mainTextureOffset += new Vector2(offset, 0);
    }

    void ResetLayer()
    {
        mat.mainTextureOffset = startOffset;
    }
}
