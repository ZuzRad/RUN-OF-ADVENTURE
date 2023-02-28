using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticData : MonoBehaviour
{
    public static int actualStage=3;
    public static int ActualStage
    {
        get { return actualStage; }
        set { actualStage = value; }
    }


}
