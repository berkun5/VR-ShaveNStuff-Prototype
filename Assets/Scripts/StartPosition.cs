using System.Collections;
using UnityEngine;
using Valve.VR;

public class StartPosition : MonoBehaviour
{
    public GameObject startPos, vrCam;

    IEnumerator Start()
    {
        while (SteamVR.initializedState == SteamVR.InitializedStates.None || SteamVR.initializedState == SteamVR.InitializedStates.Initializing) { yield return null; }
        yield return new WaitForSeconds(.5f);
        Vector3 globalCamPos = vrCam.transform.position;
        Vector3 globalPlayerPos = this.transform.position;
        Vector3 offset = new Vector3(globalCamPos.x - globalPlayerPos.x, 0, globalCamPos.z - globalPlayerPos.z);
        this.transform.position = new Vector3(startPos.transform.position.x - offset.x, this.transform.position.y, startPos.transform.position.z - offset.z);
    }

}

