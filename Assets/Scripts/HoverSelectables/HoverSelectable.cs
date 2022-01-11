using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class HoverSelectable : MonoBehaviour
{
    // Editor Vars
    public float TimeToSelect = 2f;
    public float WiggleArea = 0.2f;

    public Canvas DisplayCanvas;
    public Rigidbody2D Rigidbody;

    public Image ProgressImage;


    // Runtime Vars
    public delegate void onSelected(HoverSelectable sender);
    public event onSelected OnSelected;
    private bool _isHovered;
    private float _timeProgress;
    private float _lerpVal => _timeProgress / TimeToSelect;

    private float _randomInWiggleArea => _lerpVal * _lerpVal * Random.Range(-WiggleArea, WiggleArea);


    private void Start()
    {
        _isHovered = false;
        _timeProgress = 0f;
    }

    private void Update()
    {
        ProgressImage.fillAmount = _lerpVal;
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        Rigidbody.MovePosition(transform.position);

        if (_isHovered)
        {
            _selecting();
            _timeProgress += time;
            if (_lerpVal >= 1.0f)
                _selected();
        }
        else if (_timeProgress > 0f)
        {
            _timeProgress = Mathf.Clamp(_timeProgress - time * 2f, 0f, TimeToSelect);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isHovered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isHovered = false;
            DisplayCanvas.transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
    }

    private void _selecting()
    {
        DisplayCanvas.transform.localPosition = new Vector3(
            _randomInWiggleArea,
            _randomInWiggleArea,
            transform.localPosition.z
        );
    }

    private void _selected()
    {
        OnSelected?.Invoke(this);
        Destroy(gameObject);
    }

    public void Ignored(HoverSelectable sender)
    {
        sender.OnSelected -= Ignored;
        Destroy(gameObject);
    }
}
