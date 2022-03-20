# Unity Dynamic Lightmaps

This API allows you to create multiple lightmaps "atmospheres" for the same scene and switch between them in runtime dynamically, or you can make multiple environments as dynamic gameobjects where you can enable, disable or instantiate it at runtime and switch between the previously baked lightmaps "atmospheres" for these environments when needed.

[![Video](https://img.youtube.com/vi/lF64g4dd0mw/hqdefault.jpg)](https://www.youtube.com/watch?v=lF64g4dd0mw)

## How to use this API

    1. create the environment you want inside the scene as static gameobjects
    2. bake the lightmap for this environment
    3. open the *Window -> Dynamic Lightmaps* window in the editor
    4. select the static objects that represent the environment you want in the editor 
    5. press *Add Map Data Attacher to Selections* "don't worry the editor script will automatically applay the changes to the prefab"
    6. enter the LightState name
    7. Press *Save Current Light State* "you will find all the saved light states in the folder Assets\Light States"
    8. now you can remove the static objects or the static label from these objects and append the light states for them in runtime

## Important Notes

This API support Light Probes inside Unity, But **DO NOT CHANGE THE LIGHT PROBES POSITIONS AFTER THE BAKING IS FINISHED** because Unity doesn't support changing light probes positions after baking is done and you can only change the probes positions using the editor **it's write protected**, also changing light probes positions or loading and combining them in load scene additive mode require Unity to perform a heavy operation called [Tetrahedralize](https://docs.unity3d.com/ScriptReference/LightProbes.Tetrahedralize.html)

So you have to keep the light probes fixed in the scene and place them with respect to all the environment/atmospheres you want to use.
