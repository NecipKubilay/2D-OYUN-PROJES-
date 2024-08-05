using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaks : MonoBehaviour
{

    [SerializeField] Vector2 parallaxEffectMultiplier;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private Transform cam;


    private Vector3 lastCameraPosition;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        lastCameraPosition = cam.position;


        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.width / sprite.pixelsPerUnit;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = cam.position - lastCameraPosition;

        transform.position += new Vector3(movement.x * parallaxEffectMultiplier.x, movement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cam.position;

        //if (infiniteHorizontal)
        //{
        //    if (Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX)
        //    {
        //        float offsetPositionX = (cam.position.x - transform.position.x) % textureUnitSizeX;
        //        transform.position = new Vector3(cam.position.x + offsetPositionX, transform.position.y);
        //    }
        //}


        //if (infiniteVertical)
        //{
        //    if (Mathf.Abs(cam.position.y - transform.position.y) >= textureUnitSizeY)
        //    {
        //        float offsetPositionY = (cam.position.y - transform.position.y) % textureUnitSizeX;
        //        transform.position = new Vector3(cam.position.x, transform.position.y + offsetPositionY);
        //    }
        //}
    }
}
