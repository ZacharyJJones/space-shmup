using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    public GameObject target;
    public GameObject targetParent;

    private int t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        Utils.HeadingFromNormalizedVector((target.transform.position - transform.position).normalized);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t++;
        if (t > 5)
        {
            t = 0;
            targetParent.transform.Rotate(new Vector3(0, 0, 3));


            var a = Utils.HeadingFromNormalizedVector((target.transform.position - transform.position).normalized);
            // pointerParent.transform.rotation = Quaternion.Euler(0, 0, a);
        }
    }
}
