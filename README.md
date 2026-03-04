
⚠️ Portfolio Notice

This repository exists only as a demonstration of my work. The code may be viewed and tested but may not be reused in other projects.


# Flungus-FrightClub

is the project I worked on during the second semester of my education at the _School for Games_ (S4G) in colaboration with 9 of my felow students -_you can find their names on the itch page_-. Using Unity Engine was a requirement as per the S4G's curriculum.

Concidering the 10-week deadline of the project and that this was my first experience making a multiplayer game, I find this project to be a total success. I got some practice using _Netcode for GameObjects_ and incorporating Unity services (Unity Relay) into the game, as well as learning some do's and dont's of multiplayer programming. That said i must admit that a lot of the production code is my first attempt and rushed, and thus is often not up to my standart. 

Additionaly my focus has shifted awawy from Multiplayer so i would like to shift your focus to other Systems i continued improving after the project:

## Scene Management using Unity Addressables

Source: Assets/Team3/Core/SceneManagement/

Already before the project began I disliked the default implementation of Unity Scene List. Inspired by one of the S4G's lecturer mentioning Unity's Addressable System, I tryed to design and implement a custom _Scene Management System_.

This first iteration of the system worked like this:

### 1. Scene Map Generator | Scene Map

<img align="right" width="290" height="461" alt="image" src="https://github.com/user-attachments/assets/87ca53e6-8d29-44ea-8318-15f0ba9c4035" />

You would add all relavent _Scene Assets_ to the _Scene Map Generator_ and then _Cook_ the scene map, resulting in a C# file containing an enum and Dictionary, where the enum is generated from the names of the _Scene Assets_. The Dictionary contains the enum type as Key and the _Asset Reference_ of the _Scene Assets_ as Value.

### 2. Scene Changer

Is the _Monobehaviour_ that is placed into a Scene to Change the currently showing Scenes. The Scenes that can be dispayed -they are part of the Generated SceneMap's enum- are shown as a dropdown. Then, when prompted at runtime it uses the _Asset Reference_ to load the specified Scene via the Addressable System.

<img width="507" height="258" alt="image" src="https://github.com/user-attachments/assets/2113dbec-5b52-4b3d-aafe-393f653a8377" />

<br clear="right"/>

### 3. Runtime Scene Container

A static class that simply keeps track of which Scenes are currently loaded by _Asset Reference_ and whether a _Scene Changer_ is currently operating to avoid multiple _Scene Changers_ to operate simultaneously.

##
Altho I think this iteration had clear advantages I was certain i could still improve on this. Among other things this System is not Portable to other projects because of the way the _Scene Map_ works as well as it is not very expandable. 

If you'd like, read more about the improved version [here]().

## Semi Physics-based Charactermovement
