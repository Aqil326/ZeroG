using UnityEngine;

public class MaterialOffsetController : MonoBehaviour
{
    public float xOffsetSpeed = 0.01f;
    public float yOffsetSpeed = 0.02f;

    private Renderer renderer;
    private Material material;

    void Start()
    {
        // Assuming your object has a Renderer component with a material
        renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    void Update()
    {
        // Update material offsets slowly
        float xOffset = material.mainTextureOffset.x + xOffsetSpeed * Time.deltaTime;
        float yOffset = material.mainTextureOffset.y + yOffsetSpeed * Time.deltaTime;

        // Apply the offsets to the material
        material.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}