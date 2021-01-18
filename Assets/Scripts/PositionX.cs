using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionX : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    public Text posXText;

    // Update is called once per frame
    void Update()
    {
      posXText.text = playerObject.transform.position.x.ToString();
    }
}
