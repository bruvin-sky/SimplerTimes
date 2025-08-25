using HarmonyLib;
using NewHorizons.Utility;
using OWML.Common;
using OWML.ModHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using NewHorizons.Utility.Files;
using System.Linq;
using NewHorizons.Utility.OWML;
using NewHorizons.Components.Orbital;
using Autodesk.Fbx;
using Epic.OnlineServices;
using NewHorizons.Utility.OuterWilds;
using NewHorizons.Handlers;
using NewHorizons.External.Modules.VariableSize;
using NewHorizons.Components;

namespace IntactMod2
{
    public class IntactMod2 : ModBehaviour
    {
        public static IntactMod2 Instance;

        private void Awake()
        {
            Instance = this;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void Update()
        {
            TimeLoop._loopDuration = 2820;
            if (TimeLoop.GetMinutesElapsed() > 44) TimeLoop.SetSecondsRemaining(2820);

        }

        public void FixedUpdate()
        { 
            
        }
        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(IntactMod2)} is loaded!", MessageType.Success);

            // Get the New Horizons API and load configs
            var newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
            newHorizons.LoadConfigs(this);

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;
                ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);

                Destroy(obj: GameObject.Find("FocalBody/Sector_HGT").GetComponent<EffectRuleset>());
                Destroy(obj: GameObject.Find("FakeCannonBarrel_Body (1)/Structure_NOM_OrbitalProbeCannon_Proxy"));
                Destroy(obj: GameObject.Find("FakeCannonMuzzle_Body (1)/Structure_NOM_OrbitalProbeCannon_Proxy"));
                Destroy(obj: GameObject.Find("GiantsDeep_Body/Sector_GD/Sector_GDInterior/Sector_GDCore/Sector_Module_Sunken/Interactables_Module_Sunken/Prefab_NOM_RemoteViewer (1)"));
                Destroy(obj: GameObject.Find("SS_Debris_Body/Structure_NOM_SunStation_Debris_Small"));
                Destroy(obj: GameObject.Find("SS_Debris_Body/Structure_NOM_SunStation_Debris_Big"));

                Destroy(obj: GameObject.Find("TowerTwin_Body/SandSphere_Draining"));
                Destroy(obj: GameObject.Find("TowerTwin_Body/SandSphere_Draining").GetComponent<SandLevelController>());
                Destroy(obj: GameObject.Find("FocalBody/HourglassTwinsEffects").GetComponent<HourglassTwinsShaderController>());
                Destroy(obj: GameObject.Find("RingWorld_Body").GetComponent<RingWorldController>());

                var sunController = GameObject.Find("Sun_Body").GetComponent<SunController>();
                sunController._progressionEndTime = 50;
                sunController._progressionStartTime = 50;
                sunController._scaleEndTime = 50;
                sunController._scaleStartTime = 50;
            };


        }
    }

}
