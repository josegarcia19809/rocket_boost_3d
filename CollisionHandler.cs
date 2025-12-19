using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                print("Finish");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                break;
        }
    }
}