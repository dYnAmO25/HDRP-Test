using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceLight : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float fDistance;
    [SerializeField] float fSpeed;
    [SerializeField] Color[] colors;
    [SerializeField] float fIntervall;
    [SerializeField] bool random;
    [SerializeField] Light[] lights;

    private int iCurrentColor;
    private float fWastedTime;

    private Vector3 pos;

    void Start()
    {
        pos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fWastedTime >= fIntervall)
        {
            fWastedTime = 0;

            cam.transform.position = pos;

            if (random)
            {
                RandomLight();
            }
            else
            {
                ChangeLight();
            }
        }

        fWastedTime += Time.deltaTime;

        cam.transform.position = Vector3.Lerp(cam.transform.position, pos + new Vector3(0, 0, fDistance), fSpeed);
    }

    private void ChangeLight()
    {
        if (iCurrentColor < colors.Length - 1)
        {
            iCurrentColor++;
        }
        else
        {
            iCurrentColor = 0;
        }

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = colors[iCurrentColor];
        }
    }

    private void RandomLight()
    {
        int j;

        for (int i = 0; i < lights.Length; i++)
        {
            j = Random.Range(1, colors.Length) - 1;
            lights[i].color = colors[j];
        }
    }
}
