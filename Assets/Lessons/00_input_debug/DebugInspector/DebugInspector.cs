using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInspector : MonoBehaviour  // <--------- can attach to gameObject
{
    [Header("ShowInInspector")]
    // Show Inspector
    public int publicInt;
    // Not Show Inspector
    private int privateInt;
    // Keep it private but still show on inspector
    [SerializeField] private int serializeInt;
    public readonly int readOnlyInt;

    [Header("float")]
    public float float_a = 8.5f;
    public float float_b = .5f;     // same as 0.5f

    [Header("string")]
    public string emptyString = "";
    public string justString = "i am string";

    // define variables in same type at the same time
    public float A = 5, B = 3;

    [Header("stringFormat")]
    public string formatString_1;
    public string formatString_2;

    [Header("Array")]
    public int[] uninitArray;
    // array can't changes thier size after init
    public int[] initEmptyArray = new int[10];
    public int[] initArray = new int[] { 1, 2, 3, 4, 5, 6 };

    [Header("List")]
    public List<int> listDefault;
    public List<int> listAssignValueOnInit = new() { 4,5,6};
    public List<int> listCloneOther;

    [Header("Class")]
    // Use SerializeReference if this field needs to hold both
    // Apples and Oranges.  Otherwise only m_Data from Base object would be serialized
    [SerializeReference]
    public Base m_Item = new Apple();

    [SerializeReference]
    public Base m_Item2 = new Orange();

    // Use by-value instead of SerializeReference, because
    // no polymorphism and no other field needs to share this object
    public Apple m_MyApple = new();

    private void Start()
    {
        formatString_1 = "A : " + A + " ,B : " + B + " ,A+B : " + (A + B);
        formatString_2 = $"A : {A} ,B : {B} ,A+B : {A + B}";

        listCloneOther = new(listAssignValueOnInit);
    }
}

[Serializable]
public class Base
{
    public int m_Data = 1;
}

[Serializable]
public class Apple : Base
{
    public string m_Description = "Ripe";
}

[Serializable]
public class Orange : Base
{
    public bool m_IsRound = true;
}
