using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// シーンを実行しなくてもカメラワークが反映されるよう、ExecuteInEditModeを付与
[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public class Parameter
    {
        public Transform trackTarget;
        public Vector3 position;
        public Vector3 angles = new Vector3(10f, 0f, 0f);
        public float distance = 7f;
        public float fieldOfView = 45f;
        public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
        public Vector3 offsetAngles;

        public static Parameter Lerp(Parameter a, Parameter b, float t, Parameter ret)
        {
            ret.position = Vector3.Lerp(a.position, b.position, t);
            ret.angles = LerpAngles(a.angles, b.angles, t);
            ret.distance = Mathf.Lerp(a.distance, b.distance, t);
            ret.fieldOfView = Mathf.Lerp(a.fieldOfView, b.fieldOfView, t);
            ret.offsetPosition = Vector3.Lerp(a.offsetPosition, b.offsetPosition, t);
            ret.offsetAngles = LerpAngles(a.offsetAngles, b.offsetAngles, t);

            return ret;
        }

        private static Vector3 LerpAngles(Vector3 a, Vector3 b, float t)
        {
            Vector3 ret = Vector3.zero;
            ret.x = Mathf.LerpAngle(a.x, b.x, t);
            ret.y = Mathf.LerpAngle(a.y, b.y, t);
            ret.z = Mathf.LerpAngle(a.z, b.z, t);
            return ret;
        }
    }

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Transform _child;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Parameter _parameter;


    //　被写体などの移動更新後にカメラ更新
    private void LateUpdate()
    {
        if (_parent == null || _child == null || _camera == null)
        {
            return;
        }

        if (_parameter.trackTarget != null)
        {
            // がTransformで指定されている場合、positionパラメータに座標を上書き
            _parameter.position = Vector3.Lerp(
                a: _parameter.position,
                b: _parameter.trackTarget.position,
                t: Time.deltaTime * 4f
            );
        }


        //　パラメータを各種オブジェクトに反映
        _parent.position = _parameter.position;
        _parent.eulerAngles = _parameter.angles;

        var childPos = _child.localPosition;
        childPos.z = -_parameter.distance;
        _child.localPosition = childPos;

        _camera.fieldOfView = _parameter.fieldOfView;
        _camera.transform.localPosition = _parameter.offsetPosition;
        _camera.transform.localEulerAngles = _parameter.offsetAngles;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // マウスの動きの差分をカメラの回り込み角度に反映
        Vector3 diffAngles = new Vector3(
            x: -Input.GetAxis("Mouse Y"),
            y: Input.GetAxis("Mouse X")
        ) * 10f;
        _parameter.angles += diffAngles;
    }
}