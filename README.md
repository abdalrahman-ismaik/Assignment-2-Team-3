# **COSC 495 - Unity Game Development (Assignment 2)**
## Overview
This project is a Unity-based soccer-style game developed as part of COSC 495 - Unity Game Development. The game includes multiple enhancements such as optimized enemy behavior, improved UI, a scoring system, power-ups, and audio effects to create an engaging gameplay experience.
## Team Members & Roles
- Abd Alrahman Ismaik (Project Lead, UI/UX, Gameplay Programming, Bug Fixes)
- Tehsin Shaik (Audio Management, UI/UX, Game Mechanics, Bug Fixes)
- Faisal Alzarooni (Level Design, UI/UX, Game World & Scene Implementation, Bug Fixes)

## Repository Structure & Branching Strategy
### Main Branches:
- **Restoring**: Primary development branch, created after a merge error. All new development continued in this branch.
- **main**: Previous main branch, used for finalized versions before switching to Restoring.

### Additional Branches:
- **Testing-Merge**: Used for testing merges before integrating into Restoring.
- **main2**: Backup of main before switching to Restoring.
- **che**: A branch used for debugging or minor fixes.
- **myedits**: Personal edits and changes by Tehsin Shaik.

## Features Implemented
- **Enhanced Enemy AI**: Improved movement and attack logic.
- **Power-Up System**: Implemented speed boost, ground slam, and other abilities.
- **Wave-Based Enemy Spawning**: Increasing difficulty as levels progress.
- **Scoring System & Wave Limit**: Introduced win/loss conditions based on player and enemy scores.
- **Main Menu & Pause Menu**: Fully interactive UI with difficulty selection and game controls.
- **Audio Features**:
  - Dynamic Background Music
  - Sound Effects for Goals, Power-ups, and Interactions
- **Minimap Camera**: Helps players navigate the game more efficiently.

## Challenges & Solutions
### 1. Managing Multiple Active Powerups
**Issue**: Overlapping effects caused player confusion.
**Solution**: Implemented a state-tracking system ensuring only one power-up is active at a time.

### 2. Game UI Alignment Issues Across Resolutions
**Issue**: UI elements misaligned on different screen sizes.
**Solution**: Used Canvas Scaling to ensure consistent UI experience across devices.

### 3. Git Version Control & Scene Recovery
**Issue**: Unity scene files were lost due to merge conflicts.
**Solution**: Created a recovery branch, manually reintegrated lost components, and restructured Git workflow for better version control.

## Attribution
**Developed for COSC 495 - Unity Game Development, Spring 2025
Supervised by Dr. Jamal Zemerly & Dr. Lamees Alqassem**
