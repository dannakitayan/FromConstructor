using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InGameItems/ImportSprites", order = 3)]
public class SpriteSetting : ScriptableObject
{
    [Range(0, 10)]
    public int Endurance = 0;
    public bool IsSolid;
    [Header("Sound of breaking")]
    public AudioClip Sound;
    [Header("Animation")]
    public Texture2D[] Sprites = new Texture2D[5];
}
