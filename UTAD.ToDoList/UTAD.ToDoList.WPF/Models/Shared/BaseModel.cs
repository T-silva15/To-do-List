namespace UTAD.ToDoList.WPF.Models.Shared
{
    public abstract class BaseModel
    {
        public string id { get; set; }

        protected BaseModel()
        {
            if(string.IsNullOrEmpty(id))
                id = Guid.NewGuid().ToString();
        }
    }
}
