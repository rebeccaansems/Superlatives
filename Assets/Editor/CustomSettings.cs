﻿using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.SceneManagement;

public class CustomSettings : EditorWindow
{

    [MenuItem("Custom/Reset Playerprefs")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
}
