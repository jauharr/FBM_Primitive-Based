using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder2.MeshOperations;
using ProBuilder2.Common;
using ProBuilder2.Examples;
namespace ProBuilder2.Examples
{
    public class RectScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void makeRectangel()
        {
            pb_Object cube = pb_ShapeGenerator.CubeGenerator(Vector3.one);

            string json = JsonUtility.ToJson(cube);
        }
    }
}
