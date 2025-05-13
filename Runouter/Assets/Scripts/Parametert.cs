using UnityEngine;

public class Parametert : MonoBehaviour
{
    private Material material;
    [SerializeField]
    private float parameterFactor = 0.01f;
    private float offset = 0f;
    //[SerializeField]
   // private float gamespeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ParameterScroll();
    }
    void ParameterScroll()
    {
        float speed = GameManager.instance.GetGameSpeed()  * parameterFactor;
        offset += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
