using System.Linq;
using Il2CppSystem;
using Il2CppSystem.Linq;
using MelonLoader;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DisableVR
{
    public class VRDisablerMod : MelonMod
    {
        bool enabled = false;
        GameObject vrDisabler;

        public override void OnUpdate()
        {
            HandleMouseClick();
            HandleVrCamera();
        }

        void HandleVrCamera()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                if (vrDisabler == null)
                {
                    vrDisabler = new GameObject("VRDisabler", new Type[]
                    {
                        Il2CppType.Of<VRDisabler>(),
                        Il2CppType.Of<CameraFly>(),
                        Il2CppType.Of<Camera>()
                    });
                    vrDisabler.GetComponent<Camera>().fieldOfView = 100;
                }
                else
                {
                    enabled = !enabled;
                    vrDisabler.SetActive(enabled);
                }
            }
        }

        static void HandleMouseClick()
        {
            RaycastHit raycastHit;
            if (Input.GetKeyDown(KeyCode.Mouse0) &&
                Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out raycastHit))
            {
                var transform = raycastHit.transform;
                var component = transform.GetComponent<GunButton>();
                if (component != null) component.OnHit(transform.position, KataConfig.I.guns[0]);
            }
        }
    }
}