using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Size & Speed")]
    public float Size = 9f;
    public float Speed = 1f;

    [Header("Background")]
    public Transform[] Backgrounds;



    // Update is called once per frame
    void Update()
    {
        var delta = Time.deltaTime;

        foreach (var background in Backgrounds)
        {
            background.position += new Vector3(-Speed, 0) * delta;

            if (background.localPosition.x <= -Size)
            {
                background.localPosition = new Vector3(Size * (Backgrounds.Length - 1), background.localPosition.y);
            }
        }
    }
}
