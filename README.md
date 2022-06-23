# Unity Dynamic Lightmaps

This API allows you to create multiple lightmaps "atmospheres" for the same scene and switch between them in runtime dynamically, or you can make multiple environments as dynamic gameobjects where you can enable, disable or instantiate it at runtime and switch between the previously baked lightmaps "atmospheres" for these environments when needed.


https://user-images.githubusercontent.com/39844467/162445968-db07a4fa-41e8-4416-a8b2-e68aacabd6be.mp4


## How to use this API
#### *1. create the environment you want inside the scene as static gameobjects.*
![image](https://user-images.githubusercontent.com/39844467/175179008-7c24719e-64a1-4613-ab8c-a5657389d0ef.png)
#### *2. bake the lightmap for this environment.*
![image](https://user-images.githubusercontent.com/39844467/175179095-5733e037-12f9-44ed-8b63-8301b169af80.png)
#### *3. open the "Window -> Dynamic Lightmaps" in the editor.*
![image](https://user-images.githubusercontent.com/39844467/175179129-469f5a88-0058-4827-8ebc-f78be7f9a4f5.png)
#### *4. select the static objects that represent the environment you want in the editor, And press "Add Map Data Attacher to Selections" don't worry the editor script will automatically applay the changes to the prefabs, Then enter the LightState name, Then press "Save Current Light State".*
![image](https://user-images.githubusercontent.com/39844467/175179461-bf822181-3b10-4f63-a90f-aca6bc461a14.png)
#### *5. you will find all the saved light states in the folder "Assets\Light States".*
![image](https://user-images.githubusercontent.com/39844467/175179597-2fbc517f-be71-47e3-ba66-8d91aef3b4ec.png)
#### *6. now you can remove the static objects or the static label from these objects and append the light states for them in runtime using any script you want "CHECK THE MANAGER SCRIPT FOR REFERENCE.*
![image](https://user-images.githubusercontent.com/39844467/175179700-0c4adf9a-3547-4dd1-8dc7-b90f05efbcc3.png)
#### *7. if you open the manager gameobject you will find the Manager component which take care of LightStates and the environments we made previously.*
![image](https://user-images.githubusercontent.com/39844467/175179859-350bedf2-6f3c-4f86-81ee-95e9e463e039.png)
#### *8. if you wish you could bake the lightmap for the scene when it's empty to make a clean switch to the light states when the game starts.*
![image](https://user-images.githubusercontent.com/39844467/175180921-3fcb7917-e1b9-4ee7-add5-ba6fc974e161.png)
    

## Important Notes

This API support Light Probes inside Unity, But **DO NOT CHANGE THE LIGHT PROBES POSITIONS AFTER THE BAKING IS FINISHED** because Unity doesn't support changing light probes positions after baking is done and you can only change the probes positions using the editor **it's write protected**, also changing light probes positions or loading and combining them in load scene additive mode require Unity to perform a heavy operation called [Tetrahedralize](https://docs.unity3d.com/ScriptReference/LightProbes.Tetrahedralize.html)

So you have to keep the light probes fixed in the scene and place them with respect to all the environment/atmospheres you want to use.
