using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder2.MeshOperations;
using ProBuilder2.Common;
using ProBuilder2.Examples;
using UnityEditor;
using System.Linq;
using Parabox.CSG;
using UnityEngine.UI;
namespace ProBuilder2
{

    public class probuilder : MonoBehaviour
    {
        /*
        public class BuildAssetsExample : MonoBehaviour
        {
            [MenuItem("Example/Build Asset Bundles")]
            static void BuildProb()
            {
                BuildPipeline.BuildAssetBundles("Assets/ProCore", BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
            }
        }
        */
        GameObject left, right, composite, one, two; /// <summary>
        bool wireframe = false;
        public Material wireframeMaterial = null;
        int index = 0;
        /// CSG Class Working
        /// </summary>
        public InputField x_value, y_value, z_value, x_cord, y_cord, z_cord, x_rot, y_rot,z_rot,w_rot;
        public static string shifter,updater,up02, CSGupdate;
        public Button rec;
        public Text feature, FeaturetoUpdate, CSG1, CSG2;
        public List<string> featureList;
        public static int number,j;
        public static Vector3 SquareValues;
        public static double d1, d2, d3;
        public void Rectangle()

        {
            shifter = "Rectangle";
            //pb_Object cube = pb_ShapeGenerator.CubeGenerator(Vector3.one);


        }
        public void Prism()
        {
            shifter = "Prism";
            //pb_Object Prism = pb_ShapeGenerator.PrismGenerator(Vector3.one);

        }
        public void Cone()
        {
            shifter = "Cone";
        }
        public void Cylinder()
        {
            shifter = "Cylinder";
            //pb_Object tempshape = pb_ShapeGenerator.CylinderGenerator(16, 0.5f, 2.0f, 16);
        }
        public void Torus()
        {
            pb_Object tempshape = pb_ShapeGenerator.TorusGenerator(12, 24, 5f, 2f, false, 360, 360);
        }
        public void SubtractOp()
        {
            GameObject sourceObject = GameObject.Find("cube");
            GameObject stampObject = GameObject.Find("Cylinder");
            GameObject output = ExtractIntersection(sourceObject, stampObject);

        }
        public void UnityCylinder()
        {
            GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        }
        public void Capsule()
        {
            shifter = "Capsule";
        }
        public void Sphere()
        {
            shifter = "Sphere";

        }
        public void UCube()
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        // Use this for initialization
        void Start()
        {
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            number = 0;
            /*
            if (GameObject.Find("Cube"))
            {
                Debug.Log("Object Found");
            }*/
            feature.text = "Features List";
            //featureList.Add(feature.text);
            j = 1;
            /*
            if (feature.text == "Features List")
            {
                string x1 = "\nValue";
                feature.text =feature.text + x1;
            }*/
            /*
            GameObject sourceObject = GameObject.Find("Cube");
            GameObject stampObject = GameObject.Find("Cylinder");
            Mesh m = CSG.Subtract(sourceObject, stampObject);
            Material material = sourceObject.GetComponent<Renderer>().material;
            GameObject composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = m;
            composite.AddComponent<MeshRenderer>().sharedMaterial = material;
            */
            //pb_Object tempshape = pb_ShapeGenerator.TorusGenerator(12, 24, 5f,2f, false,360, 360);
            //tempshape.ToMesh();
            //pb_Object Prism = pb_ShapeGenerator.PrismGenerator(Vector3.one);
            // GameObject go = new GameObject();
            // pb_Object pb = go.AddComponent<pb_Object>();
            // JsonUtility.FromJsonOverwrite(json, pb);
            // pb.ToMesh();
            // pb.Refresh();
        }

        // Update is called once per frame
        void Update()
        {

            if (shifter == "Rectangle")
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = OnSubmit();
                cube.transform.position = Cords();
                cube.transform.rotation = Rotors();
                string x1 = "Rectangle-" + number.ToString();
                feature.text = feature.text + "\n"+x1;
                cube.name =  x1;
                featureList.Add(x1);
                number = number + 1;
                shifter = null;
            }
            if (shifter == "Cylinder")
            {
                GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                Cylinder.transform.localScale = OnSubmit();
                Cylinder.transform.position = Cords();
                Cylinder.transform.rotation = Rotors();
               
                string x1 = "Cylinder-" + number.ToString();
                Cylinder.name = x1;
                feature.text = feature.text + "\n" + x1;
                number = number + 1;
                featureList.Add(x1);
                shifter = null;
            }
            if (shifter == "Sphere")
            {
                GameObject Sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Sph.transform.localScale = OnSubmit();
                Sph.transform.position = Cords();
                Sph.transform.rotation = Rotors();
                string x1 = "Sphere-" + number.ToString();
                feature.text = feature.text + "\n" + x1;
                number = number + 1;
                Sph.name = x1;
                featureList.Add(x1);
                shifter = null;
            }
            if (shifter == "Capsule")
            {
                GameObject Sph = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                Sph.transform.localScale = OnSubmit();
                Sph.transform.position = Cords();
                Sph.transform.rotation = Rotors();
                string x1 = "Capsule-" + number.ToString();
                feature.text = feature.text + "\n" + x1;
                featureList.Add(x1);
                Sph.name = x1;
                number = number + 1;
                shifter = null;
            }
            if (shifter == "Prism")
            {
                pb_Object Prism = pb_ShapeGenerator.PrismGenerator(OnSubmit());
                Prism.transform.position = Cords();
                Prism.transform.rotation = Rotors();
                string x1 = "Prism-" + number.ToString();
                feature.text = feature.text + "\n" + x1;
                featureList.Add(x1);
                Prism.name = x1;
                number = number + 1;
                shifter = null;
            }
            if (shifter == "Cone")
            {
                pb_Object Prism = pb_ShapeGenerator.ConeGenerator(OnSubmit().x, OnSubmit().y, 32);
                Prism.transform.position = Cords();
                Prism.transform.rotation = Rotors();
                string x1 = "Cone-" + number.ToString();
                featureList.Add(x1);
                feature.text = feature.text + "\n" + x1;
                number = number + 1;
                Prism.name = x1;
                shifter = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse is down");
                RaycastHit hitinfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitinfo);
                if (hit)
                {
                    GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                    Debug.Log("Hit" + hitinfo.transform.gameObject.name);
                    for (int i=0; i<featureList.Count;i++)
                    {
                        if (hitinfo.transform.gameObject.name.Contains(featureList[i]))
                        {
                            //hitinfo.transform.gameObject.transform.localPosition;
                            Vector3 y = hitinfo.transform.gameObject.transform.localPosition;
                            Vector3 x = hitinfo.transform.gameObject.transform.localScale;
                            Quaternion z = hitinfo.transform.gameObject.transform.rotation;
                            toview(x, y,z);
                            GameObject tempObject = hitinfo.transform.gameObject;
                            
                            
                            FeaturetoUpdate.text = hitinfo.transform.gameObject.name;

                            if (j == 1)
                            {
                                CSG1.text = hitinfo.transform.gameObject.name;
                                j = -1;
                            }
                            else if (j == -1)
                            {
                                CSG2.text = hitinfo.transform.gameObject.name;
                                j = 1;
                            }
                            //udater(tempObject);
                            updater = hitinfo.transform.gameObject.name;
                            //x_cord.text = x;
                            Debug.Log("It's working");
                        }
                        else { Debug.Log("Nopz"); }

                    }
                }
                else { Debug.Log("No hit"); }
                Debug.Log("Mouse is down");
               
            }
            if (up02 == "Update")
            {
                if (GameObject.Find(updater))
                {
                    GameObject toedit = GameObject.Find(updater);
                    toedit.transform.localScale = OnSubmit();
                    toedit.transform.localPosition = Cords();
                    up02 = null;
                    updater = null;
                }
                else
                {
                    
                }

            }
            if (CSGupdate == "CSG Update")
            {
                Debug.Log("CSG is working");
                //SubtractionLR();
                CSGupdate = null;
            }
            /*
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (Object go in allObjects)
            {
                print(go.name.ToString() + "is an object");
            }*/
        }
        public double X_Value(double stg)
        {
            d1 = stg;
            return d1;
        }
        public double Y_Value(double stg)
        {
            d2 = stg;
            return d2;
        }
        public double Z_Value(double stg)
        {
            d3 = stg;
            return d3;
        }
        public Vector3 OnSubmit()
        {
            Vector3 temvec = SquareValues;

            temvec.x = float.Parse(x_value.text, System.Globalization.CultureInfo.InvariantCulture);
            temvec.y = float.Parse(y_value.text, System.Globalization.CultureInfo.InvariantCulture);
            temvec.z = float.Parse(z_value.text, System.Globalization.CultureInfo.InvariantCulture);
            return temvec;
        }
        public Vector3 Cords()
        {
            Vector3 vector3;
            vector3.x = float.Parse(x_cord.text, System.Globalization.CultureInfo.InvariantCulture);
            vector3.y = float.Parse(y_cord.text, System.Globalization.CultureInfo.InvariantCulture);
            vector3.z = float.Parse(z_cord.text, System.Globalization.CultureInfo.InvariantCulture);
            return vector3;
        }
        public Quaternion Rotors()
        {
            Quaternion vector3;
            vector3.x = float.Parse(x_rot.text, System.Globalization.CultureInfo.InvariantCulture);
            vector3.y = float.Parse(y_rot.text, System.Globalization.CultureInfo.InvariantCulture);
            vector3.z = float.Parse(z_rot.text, System.Globalization.CultureInfo.InvariantCulture);
            vector3.w = float.Parse(w_rot.text, System.Globalization.CultureInfo.InvariantCulture);
            return vector3;
        }
        public static GameObject ExtractIntersection(GameObject source, GameObject stamp)
        {

            Material material = source.GetComponent<Renderer>().material;
            Mesh m = CSG.Subtract(source, stamp);
            GameObject composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = m;
            composite.AddComponent<MeshRenderer>().sharedMaterial = material;

            var sourcePb = source.GetComponent<pb_Object>();
            var sourceMeshFilter = source.GetComponent<MeshFilter>();
            var originalCenter = sourceMeshFilter.sharedMesh.bounds.center;

            // Create a mesh comprising only parts of the original mesh that overlap with the stamp.
            var intersectionMesh = CSG.Intersect(source, stamp);
            // Create a game object and pb_object from mesh
            pb_Object newobject;
            //var intersectionPb = 
            //(intersectionMesh);
            /*
            Mesh newmesh = intersectionMesh;
            var interObject = ProBuilder2.MeshOperations.
            pb_Object pbMesh = intersectionMesh.GetTopology;


            // Cut out the intersection from the original, so that the original object remains the same visually but is now split into two objects.
            var newMesh = CSG.Subtract(source, intersectionPb.gameObject);

            sourceMeshFilter.sharedMesh = newMesh;

            //// Update the pb_object to reflect changes to the underlying mesh.
            ///
            ProBuilder2.MeshOperations.pbMeshOps.ResetPbObjectWithMeshFilter(sourcePb, true);
            //ResetPbObjectWithMeshFilter(sourcePb, true);

            // The modified mesh coming out of CSG probably needs to be repositioned relative to previous pivot.
            var newCenter = sourceMeshFilter.sharedMesh.bounds.center;
            var offset = originalCenter - newCenter;
            var indices = sourceMeshFilter.sharedMesh.GetIndices(0);

            sourcePb.TranslateVertices(indices, offset);
            ProBuilder2.MeshOperations.pbMeshOps.CenterPivot(sourcePb, indices);
            //CenterPivot(sourcePb, indices);

            sourcePb.ToMesh();
            sourcePb.Refresh();

            //pb_Smoothing.ApplySmoothingGroups(sourcePb, sourcePb.faces, 1, pb_Vertex.GetVertices(newMesh).Select(x => x.normal).ToArray());
            //CollapseCoincidentVertices(sourcePb, sourcePb.faces);
            */
            return composite;
        }
        private void toview(Vector3 scale, Vector3 orientation, Quaternion quat)
        {
            x_value.text = scale.x.ToString();
            y_value.text = scale.y.ToString();
            z_value.text = scale.z.ToString();
            x_cord.text = orientation.x.ToString();
            y_cord.text = orientation.y.ToString();
            z_cord.text = orientation.z.ToString();
            x_rot.text = quat.x.ToString();
            y_rot.text = quat.y.ToString();
            z_rot.text = quat.z.ToString();
            w_rot.text = quat.w.ToString();
        }
        public void udater()
        {
            up02 = "Update";
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            Debug.Log("level 1");
            for (int i =0; i<featureList.Count;i++)
            {
                if (FeaturetoUpdate.text.Contains("Cone"))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    
                    toedit.transform.localScale = OnSubmit();
                    toedit.transform.localPosition = Cords();
                    toedit.transform.localRotation = Rotors();
                    up02 = null;
                    updater = null;
                    break;
                }
                else if (FeaturetoUpdate.text.Contains("Prism"))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    toedit.transform.localScale = OnSubmit();
                    toedit.transform.localPosition = Cords();
                    up02 = null;
                    updater = null;
                    break;
                }
                else if (GameObject.Find(FeaturetoUpdate.text))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    toedit.transform.localScale = OnSubmit();
                    toedit.transform.localPosition = Cords();
                    toedit.transform.localRotation = Rotors();
                    up02 = null;
                    updater = null;
                    break;
                }
                else
                {
                    Debug.Log("level 3");
                }
            }
            

        }

        public void destroyObj()

        {
            for (int i = 0; i < featureList.Count; i++)
            {
                if (FeaturetoUpdate.text.Contains("Cone"))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    
                    Destroy(toedit);

                    feature.text = null;
                    feature.text = "Features List";
                    for (int j = 0; j < featureList.Count; j++)
                    {
                        if (featureList[j].Contains(updater))
                        {
                            continue;
                        }
                        else
                        {
                            feature.text = feature.text + "\n" + featureList[j];
                        }
                    }
                    featureList.Remove(updater);
                    up02 = null;
                    updater = null;
                    break;
                }
                else if (FeaturetoUpdate.text.Contains("Prism"))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    Destroy(toedit);
                    feature.text = null;
                    feature.text = "Features List";
                    for (int j = 0; j < featureList.Count; j++)
                    {
                        if (featureList[j].Contains(updater))
                        {
                            continue;
                        }
                        else
                        {
                            feature.text = feature.text + "\n" + featureList[j];
                        }
                    }
                    featureList.Remove(updater);
                    up02 = null;
                    updater = null;
                    break;
                }
                else if (GameObject.Find(FeaturetoUpdate.text))
                {
                    Debug.Log("level 2");
                    GameObject toedit = GameObject.Find(updater);
                    Destroy(toedit);
                    feature.text = null;
                    feature.text = "Features List";
                    for (int j = 0; j < featureList.Count; j++)
                    {
                        if (featureList[j].Contains(updater))
                        {
                            continue;
                        }
                        else
                        {
                            feature.text = feature.text + "\n" + featureList[j];
                        }
                    }
                    featureList.Remove(updater);
                    up02 = null;
                    updater = null;
                    break;
                }
                else
                {
                    Debug.Log("level 3");
                }
            }
        }

        public void CSGWorker()
        {

            string[] csgstring = new string[2];
            csgstring[0] = CSG1.text;
            csgstring[1] = CSG2.text;
            int interupt = 0;
            if ((CSG1.text == CSG2.text) || (CSG1.text == "CSG-1") || (CSG2.text == "CSG-2"))
            {
                interupt = 1;
                return;
            }


            for (int l = 0; l < 2; l++)
            {
                GameObject toedit = GameObject.Find(csgstring[l]);
                if (l == 0)
                {
                    one = toedit;
                }
                else
                {
                    two = toedit;
                }
            }
            //one = GameObject.Find(CSG1.text); //CreatePrimitive(PrimitiveType.Cube);
            //two = GameObject.Find(CSG2.text);// CreatePrimitive(PrimitiveType.Capsule);


            Reset();

            wireframeMaterial.SetFloat("_Opacity", 0);
            cur_alpha = 0f;
            dest_alpha = 0f;

            ToggleWireframe();

            CSGupdate = "CSG Update";
        }
        public void Reset()
        {
            if (composite) GameObject.Destroy(composite);
            //if(left) GameObject.Destroy(left);
            //if(right) GameObject.Destroy(right);

            //GameObject go = GameObject.Instantiate(fodder[index]);
            //GameObject.CreatePrimitive(PrimitiveType.Cube);
            //GameObject.CreatePrimitive(PrimitiveType.Capsule);
            //eft = GameObject.CreatePrimitive(PrimitiveType.Cube);//GameObject.Instantiate( go.//transform.GetChild(2).gameObject );
            //right = GameObject.CreatePrimitive(PrimitiveType.Capsule);//GameObject.Instantiate( go.transform.GetChild(3).gameObject );
            left = one;
            right = two;
            //GameObject.Destroy(go);

            wireframeMaterial = left.GetComponent<MeshRenderer>().sharedMaterial;

            GenerateBarycentric(left);
            GenerateBarycentric(right);
            //added



            //Destroy(union);
        }
        enum BoolOp
        {
            Union,
            SubtractLR,
            SubtractRL,
            Intersect
        };
        ///CSG Related
        ///
        public void Union()
        {
            Reset();
            Boolean(BoolOp.Union);
            //CSGupdate = "CSG Update";
        }

        public void SubtractionLR()
        {
            Reset();
            Boolean(BoolOp.SubtractLR);
        }

        public void SubtractionRL()
        {
            Reset();
            Boolean(BoolOp.SubtractRL);
        }

        public void Intersection()
        {
            Reset();
            Boolean(BoolOp.Intersect);
        }

        void Boolean(BoolOp operation)
        {
            Mesh m;

            /**
             * All boolean operations accept two gameobjects and return a new mesh.
             * Order matters - left, right vs. right, left will yield different
             * results in some cases.
             */
            switch (operation)
            {
                case BoolOp.Union:
                    m = CSG.Union(left, right);
                    break;

                case BoolOp.SubtractLR:
                    m = CSG.Subtract(left, right);
                    break;

                case BoolOp.SubtractRL:
                    m = CSG.Subtract(right, left);
                    break;

                case BoolOp.Intersect:
                default:
                    m = CSG.Intersect(right, left);
                    break;
            }

            composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = m;
            composite.AddComponent<MeshRenderer>().sharedMaterial = left.GetComponent<MeshRenderer>().sharedMaterial;
            composite.name = "Cut " + CSG1.text + " " + CSG2.text;
            int count = 0;
            //feature.text = "Features tree";
            for (int j = 0; j < featureList.Count; j++)
            {

                if ((featureList[j] == (CSG1.text)) || (featureList[j] == (CSG2.text)))
                {
                    continue;
                }
                else if (featureList[j].Contains(composite.name))
                {
                    count = 1;
                    continue;
                }
                else
                {
                    count = 1;
                    featureList.Add(composite.name);
                    feature.text = feature.text + "\n" + composite.name;
                }
            }
            if (count == 0)
            {
                featureList.Add(composite.name);
                feature.text = feature.text + "\n" + composite.name;
            }
            GenerateBarycentric(composite);

            //GameObject.Destroy(left);
            left.SetActive(false);
            right.SetActive(false);
            //GameObject.Destroy(right);
        }

        /**
         * Turn the wireframe overlay on or off.
         */
        public void ToggleWireframe()
        {
            wireframe = !wireframe;

            cur_alpha = wireframe ? 0f : 1f;
            dest_alpha = wireframe ? 1f : 0f;
            start_time = Time.time;
        }

        /**
         * Swap the current example meshes
         */
        /*
        public void ToggleExampleMeshes()
        {
            index++;
            if (index > fodder.Length - 1) index = 0;

            Reset();
        }
        */
        float wireframe_alpha = 0f, cur_alpha = 0f, dest_alpha = 1f, start_time = 0f;

        void GenerateBarycentric(GameObject go)
        {
            Mesh m = go.GetComponent<MeshFilter>().sharedMesh;

            if (m == null) return;

            int[] tris = m.triangles;
            int triangleCount = tris.Length;

            Vector3[] mesh_vertices = m.vertices;
            Vector3[] mesh_normals = m.normals;
            Vector2[] mesh_uv = m.uv;

            Vector3[] vertices = new Vector3[triangleCount];
            Vector3[] normals = new Vector3[triangleCount];
            Vector2[] uv = new Vector2[triangleCount];
            Color[] colors = new Color[triangleCount];

            for (int i = 0; i < triangleCount; i++)
            {
                vertices[i] = mesh_vertices[tris[i]];
                normals[i] = mesh_normals[tris[i]];
                uv[i] = mesh_uv[tris[i]];

                colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);

                tris[i] = i;
            }

            Mesh wireframeMesh = new Mesh();

            wireframeMesh.Clear();
            wireframeMesh.vertices = vertices;
            wireframeMesh.triangles = tris;
            wireframeMesh.normals = normals;
            wireframeMesh.colors = colors;
            wireframeMesh.uv = uv;

            go.GetComponent<MeshFilter>().sharedMesh = wireframeMesh;
        }
    }

 }

