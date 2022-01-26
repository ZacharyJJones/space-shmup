using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HoverSelectableGroup : MonoBehaviour
{
    // Editor Vars
    public float SelectableMoveDelay;
    public float SelectableMoveDuration;
    public HoverSelectable[] GroupSelectables;


    protected void InitializeSelectables(string[] keys, HoverSelectable.onSelected actionOnSelected)
    {
        for (int i = 0; i < GroupSelectables.Length; i++)
        {
            var current = GroupSelectables[i];
            current.OnSelected += actionOnSelected;
            current.Key = keys[i];
            _moveSelectableIntoView(current.gameObject, i*SelectableMoveDelay);

            for (int j = 0; j < GroupSelectables.Length; j++)
            {
                if (i != j)
                    current.OnSelected += GroupSelectables[j].Ignored;
            }
        }
    }

    private void _moveSelectableIntoView(GameObject selectable, float waitBeforeMove)
    {
        StartCoroutine(Utils.SimpleWait(waitBeforeMove,
            () =>
            {
                StartCoroutine(_moveSelectable(selectable));
            }
        ));
    }

    private IEnumerator _moveSelectable(GameObject toMove)
    {
        float t = 0f;
        Vector3 initialLocalPosition = toMove.transform.localPosition;
        Vector3 localDestination = new Vector3(0, initialLocalPosition.y);
        while (t < 1f)
        {
            var newPos = Vector3.Lerp(
                initialLocalPosition,
                localDestination,
                Transforms.SmoothStopX(t, 2)
            );
            t += Time.deltaTime / SelectableMoveDuration;
            toMove.transform.localPosition = newPos;
            yield return null;
        }
    }
}
