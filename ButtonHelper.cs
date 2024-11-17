using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    private bool stop = false;
    public Shower shower;

    // Start is called before the first frame update
    void Start()
    {
        shower = gameObject.AddComponent<Shower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        if (!stop)
        {
            shower.Execute();
            stop = true;
        }
    }
}
