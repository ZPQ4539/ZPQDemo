using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RectRender : MonoBehaviour
{
    /// <summary>
    /// 这是一个自定义类，用来表示一个生效的选定框；创建它时会自动算出包含四角坐标的四项数据
    /// </summary>
    /// 
    List<GameObject> games = new List<GameObject>();
    public class Selector
    {
        public float Xmin;
        public float Xmax;
        public float Ymin;
        public float Ymax;

        //构造函数，在创建选定框时自动计算Xmin/Xmax/Ymin/Ymax
        public Selector(Vector3 start, Vector3 end)
        {
            Xmin = Mathf.Min(start.x, end.x);
            Xmax = Mathf.Max(start.x, end.x);
            Ymin = Mathf.Min(start.y, end.y);
            Ymax = Mathf.Max(start.y, end.y);
        }
    }

    private bool onDrawingRect;//是否正在画框(即鼠标左键处于按住的状态)

    private Vector3 startPoint;//框的起始点，即按下鼠标左键时指针的位置
    private Vector3 currentPoint;//在拖移过程中，玩家鼠标指针所在的实时位置
    private Vector3 endPoint;//框的终止点，即放开鼠标左键时指针的位置

    private Selector selector;

    void Update()
    {

        //玩家按下鼠标左键，此时进入画框状态，并确定框的起始点
        if (Input.GetMouseButtonDown(0))
        {
            onDrawingRect = true;
            startPoint = Input.mousePosition;
        }

        //在鼠标左键未放开时，实时记录鼠标指针的位置
        if (onDrawingRect)
        {
            currentPoint = Input.mousePosition;
        }

        //玩家放开鼠标左键，说明框画完，确定框的终止点，退出画框状态
        if (Input.GetMouseButtonUp(0))
        {
            if (onDrawingRect)
            {
                endPoint = Input.mousePosition;
                onDrawingRect = false;
                selector = new Selector(startPoint, endPoint);
                //执行框选事件
                CheckSelection(selector);
            }
        }
    }

    //框选事件
    //按照选定框的范围，捕获标签为tag的所有物体，并打印这些物体的名字
    void CheckSelection(Selector selector)
    {
        Dictionary<string, List<GameObject>> Units = PBDG_GameMain.Instance.AllBuding;

        games.Clear();

        foreach (var Unit in Units)
        {
            foreach (var item in Unit.Value)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(item.transform.position);
                if (screenPos.x > selector.Xmin && screenPos.x < selector.Xmax && screenPos.y > selector.Ymin && screenPos.y < selector.Ymax)
                {
                    games.Add(item);
                    Debug.LogFormat("已选中目标:{0}", item.name);
                }
            }
        }
        if (games.Count > 0)
        {
            PBDG_GameMain.Instance.AllChooseBuding.Clear();
            PBDG_GameMain.Instance.Choose(games);
        }
        else
        {
            PBDG_GameMain.Instance.OnClickMove();
        }

       
    }

    public Material GLRectMat;//绘图的材质，在Inspector中设置
    public Color GLRectColor;//矩形的内部颜色，在Inspector中设置
    public Color GLRectEdgeColor;//矩形的边框颜色，在Inspector中设置

    void OnPostRender()
    {
        if (onDrawingRect)
        {
            //准备工作:获取确定矩形框各角坐标所需的各个数值
            float Xmin = Mathf.Min(startPoint.x, currentPoint.x);
            float Xmax = Mathf.Max(startPoint.x, currentPoint.x);
            float Ymin = Mathf.Min(startPoint.y, currentPoint.y);
            float Ymax = Mathf.Max(startPoint.y, currentPoint.y);

            GL.PushMatrix();//GL入栈
                            //在这里，只需要知道GL.PushMatrix()和GL.PopMatrix()分别是画图的开始和结束信号,画图指令写在它们中间
            if (!GLRectMat)
                return;

            GLRectMat.SetPass(0);//启用线框材质rectMat

            GL.LoadPixelMatrix();//设置用屏幕坐标绘图

            /*------第一步，绘制矩形------*/
            GL.Begin(GL.QUADS);//开始绘制矩形,这一段画的是框中间的半透明部分，不包括边界线

            GL.Color(GLRectColor);//设置矩形的颜色，注意GLRectColor务必设置为半透明

            //陈述矩形的四个顶点
            GL.Vertex3(Xmin, Ymin, 0);//陈述第一个点，即框的左下角点，记为点1
            GL.Vertex3(Xmin, Ymax, 0);//陈述第二个点，即框的左上角点，记为点2
            GL.Vertex3(Xmax, Ymax, 0);//陈述第三个点，即框的右上角点，记为点3
            GL.Vertex3(Xmax, Ymin, 0);//陈述第四个点，即框的右下角点，记为点4

            GL.End();//告一段落，此时画好了一个无边框的矩形


            /*------第二步，绘制矩形的边框------*/
            GL.Begin(GL.LINES);//开始绘制线，用来描出矩形的边框

            GL.Color(GLRectEdgeColor);//设置方框的边框颜色，建议设置为不透明的

            //描第一条边
            GL.Vertex3(Xmin, Ymin, 0);//起始于点1
            GL.Vertex3(Xmin, Ymax, 0);//终止于点2

            //描第二条边
            GL.Vertex3(Xmin, Ymax, 0);//起始于点2
            GL.Vertex3(Xmax, Ymax, 0);//终止于点3

            //描第三条边
            GL.Vertex3(Xmax, Ymax, 0);//起始于点3
            GL.Vertex3(Xmax, Ymin, 0);//终止于点4

            //描第四条边
            GL.Vertex3(Xmax, Ymin, 0);//起始于点4
            GL.Vertex3(Xmin, Ymin, 0);//返回到点1

            GL.End();//画好啦！

            GL.PopMatrix();//GL出栈
        }
    }
}