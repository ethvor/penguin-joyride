using UnityEngine;

// attached to hazard prefab roots so the spawner knows which difficulty tier this pattern is
public class HazardPattern : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty difficulty = Difficulty.Easy;
}
