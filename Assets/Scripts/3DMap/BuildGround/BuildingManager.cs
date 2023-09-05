using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //Since we are not expected to touch buildings that much, I initialized List as Gameobject.
    //This can always change as my project become bigger.
    [SerializeField]
    List<GameObject> buildings;

    //======Event Related======//
    [SerializeField]
    private JSON_BuildingManager_Init_SO buildingInitEvent;
    //=========================//

    private void OnEnable()
    {
        buildingInitEvent.jsonBuildingInitEvent.AddListener(GetInitInfo);
    }
    private void OnDisable()
    {
        buildingInitEvent.jsonBuildingInitEvent.RemoveListener(GetInitInfo);
    }

    public void GetInitInfo(List<BuildingInfo> bi)
    {
        foreach (BuildingInfo bds in bi)
        {
            buildings.Add(Instantiate<GameObject>(Resources.Load<GameObject>(Whereabouts.Building3d + bds.prefabName),
                new Vector3(bds.xyz[0], bds.xyz[1], bds.xyz[2]),
                Quaternion.Euler(bds.rotation[0], bds.rotation[1], bds.rotation[2]),
                this.transform)
                );
            buildings[bds.index].transform.localScale = new Vector3(bds.scale[0], bds.scale[1], bds.scale[2]);
            buildings[bds.index].GetComponent<TactMap_Buildings>().InitBuilding(bds.index);
        }    
    }
}
