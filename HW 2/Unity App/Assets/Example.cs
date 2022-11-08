using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

public class Example : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefab2;
    public string[] pointsOne;
    public string[] pointsTwo;
    public float pointsOneSize;
    public float pointsTwoSize;
    public string[] points_;
    public string[] points__;
    [SerializeField] TextAsset file1;
    [SerializeField] TextAsset file2;
    void Start()
    {
        pointsOne = Regex.Split(file1.text, "\r\n|\r|\n");
        pointsTwo = Regex.Split(file2.text, "\r\n|\r|\n");
        pointsOneSize = float.Parse((string)pointsOne.GetValue(0), CultureInfo.InvariantCulture.NumberFormat);
        pointsTwoSize = float.Parse((string)pointsTwo.GetValue(0), CultureInfo.InvariantCulture.NumberFormat);

        for (var i = 1; i < pointsOne.Length; i++)
        {
            prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            string point = (string)pointsOne.GetValue(i);
            points_ = point.Split(' ');
            float x = float.Parse((string)points_.GetValue(0), CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse((string)points_.GetValue(1), CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse((string)points_.GetValue(2), CultureInfo.InvariantCulture.NumberFormat);
            prefab.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 0);
            Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        }
        for (var i = 1; i < pointsTwo.Length; i++)
        {
            prefab2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            string point = (string)pointsTwo.GetValue(i);
            points__ = point.Split(' ');
            float x = float.Parse((string)points__.GetValue(0), CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse((string)points__.GetValue(1), CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse((string)points__.GetValue(2), CultureInfo.InvariantCulture.NumberFormat);
            prefab2.GetComponent<Renderer>().material.color = new Color32(255, 255, 0, 0);
            Instantiate(prefab2, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
