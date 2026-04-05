using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speedMultiplier; //lower = farther away, scrolls slower

    private Material mat;         //this object's material, grabbed in Start
    private Vector2 startOffset;  //saves starting offset so we can reset it

    void Start()
    {
        //mainTextureOffset slides the texture sideways without moving game object
        //texture wrap mode on repeat in the Inspector so it loops
        mat = GetComponent<Renderer>().material; //Renderer is the component that draws this object
        startOffset = mat.mainTextureOffset;   //get mat start offset so we can reset to it after death

        GameManager.Instance.onReset.AddListener(ResetLayer); //snap back to start on reset
    }

    // Update is called once per frame
    void Update()
    {
        //shift texture left based on game scroll speed, scaled by this layer's multiplier
        float offset = GameManager.Instance.scrollSpeed * speedMultiplier * Time.deltaTime;
        mat.mainTextureOffset += new Vector2(offset, 0); //only moves horizontally
    }

    void ResetLayer()
    {
        mat.mainTextureOffset = startOffset; //undo all scrolling
    }
}
