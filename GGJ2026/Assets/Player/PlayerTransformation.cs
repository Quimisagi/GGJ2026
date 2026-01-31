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
    public float coolTime = 0.0f;
    public float coolTimeMax = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = coolTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        // クールタイムを終えていて、変身していなかったら
        if (!isTransformation && coolTime <= 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetTransformationflag(true);
            }
        }
        else if(GetTransfomaitonFlag())
        {
            TransformTimeLimit -= 1.0f * Time.deltaTime;

            if(TransformTimeLimit <= 0.0f)
            {
                SetTransformationflag(false);
                Debug.Log("変身強制解除");
                
                TransformTimeLimit = TransformTimeLimitMax;
                Debug.Log(TransformTimeLimit);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetTransformationflag(false);
            }
        }

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
        if (!GetTransfomaitonFlag())
        {
            if (coolTime > 0.0f)
            {
                coolTime -= 1.0f * Time.deltaTime;
            }
            else
            {
                coolTime = 0.0f;
            }
        }
        else
        {
            if (coolTime <= 0.0f)
            {
                coolTime = coolTimeMax;
            }
        }

        image.fillAmount = coolTime / coolTimeMax;
    }
}