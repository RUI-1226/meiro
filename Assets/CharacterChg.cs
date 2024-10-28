using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChg : MonoBehaviour
{

    private int index = 0;
    private int o_max = 0;
    GameObject[] ChildObject; 

    // Start is called before the first frame update
    void Start()
    {       
        o_max = this.transform.childCount;　　//子オブジェクトの偶数取得
       
        ChildObject = new GameObject[o_max];　//インスタンス作成

        
        for (int i = 0; i < o_max; i++)
        {
            ChildObject[i] = transform.GetChild(i).gameObject;　//すべての子オブジェクト取得
        }
        //すべての子オブジェクトを非アクティブ
        foreach (GameObject gamObj in ChildObject)
        {
            gamObj.SetActive(false);
        }
        //最初はひとつだけアクティブ化しておく
        ChildObject[index].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Q"))
        {
            //現在のアクティブな子オブジェクトを非アクティブ
            ChildObject[index].SetActive(false);
            index++;

            //子オブジェクトをすべて切り替えたらまた最初のオブジェクトに戻る
            if (index == o_max) { index = 0; }

            //次のオブジェクトをアクティブ化
            ChildObject[index].SetActive(true);
        }


    }
}
