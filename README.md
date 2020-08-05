# Nowhere Know-How Multiplayer Core

<p align="center">
    <img width="400" height="308" src="https://github.com/HappyMaki/nwkh-multiplayer-core/blob/master/Assets/Sprites/logo.png">
</p>

## Set up
This code has external dependencies. Install these first by following the instructions in the links below.
- [Unity](https://unity.com/)
- [Docker](https://www.docker.com/get-started)
- Install [Vivox](https://assetstore.unity.com/packages/tools/audio/vivox-voice-and-text-chat-148188) from the Asset Store through Unity
    - Verify installation by checking that the Assets/Vivox/Plugins directory is not empty.

## Get your own keys and credentials
- Sign up for a developer account with [Vivox](https://developer.vivox.com/) and create a Sandbox Environment. Gather the following keys.
    - Environment
        - API End-Point
        - Domain
    - API Keys
        - Issuer
        - Secret Key

## Run Instructions
- Build -> Development
- Run -> Server -> Start Server
- Set Master Scene to server discovery scene from File -> Scene Autoload -> Select Master Scene
- Click Play in editor
- Open Build and Run Game
- Create accounts
- Log in

## Architecture
- Incoming Miro pictures
    - Unity
    - Nakama
    - Vivox

## Features
- Server discovery
- Register new accounts
- Login to game with email and credentials
- Multiplayer Movement
- Multiplayer Scene Changing
- Persistent Scene Instancing
- Logout
- Player Location Save on Logout
- Voice Communication based on scene.