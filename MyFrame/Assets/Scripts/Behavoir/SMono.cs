using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Server;
public class SMono : MonoBehaviour
{
    protected World world { get; private set; }
    protected Client client { get; private set; }
    protected virtual void Start()
    {
        client=FindObjectOfType<Client>();
        world = client.world;
    }
}
