using System.Collections.Generic;
using Server;
using System;
[Globel]
public class EventComponent : Component
{
    public Dictionary<string, Action> funcs = new Dictionary<string, Action>();
    public Queue<string> fname = new Queue<string>();
}
