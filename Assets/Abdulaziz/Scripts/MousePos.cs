using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePos : MonoBehaviour
{

    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        image.rectTransform.position = Input.mousePosition;
    }
}
