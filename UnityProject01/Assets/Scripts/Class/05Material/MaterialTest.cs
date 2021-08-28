using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTest : MonoBehaviour
{
    private Renderer render;
    public Color oriColor;

    Material mat;
    Material[] mats;

    public Shader shaderVertexColor;
    public Shader shaderStandard;


    public bool bChange = false;
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        shaderVertexColor = Shader.Find("Mobile/Bumped Diffuse");
        if (!shaderVertexColor)
        {
            Debug.Log("shaderVertexColor not Found");
        }
        //shaderAlphaBlended = Shader.Find("Mobile/Particles/Alpha Blended");
        shaderStandard = Shader.Find("Standard");

        if (!shaderStandard)
        {
            Debug.Log("shaderAlphaBlended not Found");
        }
        if (bChange)
            meshRenderer.material.shader = shaderVertexColor;
        else
            meshRenderer.material.shader = shaderStandard;
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;
        mats = render.materials;
        oriColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); // mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Mat_1();
        Mat_2();
    }

    void Mat_1()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    void Mat_2()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            mat.SetColor("_Color", Color.red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mat.color = new Color(0, 1, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mat.SetColor("_Color", new Color(oriColor.r, oriColor.g, oriColor.b, 0.5f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) // Opaque
        {
            mat.SetFloat("_Mode", 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPAHTEST_ON");
            mat.DisableKeyword("_ALPAHBLEND_ON");
            mat.DisableKeyword("_ALPAHPREMULTIPLY_ON");
            mat.renderQueue = -1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) // Cutout
        {
            mat.SetFloat("_Mode", 1);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.EnableKeyword("_ALPAHTEST_ON");
            mat.DisableKeyword("_ALPAHBLEND_ON");
            mat.DisableKeyword("_ALPAHPREMULTIPLY_ON");
            mat.renderQueue = 2450;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) // Fade
        {
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPAHTEST_ON");
            mat.EnableKeyword("_ALPAHBLEND_ON");
            mat.DisableKeyword("_ALPAHPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) // Transparent
        {
            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPAHTEST_ON");
            mat.DisableKeyword("_ALPAHBLEND_ON");
            mat.EnableKeyword("_ALPAHPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) // texture change
        {
            Texture albedoTexture = Resources.Load("Textures/vol_2_3_Base_Color") as Texture;
            render.material.SetTexture("_MainTex", albedoTexture);

            Texture bumpTexture = Resources.Load("Textures/vol_2_3_Normal") as Texture;
            render.material.SetTexture("_BumpMap", bumpTexture);
        }

    }
}
