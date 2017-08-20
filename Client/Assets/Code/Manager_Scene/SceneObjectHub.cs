using System.Collections.Generic;
using UnityEngine;
public class SceneObjectHub
{
    public MapItemHub mapItemHub { get; private set; }
    public SceneObjectHub()
    {
        mapItemHub = new MapItemHub();
    }
}