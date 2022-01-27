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
    public Text KeyDisplayText;
    public float TimeToSelect;
    public float WiggleArea;

    public Canvas DisplayCanvas;
    public Image ProgressImage;
    public Rigidbody2D Rigidbody;


    // Runtime Vars
    private bool _isHovered;
    private float _progress;

    public delegate void onSelected(HoverSelectable sender);
    public event onSelected OnSelected;


    private float _randomInWiggleArea => _progress * _progress * Random.Range(-WiggleArea, WiggleArea);


    private void Start()
    {
        KeyDisplayText.text = Key;
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
            DisplayCanvas.transform.localPosition = Vector3.zero;
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
        Destroy(this.gameObject);
    }
}
