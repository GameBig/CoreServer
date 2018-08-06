using Server;
using UnityEngine;
namespace Client
{
    class Player:Entity
    {
        public override void Start()
        {
            AddComponent<PositionComponent>();
            var r= AddComponent<CycleComponent>().r = 10;
            var ui = AddComponent<UIComponent>().gameObject;
            //ui.transform.localScale = Vector3.one * r / 10f;
            //ui.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
