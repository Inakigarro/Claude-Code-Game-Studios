# Unity 6.3 LTS — Breaking Changes

**Last verified:** 2026-04-27

This document tracks breaking API changes and behavioral differences between Unity 2022 LTS
(likely in model training) and Unity 6.3 LTS (current version). Organized by risk level.

## HIGH RISK — Will Break Existing Code

### Entities/DOTS API Complete Overhaul
**Versions:** Entities 1.0+ (Unity 6.0+)

```csharp
// ❌ OLD (pre-Unity 6, GameObjectEntity pattern)
public class HealthComponent : ComponentData {
    public float Value;
}

// ✅ NEW (Unity 6+, IComponentData)
public struct HealthComponent : IComponentData {
    public float Value;
}

// ❌ OLD: ComponentSystem
public class DamageSystem : ComponentSystem { }

// ✅ NEW: ISystem (unmanaged, Burst-compatible)
public partial struct DamageSystem : ISystem {
    public void OnCreate(ref SystemState state) { }
    public void OnUpdate(ref SystemState state) { }
}
```

**Migration:** Follow Unity's ECS migration guide. Major architectural changes required.

---

### Input System — Legacy Input Deprecated
**Versions:** Unity 6.0+

```csharp
// ❌ OLD: Input class (deprecated)
if (Input.GetKeyDown(KeyCode.Space)) { }

// ✅ NEW: Input System package
using UnityEngine.InputSystem;
if (Keyboard.current.spaceKey.wasPressedThisFrame) { }
```

**Migration:** Install Input System package, replace all `Input.*` calls with new API.

---

### URP/HDRP Renderer Feature API Changes
**Versions:** Unity 6.0+

```csharp
// ❌ OLD: ScriptableRenderPass.Execute signature
public override void Execute(ScriptableRenderContext context, ref RenderingData data)

// ✅ NEW: Uses RenderGraph API
public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
```

**Migration:** Update custom render passes to use RenderGraph API.

---

## MEDIUM RISK — Behavioral Changes

### Addressables — Asset Loading Returns
**Versions:** Unity 6.2+

Asset loading failures now throw exceptions by default instead of returning null.
Add proper exception handling or use `TryLoad` variants.

```csharp
// ❌ OLD: Silent null on failure
var handle = Addressables.LoadAssetAsync<Sprite>("key");
var sprite = handle.Result; // null if failed

// ✅ NEW: Throws on failure, use try/catch or TryLoad
try {
    var handle = Addressables.LoadAssetAsync<Sprite>("key");
    var sprite = await handle.Task;
} catch (Exception e) {
    Debug.LogError($"Failed to load: {e}");
}
```

---

### Physics — Default Solver Iterations Changed
**Versions:** Unity 6.0+

Default solver iterations increased for better stability.
Check `Physics.defaultSolverIterations` if you rely on old behavior.

---

## LOW RISK — Deprecations (Still Functional)

### UGUI (Legacy UI)
**Status:** Deprecated but supported
**Replacement:** UI Toolkit

UGUI still works but UI Toolkit is recommended for new projects.

---

### Legacy Particle System
**Status:** Deprecated
**Replacement:** Visual Effect Graph (VFX Graph)

---

### Old Animation System
**Status:** Deprecated
**Replacement:** Animator Controller (Mecanim)

---

## Platform-Specific Breaking Changes

### WebGL
- **Unity 6.0+**: WebGPU is now the default (WebGL 2.0 fallback available)
- Update shaders for WebGPU compatibility

### Android
- **Unity 6.0+**: Minimum API level raised to 24 (Android 7.0)
- **Unity 6.3**: Minimum API level raised again to **25** (Android 7.1)
- **Unity 6.3**: `PlayerSettings.Android.androidIsGame` obsoleted — use new App Category setting
- **Unity 6.3**: Round and legacy icon support deprecated — use adaptive icons

### iOS
- **Unity 6.0+**: Minimum deployment target raised to iOS 13

---

## Unity 6.2 — Additional Breaking Changes

### URP AfterRendering Injection Point
**Breaking Change:** `AfterRendering` now executes consistently after the final back buffer blit.
Previously it could run before additional post-processing passes.

**Migration:** Change event from `AfterRendering` to `AfterRenderingPostProcessing` to preserve previous behavior.

### SetupRenderPasses API (URP)
**Deprecation:** `SetupRenderPasses` in URP is deprecated and will be removed in a future release.

```csharp
// ❌ Deprecated (Unity 6.2+)
public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData) { }

// ✅ New: Use render graph system
public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) { }
public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData) { }
```

### VisualElement.transform API
**Deprecation:** `VisualElement.transform` is deprecated.

```csharp
// ❌ Deprecated (Unity 6.2+)
element.transform.position = new Vector3(10, 20, 0);

// ✅ For setting values
element.style.translate = new Translate(10, 20);
element.style.rotate = new Rotate(45);
element.style.scale = new Scale(new Vector2(1.5f, 1.5f));

// ✅ For reading values
var pos = element.resolvedStyle.translate;
```

## Unity 6.3 — Additional Breaking Changes

### Accessibility API
**Breaking Change:** `AccessibilityRole` enum converted from flags enum to standard enum.
**Deprecation:** `AccessibilityNode.selected` renamed to `AccessibilityNode.invoked`.

### 2D Physics — Box2D v3
A new low-level 2D physics API based on Box2D v3 is available at `UnityEngine.LowLevelPhysics2D`.
Runs alongside the existing API but will eventually replace it.

---

## Migration Checklist

When upgrading from 2022 LTS to Unity 6.3 LTS:

- [ ] Audit all DOTS/ECS code (complete rewrite likely needed)
- [ ] Replace `Input` class with Input System package
- [ ] Update custom render passes to RenderGraph API
- [ ] Add exception handling to Addressables calls
- [ ] Test physics behavior (solver iterations changed)
- [ ] Consider migrating UGUI to UI Toolkit for new UI
- [ ] Update WebGL shaders for WebGPU
- [ ] Verify minimum platform versions (Android/iOS)

---

**Sources:**
- https://docs.unity3d.com/6000.0/Documentation/Manual/upgrade-guides.html
- https://docs.unity3d.com/Packages/com.unity.entities@1.3/manual/upgrade-guide.html
