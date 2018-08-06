using Server;
using System;
public class EventSender : Behavior,IUpdate
{
    public void Regist(string ename, Action action)
    {
        var e= world.Globel.GetComponent<EventComponent>();
        if (e.funcs.ContainsKey(ename))
        {
            e.funcs[ename] += action;
        }
        else
        {
            e.funcs.Add(ename, action);
        }
    }
    public void Remove(string ename,Action action)
    {
        var e = world.Globel.GetComponent<EventComponent>();
        if (e.funcs.ContainsKey(ename))
        {
            e.funcs[ename] -= action;
        }
    }
    public void Invoke(string ename)
    {
        var e = world.Globel.GetComponent<EventComponent>();
        e.fname.Enqueue(ename);
    }

    public void Update(float tick)
    {
        var e = world.Globel.GetComponent<EventComponent>();
        while (e.fname.Count > 0)
        {
            string ename = e.fname.Dequeue();
            if (e.funcs.ContainsKey(ename))
            {
                e.funcs[ename].Invoke();
            }
        }
    }
}
