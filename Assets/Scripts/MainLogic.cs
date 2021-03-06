﻿//此脚本attach在player这个物体上

using UnityEngine;
using System.Collections;
using VacuumShaders.CurvedWorld;    //shader插件，用来扭曲隧道
using DG.Tweening;                  //Dotween插件

public class MainLogic : MonoBehaviour {
    public GameObject elementPrefab;    //一个隧道预设
    public Material polygonMat;         //10边形的材质
    public Material highlightMat;       //轨道高亮的材质
    public Material originalMat;        //轨道原本的材质
    int currentHighlight = 0;           //当前高亮的轨道索引

    float interval;                     //每间隔多少秒克隆一个隧道

    float timer = 0;                    //克隆隧道计时器

    float dirTimer = 0;                 //改变隧道扭曲方向的计时器

    public float dirInterval = 5;       //每隔5秒改变一次隧道方向

    public float currentSpeed = 30;     //当前隧道移动速度

    public float currentOffset = 25;    //当前隧道的间隔

    GameObject current;                 //当前隧道

    public CurvedWorld_GlobalController curveCtr;   //扭曲shader总控

    public GameObject pivot;            //中心点，飞船是围绕此中心点旋转

    GameObject player;                  //玩家飞船

    public float playerSpeed;           //飞船的旋转速度

    public GameObject disappearFx;      //光晕消失后的特效


	// Use this for initialization
	void Start () {
        interval = currentOffset / currentSpeed;    //克隆隧道的间隔时间等于隧道之间的间隔除以隧道的移动速度

        player = GameObject.FindGameObjectWithTag("Player");    //找到玩家飞船的GameObject
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;        //克隆隧道计时器 + 距上次Update函数到此次Update函数所流逝的时间
        dirTimer += Time.deltaTime;     //改变隧道扭曲方向的计时器 + 同时

        if (timer > interval)   //当克隆隧道计时器大于克隆隧道的间隔时间，则克隆一个隧道
        {
            current = Instantiate(elementPrefab) as GameObject; //克隆体保存在current此变量里
            current.transform.position = new Vector3(53, -8.1f, 275);//将新隧道放在 （53，-8.1f,275)这个位置
            timer = 0;      //计时器复位
        }

        if (dirTimer > dirInterval) //同上，改变扭曲方向
        {
            dirTimer = 0;
            ChangeDir(dirInterval-0.5f, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-100, 100)));//随机在3个轴上进行扭曲 x:-10-10,y:-10-10,z:-100-100
            ChangeHighlight(Random.Range(0, 10));   //同时随机改变轨道的高亮
        }
        
	}

    void LateUpdate()
    {
        if (current)
        {
            current.GetComponent<MeshRenderer>().material = polygonMat; //current的材质必须在LateUpdate里更新，此乃Porbuilder插件的BUG

            current.transform.FindChild(currentHighlight.ToString()).GetComponent<MeshRenderer>().material = highlightMat;  //新隧道要找到currentHighlight索引的轨道，使之高亮
        }
    }

    public void OnJoyStickMove(Vector2 stickPos)
    {
        player.transform.RotateAround(pivot.transform.position, Vector3.forward, Time.deltaTime * playerSpeed * stickPos.x);//当移动虚拟摇杆，则玩家飞船绕着中心点旋转
    }

    void ChangeDir(float sec, Vector3 dir)
    {
        DOTween.Rewind();//dotween里所有的缓动都停止
        DOTween.To(() => curveCtr._V_CW_X_Bend_Size_GLOBAL, x => curveCtr._V_CW_X_Bend_Size_GLOBAL = x, dir.x, sec);//隧道在x轴上的扭曲
        DOTween.To(() => curveCtr._V_CW_Y_Bend_Size_GLOBAL, x => curveCtr._V_CW_Y_Bend_Size_GLOBAL = x, dir.y, sec);//y
        DOTween.To(() => curveCtr._V_CW_Z_Bend_Size_GLOBAL, x => curveCtr._V_CW_Z_Bend_Size_GLOBAL = x, dir.z, sec);//z
    }

    //idx取值范围为0-9，对应隧道上的10条轨道
    void ChangeHighlight(int idx)
    {
        //隧道的tag是Element,此语句是找到当前时间里的所有隧道
        GameObject[] elements = GameObject.FindGameObjectsWithTag("Element");

        foreach (GameObject e in elements)
        {
            //把当前的高亮轨道复原
            e.transform.FindChild(currentHighlight.ToString()).GetComponent<MeshRenderer>().material = originalMat;
        }

        //高亮轨道的索引变成idx
        currentHighlight = idx;

        foreach (GameObject e in elements)
        {
            //保存currentHighlight对应的轨道的材质
            originalMat = e.transform.FindChild(currentHighlight.ToString()).GetComponent<MeshRenderer>().material;
            //高亮currentHighlight对应的隧道
            e.transform.FindChild(currentHighlight.ToString()).GetComponent<MeshRenderer>().material = highlightMat;
        }

            
    }

    //用来保存收集物，即光晕
    GameObject currentCollection;

    void OnTriggerEnter(Collider other)
    {
        if (other.collider.gameObject.tag != "Collection") return;//当触发器进入的物体不是光晕则返回
        currentCollection = other.gameObject;   //保存当前光晕
    }

    void OnTriggerExit(Collider other)
    {
        if (other.collider.gameObject.tag != "Collection") return;   //同上
        currentCollection = null;               //当前光晕设为空
    }

    public void OnPress()
    {
        if (currentCollection == null) return;
        Destroy(currentCollection); //销毁光晕
        GameObject go = Instantiate(disappearFx) as GameObject;// 生成一个光晕销毁后的粒子特效
        go.transform.parent = this.transform;    //移动特效到飞机上
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = new Vector3(-90, 0, 0);//调整特效角度
        Destroy(go, 1);//1秒钟后销毁此特效
    }
}
