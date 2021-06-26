using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeshLine : Graphic
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        DoCreatPloygonMesh();
    }
    public void DoCreatPloygonMesh()
    {

        float x1 = 176, y1=156;
        float x2 = 232, y2=156;
        float x3 = 157, y3=181;
        float x4 = 221, y4=181;
        //新申请一个Mesh网格
        Mesh tMesh = new Mesh();
 
        //存储所有的顶点
        Vector3[] tVertices = new Vector3[3]{new Vector3(-0.2f ,0.1f,0f),new Vector3(0f,0.1f,0f),new Vector3(-0.2f,0f,0f)};
 
        //存储画所有三角形的点排序
        List<int> tTriangles = new List<int>();
 
        //根据所有顶点填充点排序
        for (int i = 0; i < tVertices.Length - 1; i++)
        {
            tTriangles.Add(i);
            tTriangles.Add(i + 1);
            tTriangles.Add(tVertices.Length - i - 1);
        }
 
        //赋值多边形顶点
        tMesh.vertices = tVertices;
 
        //赋值三角形点排序
        tMesh.triangles = tTriangles.ToArray();
 
        //重新设置UV，法线
        tMesh.RecalculateBounds();
        tMesh.RecalculateNormals();
    
 
        //将绘制好的Mesh赋值
        GetComponent<MeshFilter>().mesh = tMesh;
        GetComponent<MeshRenderer>().enabled = true;
    }

}
