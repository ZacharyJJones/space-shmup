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
    public string Key;

    public float TimeToSelect = 2f;
    public float WiggleArea = 0.2f;

    public Canvas DisplayCanvas;
    public Rigidbody2D Rigidbody;

    public Image ProgressImage;


    // Runtime Vars
    public delegate void onSelected(HoverSelectable sender);
    public event onSelected OnSelected;
    private bool _isHovered;
    private float _progress;

    private float _randomInWiggleArea => _progress * _progress * Random.Range(-WiggleArea, WiggleArea);


    private void Start()
    {
        _isHovered = false;
        _progress = 0f;
    }

    private void Update()
    {
        ProgressImage.fillAmount = _progress;
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        Rigidbody.MovePosition(transform.position);

        if (_isHovered)
        {
            _selecting();
            _progress += time / TimeToSelect;
            if (_progress >= 1.0f)
                _selected();
        }
        else if (_progress > 0f)
        {
            _progress = Mathf.Max(0f, _progress - 2 * time);
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
        // sender.OnSelected -= Ignored;
        Destroy(this.gameObject);
    }
}
