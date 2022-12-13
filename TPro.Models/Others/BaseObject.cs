namespace TPro.Models.Others
{
    public class BaseObject
    {
        //通过属性名获取对象值
        public dynamic this[string name]
        {
            get
            {
                return this.GetType().GetProperty(name).GetValue(this);
            }
        }
    }
}