using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class g_VibratePhoneFlashScreen : Photon.MonoBehaviour
{

    public void SendVibrateFlashToPhone(PhotonPlayer target)
    {
        this.GetComponent<PhotonView>().RPC("VibrateFlash", target);
    }

    [PunRPC]
    public void VibrateFlash()
    {
#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
        Debug.Log("buzz buzz buzz");
    }
}
