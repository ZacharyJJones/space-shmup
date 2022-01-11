using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSelectableGroup : MonoBehaviour
{
    // Editor Vars
    public HoverSelectable[] GroupSelectables;


    private void Start()
    {
        for (int i = 0; i < GroupSelectables.Length; i++)
        {
            var current = GroupSelectables[i];
            for (int j = 0; j < GroupSelectables.Length; j++)
            {
                if (i == j)
                    continue;

                current.OnSelected += GroupSelectables[j].Ignored;
            }
        }

        Destroy(this);
    }
}
