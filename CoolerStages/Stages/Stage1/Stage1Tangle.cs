using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

public class Stage1Something
{
  // RoR2/Base/Common/TrimSheets/matTrimsheetPurpleStoneSkymeadow.mat
  // RoR2/Base/intro/matColonyShipStandard.mat
  // RoR2/Base/Common/TrimSheets/matTrimSheetAlien3Wires.mat
  // RoR2/Base/Common/TrimSheets/matTrimSheetAlien1Lichen.mat
  private static readonly PostProcessProfile shroudProfile = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneArtifactWorld.asset").WaitForCompletion();
  private static readonly Material shroudTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMTerrainInfiniteTower.mat").WaitForCompletion();
  private static readonly Material shroudTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMTerrainInfiniteTower.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMSpikeBridgeInfinitetower.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matTrimSheetMeadowRuinsProjectedInfiniteTower.mat").WaitForCompletion();
  private static readonly Material shroudDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_TempleDecal2.mat").WaitForCompletion();
  private static readonly Material shroudSkyMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matSkyboxArtifactWorld.mat").WaitForCompletion();
  private static readonly Material shroudSkyMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphere.mat").WaitForCompletion();
  private static readonly Material shroudSkyMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/artifactworld/matAWSkySphereSparks.mat").WaitForCompletion();
  private static readonly Material shroudGrassMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/LeavesInfiniteTower_0_LOD0.mat").WaitForCompletion();
  private static readonly Material leaves = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/FacingLeaves_3.mat").WaitForCompletion();
  private static readonly Material frond = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Fronds_1.mat").WaitForCompletion();
  private static readonly Material branch = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
  private static readonly Material branch2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
  private static readonly Material grassMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Props/Branches_0.mat").WaitForCompletion();
  // RoR2/DLC1/itskymeadow/LeavesInfiniteTower_0_LOD0.mat
  private static readonly GameObject shroudPlainsGrass1 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
  private static readonly GameObject shroudPlainsGrass2 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
  private static readonly GameObject shroudPlainsGrass3 = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/itskymeadow/spmSMGrassInfiniteTower.spm").WaitForCompletion();
  private static readonly GameObject[] shroudGrassArr = new GameObject[3] { shroudPlainsGrass1, shroudPlainsGrass2, shroudPlainsGrass3 };

  private static void SetAmbience()
  {
    GameObject.Find("Directional Light (SUN)").SetActive(false);
    GameObject.Find("Reflection Probe").SetActive(false);
    GameObject probe2 = GameObject.Find("Reflection Probe");
    if (probe2)
      probe2.SetActive(false);
    GameObject origAmb = GameObject.Find("PP + Amb");
    GameObject weather = GameObject.Find("Weather, Golemplains");
    if (origAmb)
      origAmb.SetActive(false);
    if (weather)
      weather.SetActive(false);
    GameObject pp = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/golemplains/Weather, Golemplains.prefab").WaitForCompletion());
    GameObject amb = pp.transform.GetChild(2).gameObject;
    PostProcessVolume ppv = amb.GetComponent<PostProcessVolume>();
    ppv.sharedProfile = shroudProfile;
    ppv.priority = 9999f;
    SetAmbientLight ambientLight = amb.GetComponent<SetAmbientLight>();
    ambientLight.skyboxMaterial = shroudSkyMat;
    //ambientLight.ambientSkyColor = new Color(0.7f, 0.7f, 1, 1); 
    ambientLight.ambientIntensity = 0.75f;
    ambientLight.ApplyLighting();

    Light sunLight = pp.transform.GetChild(1).GetComponent<Light>();
    sunLight.color = new Color(0.9608f, 0.9541f, 0.7843f, 1);
    sunLight.intensity = 0.5f;

    Transform skybox = Object.Instantiate(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion().transform.GetChild(6).GetChild(0), pp.transform);
    skybox.gameObject.GetComponent<MeshRenderer>().sharedMaterials = new Material[3] { shroudSkyMat, shroudSkyMat2, shroudSkyMat3 };
  }

  public static void Roost1()
  {
    SetAmbience();

    // Roost1(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudTerrainMat, shroudTreeFrond, shroudTreeFrond, shroudTreeLeaves);
    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          renderer.sharedMaterial = shroudGrassMat;
          meshBase.transform.localScale *= 0.5f;
        }
        if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
        {
          if (meshBase.name.Contains("Young"))
            renderer.sharedMaterials = new Material[3] { branch, leaves, branch };
          else renderer.sharedMaterials = new Material[4] { frond, branch, branch2, leaves };
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat2;
        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("Fern"))
          meshBase.SetActive(false);
      }
    }
  }

  public static void Roost2()
  {
    SetAmbience();

    // Roost2(shroudTerrainMat, shroudTerrainMat2, shroudDetailMat, shroudDetailMat2, shroudTerrainMat, shroudTreeFrond, shroudTreeFrond, shroudTreeLeaves);
    Transform terrain = GameObject.Find("HOLDER: Terrain").transform.GetChild(0);
    terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = shroudTerrainMat;
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform);
          cunt.transform.localPosition = Vector3.zero;
          GameObject.Destroy(meshBase.GetComponent<MeshRenderer>());
        }
        if (meshBase.name.Contains("spmBbConif") && renderer.sharedMaterials.Length > 0)
        {
          if (meshBase.name.Contains("Young"))
            renderer.sharedMaterials = new Material[3] { branch, leaves, branch };
          else if (meshBase.name.Contains("LOD1"))
            renderer.sharedMaterials = new Material[4] { frond, branch, branch2, leaves };
          else
            renderer.sharedMaterials = new Material[3] { branch, branch, leaves };
        }
        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if ((meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat2;
        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("Fern"))
          meshBase.SetActive(false);
      }
    }
  }

  public static void Plains()
  {
    SetAmbience();

    // Plains(shroudTerrainMat, shroudDetailMat, shroudDetailMat2, shroudPlainsGrass);
    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform.parent);
          cunt.transform.localPosition = Vector3.zero;
          // cunt.transform.localScale *= 0.25f;
          GameObject.Destroy(meshBase);
        }
        if ((meshBase.name.Contains("Terrain") || meshBase.name == "Wall North") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat;
        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("Ring") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
      }
    }
  }

  public static void Forest()
  {
    SetAmbience();

    MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
    foreach (MeshRenderer renderer in meshList)
    {
      GameObject meshBase = renderer.gameObject;
      if (meshBase != null)
      {
        if (meshBase.name == "meshSnowyForestGiantTreesTops")
          meshBase.gameObject.SetActive(false);
        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
        {
          GameObject cunt = GameObject.Instantiate(shroudGrassArr[UnityEngine.Random.Range(0, 3)], meshBase.transform.parent);
          cunt.transform.localPosition = Vector3.zero;
          cunt.transform.localScale *= 0.25f;
          GameObject.Destroy(meshBase);
        }
        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudTerrainMat;
        if ((meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat2;
        if ((meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk")) && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("meshSnowyForestGiantTrees") && renderer.sharedMaterial)
          renderer.sharedMaterial = shroudDetailMat;
        if (meshBase.name.Contains("mdlSnowyForestTreeStump") && renderer.sharedMaterial)
          renderer.sharedMaterials = new Material[2] { shroudDetailMat, shroudDetailMat };
      }
    }
  }
}