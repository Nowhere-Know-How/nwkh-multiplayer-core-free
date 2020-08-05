# Nowhere Know-How Multiplayer Core

<p align="center">
    <img width="400" height="308" src="Assets/Sprites/logo.png">
</p>

## Summary
This repository is a multiplayer game framework composed of free assets and tools. The intent is to have users fork this repository when starting a new multiplayer project. 

## Set up
This code has external dependencies. Install these first by following the instructions in the links below.
- [Unity 2019.4.5f1](https://unity.com/)
- [Docker](https://www.docker.com/get-started)
- Install [Vivox for Unity](https://assetstore.unity.com/packages/tools/audio/vivox-voice-and-text-chat-148188) from the Asset Store through Unity
    - Verify installation by checking that the Assets/Vivox/Plugins directory contains dll libraries.

## Get your own keys and credentials
- Sign up for a developer account with [Vivox](https://developer.vivox.com/) and create a Sandbox Environment. Gather the following keys.
    - Environment
        - API End-Point
        - Domain
    - API Keys
        - Issuer
        - Secret Key

## Run Instructions
- Start Server
    - Toolbar -> Run -> Server -> Start Server
- Build Client
    - Toolbar -> Build -> Development
- Configure Unity Editor to start Play Mode with initial server discovery scene
    - File -> Scene Autoload -> Select Master Scene -> _ServerDiscovery
- Run Client A
    - Click Play in Editor
- Run Client B
    - Open Build and Run Game
- Create accounts in both clients
- Log in to separate accounts in both clients

## Architecture
<p align="center">
    <img width="811" height="480" src="Documentation/images/Architecture.png">
</p>

## Technology
- [Unity](https://unity.com/)
- [Docker](https://www.docker.com/get-started)
- [Nakama](https://heroiclabs.com/)
- [CockroachDB](https://www.cockroachlabs.com/)
- [Vivox](https://developer.vivox.com/)
- [Unity Assets](https://assetstore.unity.com/)
    - [Invector Third Person Controller - Basic Locomotion FREE](https://assetstore.unity.com/packages/tools/utilities/third-person-controller-basic-locomotion-free-82048)
    - [Vivox for Unity](https://assetstore.unity.com/packages/tools/audio/vivox-voice-and-text-chat-148188)
    - [Nakama for Unity](https://assetstore.unity.com/packages/tools/network/nakama-81338)

## Features
- Server discovery
- Account registration
- Login
- Logout
- Persistent player location on logout
- Network synchronized third-person movement
- Network scene instancing
- Persistent scene instancing
- Vivox Voice Communication