# WOWS-Model-Tool

**Modified from Lotsbiss's WOT Model Mod. This tool is not supposed to be shared in public.**

## Download
Please find the build in the [latest release](https://github.com/SEA-group/WoT-Model-Tool-WoWS-Refit/releases)

## Update history

### 2020.07.14b
* Tried to fix the previous bug that cause list32 cannot be modified twice. The fix is likely working but I still feel that something's wrong...
* **Meanwhile, the list32 models modified with 2020.07.14a cannot be read by 2020.07.14b**

### 2020.07.14a
* Added the support of list32 index type (i.e. model parts with more than 65535 vertices)
* **Known bug : models with list32 index type can only be replaced once. The tool will then be unable to read the primitives file (but the game runs well with the modified primitives...**

### 2020.06.27
* Added the support of xyznuvr vertex type (wire model)
* Updated the support of xyznuv vertex type's texture coordinates (glass & grid model)