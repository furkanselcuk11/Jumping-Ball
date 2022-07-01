using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Circle : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] private float _colorChangeSecond=5f;
    [SerializeField] private Material[] _colorMaterials;
    void Start()
    {
        StartCoroutine(nameof(ColorChange));
    }
    void Update()
    {
        transform.Translate(transform.forward * (Time.deltaTime * speed));
    }

    private IEnumerator ColorChange()
    {
        while (true)
        {
            var randomColor = Random.Range(0, _colorMaterials.Length);
            this.gameObject.GetComponent<MeshRenderer>().material = _colorMaterials[randomColor];
            yield return new WaitForSeconds(_colorChangeSecond); // Fonk çalýþma süresi
        }        
    }
}
