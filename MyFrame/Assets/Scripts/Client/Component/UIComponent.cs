using Server;
namespace Client
{
    class UIComponent:Component
    {
        public UnityEngine.GameObject gameObject;
        protected override void Create()
        {
            gameObject = CreatePrefab.Create();
        }
        public override void Destroy()
        {
            if (gameObject)
                UnityEngine.Object.Destroy(gameObject);
        }
    }
}

