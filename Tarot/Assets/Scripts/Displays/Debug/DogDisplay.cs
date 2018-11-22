using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DogDisplay : MonoBehaviour {

    public Dog dog;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Dog:");
        sb.AppendLine(dog.ToString());
        text.text = sb.ToString();
    }
}
