using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Whilefun.FPEKit;

public class FadeToBlackTrigger : MonoBehaviour
{

    public int connectedSceneIndex = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        LocationInfoManager.instance.blackBackdrop.gameObject.SetActive(true);
        LocationInfoManager.instance.blackBackdrop.DOFade(1f, .5f).OnComplete(() => FPESaveLoadManager.Instance.ChangeSceneToAndAutoSave(connectedSceneIndex));

    }
}
