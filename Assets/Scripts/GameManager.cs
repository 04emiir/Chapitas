using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle attackMode;
    public Toggle moveMode;
    // Start is called before the first frame update
    void Start()
    {
        moveMode.isOn = true;
        attackMode.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
