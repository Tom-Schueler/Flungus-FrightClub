
⚠️ PortfolIo NotIce

This repository exists only as a demonstration of my work. The code may be viewed and tested but may not be reused in other projects.


# Flungus-FrightClub

is the project I worked on during the second semester of my education at the _School for Games_ (S4G) in collaboration with 9 of my fellow students -_you can find their names on the itch page_. Using Unity Engine was a requirement as per the S4G's curriculum.

It is a, multiround 4 Player, Party Multiplayer where the Players can choose from 3 weapons to fight each other with. Additionaly the playrs get a perk that improve the Characters abilitys. 

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/d7f90ebb-2526-43c1-97f8-1547bfd1f35d" />

Considering the 10-week deadline of the project and that this was my first experience making a multiplayer game, I find this project to be a total success. I got some practice using _Netcode for GameObjects_ and incorporating Unity services (Unity Relay) into the game, as well as learning some do's and don'ts of multiplayer programming. That said, I must admit that a lot of the production code is my first attempt and rushed, and thus is often not up to my standard. 

Additionally my focus has shifted away from Multiplayer so I would like to shift your focus to other Systems I continued improving after the project:

## Scene Management using Unity Addressables

Source: [Assets/Team3/Core/SceneManagement/](Assets/Team3/Core/SceneManagement/)

Already before the project began I disliked the default implementation of Unity Scene List. Inspired by one of the S4G's lecturers mentioning Unity's Addressable System, I tried to design and implement a custom _Scene Management System_.

This first iteration of the system worked like this:

### 1. [Scene Map Generator](Assets/Team3/Core/SceneManagement/Editor/SceneMapGenerator/) | [Scene Map](Assets/Team3/Scenes/SceneMap/SceneMap.cs)

<img align="right" width="290" height="461" alt="image" src="https://github.com/user-attachments/assets/87ca53e6-8d29-44ea-8318-15f0ba9c4035" />

You would add all relevant _Scene Assets_ to the _Scene Map Generator_ and then _Cook_ the scene map, resulting in a C# file containing an enum and Dictionary, where the enum is generated from the names of the _Scene Assets_. The Dictionary contains the enum type as Key and the _Asset Reference_ of the _Scene Assets_ as Value.

### 2. [Scene Changer](Assets/Team3/Core/SceneManagement/Runtime/SceneChanger)

is the _MonoBehaviour_ that is placed into a Scene to Change the currently showing Scenes. The Scenes that can be displayed -they are part of the Generated SceneMap's enum- are shown as a dropdown. Then, when prompted at runtime it uses the _Asset Reference_ to load the specified Scene via the Addressable System.

<img width="507" height="258" alt="image" src="https://github.com/user-attachments/assets/2113dbec-5b52-4b3d-aafe-393f653a8377" />

<br clear="right"/>

### 3. [Runtime Scene Container](Assets/Team3/Core/SceneManagement/Runtime/Misc/RuntimeSceneContainer.cs)

A static class that simply keeps track of which Scenes are currently loaded by _Asset Reference_ and whether a _Scene Changer_ is currently operating to avoid multiple _Scene Changers_ to operate simultaneously.

##
Although I think this iteration had clear advantages I was certain I could still improve on it. Among other things this system is not Portable to other projects because of the way the _Scene Map_ works as well as it is not very expandable. 

If you'd like, read more about the improved version [here](https://github.com/KEKWdetlef).

## Semi Physics-based Charactermovement

Source: 
- [Assets/Team3/Core/Characters/CharacterMovement.cs](Assets/Team3/Core/Characters/CharacterMovement.cs)
- [Assets/Team3/Core/Characters/States](Assets/Team3/Core/Characters/States)

Making a Physics-based Charactermovement was one of my priorities for the project. Quite frankly after the project was finished, I felt like I failed at this task. Even tho it worked and felt Okay to Play, the architecture of the whole system made me really unhappy. 

Among other things, the "_CharacterMovement_" class suddenly was responsible for much more than just the movement and totally bloated. I also decided that I would put the actual [movement logic](Assets/Team3/Core/Movement) into separate static helper classes, a decision that would come to haunt me in the end.

Immediately after the project I invested myself to improve the Movement, which ended up being a great learning experience in Architecture. I also referenced Unreal Engine's Controller and Pawn model in the improved system. 

If you'd like, read more about the improved version [here](https://github.com/KEKWdetlef).
