using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransformation : MonoBehaviour
{
    public Image image;
    private bool isTransformation = false;
    public float TransformTimeLimit = 5.0f;
    public float TransformTimeLimitMax = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // クールタイムを終えていて、変身していなかったら
        if (!isTransformation)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetTransformationflag(true);
            }
        }
        //else if(GetTransfomaitonFlag())
        //{
        //    if(TransformTimeLimit <= 0.0f)
        //    {
        //        SetTransformationflag(false);
        //        Debug.Log("変身強制解除");
        //    }
        //}
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetTransformationflag(false);
            }
        }

        Debug.Log(isTransformation);
        CoolTime();
    }

    void SetTransformationflag(bool flag)
    {
        isTransformation = flag;
    }

    public bool GetTransfomaitonFlag()
    {
        return isTransformation;
    }

    void CoolTime()
    {
        if (GetTransfomaitonFlag())
        {
            TransformTimeLimit -= 1.0f * Time.deltaTime;

            if (TransformTimeLimit <= 0.0f)
            {
                SetTransformationflag(false);
                //Debug.Log("変身強制解除");
            }
        }
        else
        {
            if(TransformTimeLimit < TransformTimeLimitMax)
            {
                //Debug.Log("Time回復中");
                //Debug.Log(TransformTimeLimit);

                TransformTimeLimit += 1.0f * Time.deltaTime;
            }
        }

        image.fillAmount = TransformTimeLimit / TransformTimeLimitMax;
    }
}